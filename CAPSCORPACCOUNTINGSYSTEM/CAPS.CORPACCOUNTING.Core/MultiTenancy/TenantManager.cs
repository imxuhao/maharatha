using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp;
using Abp.Application.Features;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Microsoft.AspNet.Identity;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Editions;
using CAPS.CORPACCOUNTING.MultiTenancy.Demo;
using Abp.Extensions;
using Abp.Notifications;
using Abp.Runtime.Security;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Notifications;
using AutoMapper;
using CAPS.CORPACCOUNTING.Organization;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    /// <summary>
    /// Tenant manager.
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IUserEmailer _userEmailer;
        private readonly TenantDemoDataBuilder _demoDataBuilder;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;

        private readonly IRepository<TypeOfCurrencyUnit, short> _typeOfCurrencyUnit;
        private readonly IRepository<TypeOfAccountUnit> _typeOfAccountUnit;
        private readonly IRepository<RegionUnit> _regionUnit;
        private readonly IRepository<CountryUnit> _countryUnit;
        private readonly IRepository<VendorUnit> _vendorUnit;
        private readonly IRepository<User, long> _userUnit;
        private readonly IRepository<Role> _roleUnit;
        private readonly IRepository<CoaUnit> _coaUnit;
        private readonly IRepository<EmployeeUnit> _employeeUnit;
        private readonly IRepository<CustomerUnit> _customerUnit;
        private readonly IRepository<ConnectionStringUnit> _connectionStringRepository;
        private readonly IRepository<OrganizationExtended, long> _organizationRepository;




        public TenantManager(
        IRepository<Tenant> tenantRepository,
        IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
        EditionManager editionManager,
        IUnitOfWorkManager unitOfWorkManager,
        RoleManager roleManager,
        IUserEmailer userEmailer,
        TenantDemoDataBuilder demoDataBuilder,
        UserManager userManager,
        INotificationSubscriptionManager notificationSubscriptionManager,
        IAppNotifier appNotifier,
        IAbpZeroFeatureValueStore featureValueStore,
        IAbpZeroDbMigrator abpZeroDbMigrator,
        IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyUnit,
        IRepository<TypeOfAccountUnit> typeOfAccountUnit,
        IRepository<RegionUnit> regionUnit,
        IRepository<CountryUnit> countryUnit, IRepository<VendorUnit> vendorUnit, IRepository<User, long> userUnit, IRepository<Role> roleUnit,
        IRepository<CoaUnit> coaUnit, IRepository<EmployeeUnit> employeeUnit, IRepository<CustomerUnit> customerUnit, IRepository<ConnectionStringUnit> connectionStringRepository, IRepository<OrganizationExtended, long> organizationRepository) :
        base(tenantRepository, tenantFeatureRepository, editionManager, featureValueStore)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _demoDataBuilder = demoDataBuilder;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _typeOfCurrencyUnit = typeOfCurrencyUnit;
            _typeOfAccountUnit = typeOfAccountUnit;
            _regionUnit = regionUnit;
            _countryUnit = countryUnit;
            _vendorUnit = vendorUnit;
            _userUnit = userUnit;
            _roleUnit = roleUnit;
            _coaUnit = coaUnit;
            _employeeUnit = employeeUnit;
            _customerUnit = customerUnit;
            _connectionStringRepository = connectionStringRepository;
            _organizationRepository = organizationRepository;
        }


        public async Task<int> CreateWithAdminUserAsync(string tenancyName, string name, string adminPassword, string adminEmailAddress, string connectionString, bool isActive, int? editionId, bool shouldChangePasswordOnNextLogin, bool sendActivationEmail)
        {
            int newTenantId;
            long newAdminId;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                //Create tenant
                var tenant = new Tenant(tenancyName, name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    ConnectionString = connectionString.IsNullOrWhiteSpace() ? null : SimpleStringCipher.Instance.Encrypt(connectionString)
                };

                CheckErrors(await CreateAsync(tenant));
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
                {
                    //Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Create admin user for the tenant
                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = User.CreateRandomPassword();
                    }

                    var adminUser = User.CreateTenantAdminUser(tenant.Id, adminEmailAddress, adminPassword);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = isActive;

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _demoDataBuilder.BuildForAsync(tenant);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;
                }
                if (string.IsNullOrEmpty(connectionString))
                    await CustomTenantSeeding(newTenantId);

                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(new UserIdentifier(newTenantId, newAdminId));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }

            return newTenantId;
        }

        public async Task<int> CreateWithAdminUserAsync(string tenancyName, string name, string adminPassword, string adminEmailAddress, bool isActive, int? editionId, bool shouldChangePasswordOnNextLogin,
            bool sendActivationEmail, long? organizationId, int? sourcetenantId, List<string> entityList)
        {
            int newTenantId;
            long newAdminId;

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {

                var connectionstringUnit = await (from org in _organizationRepository.GetAll()
                                                  join constr in _connectionStringRepository.GetAll() on org.ConnectionStringId equals
                                                      constr.Id where org.Id == organizationId
                                                  select constr).FirstOrDefaultAsync();

                //Create tenant
                var tenant = new Tenant(tenancyName, name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    ConnectionString = ReferenceEquals(connectionstringUnit, null)?null: connectionstringUnit.ConnectionString,
                    OrganizationUnitId = organizationId
                };

                CheckErrors(await CreateAsync(tenant));
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetTenantId(tenant.Id))
                {
                    //Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Create admin user for the tenant
                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = User.CreateRandomPassword();
                    }

                    var adminUser = User.CreateTenantAdminUser(tenant.Id, adminEmailAddress, adminPassword);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = isActive;

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _demoDataBuilder.BuildForAsync(tenant);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;

                }
                if (sourcetenantId.HasValue)
                    await CloneTenantData(newTenantId, sourcetenantId, entityList);
                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(new UserIdentifier(newTenantId, newAdminId));
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                    await uow.CompleteAsync();
                }
            }

            return newTenantId;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
        /// <summary>
        /// Chaching Method
        /// Tenant based seeding
        /// </summary>
        /// <param name="newTenantId"></param>
        /// <returns></returns>
        public async Task CustomTenantSeeding(int newTenantId)
        {
            //Currency Seeding
            if (await _typeOfCurrencyUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var currencyList = await _typeOfCurrencyUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                currencyList.ForEach(u => u.TenantId = newTenantId);
                foreach (var currency in currencyList)
                {
                    await _typeOfCurrencyUnit.InsertAsync(currency);
                }
            }

            //Account Seeding
            if (await _typeOfAccountUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var typeOfAccountList = await _typeOfAccountUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                typeOfAccountList.ForEach(u => u.TenantId = newTenantId);
                foreach (var typeOfAccount in typeOfAccountList)
                {
                    await _typeOfAccountUnit.InsertAsync(typeOfAccount);
                }
            }

            //Region Seeding
            if (await _regionUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var regionList = await _regionUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                regionList.ForEach(u => u.TenantId = newTenantId);
                foreach (var region in regionList)
                {
                    await _regionUnit.InsertAsync(region);
                }
            }

            //Country Seeding
            if (await _countryUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var countryList = await _countryUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                countryList.ForEach(u => u.TenantId = newTenantId);
                foreach (var country in countryList)
                {
                    await _countryUnit.InsertAsync(country);
                }
            }
        }


        /// <summary>
        /// Sumit Method to add Users when map the user to Tenant
        /// Adding the newly created User in some other Tenanats of the same Organization
        /// </summary>
        /// <param name="newTenantId"></param>
        /// <param name="sourceTenantId"></param>
        /// <param name="entityList"></param>
        /// <returns></returns>
        private async Task CloneTenantData(int newTenantId, int? sourceTenantId, List<string> entityList)
        {
            if (!ReferenceEquals(entityList, null))
            {
                foreach (string entityName in entityList)
                {
                    switch (entityName)
                    {
                        case "Vendors":
                            {
                                List<VendorUnit> vendorList = null;
                                //Get vendor data from Tenanat of Source
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _vendorUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        vendorList = await _vendorUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                //Inserting Vendor Data in DestinationTenant
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    Mapper.CreateMap<VendorUnit, VendorUnit>()
                                        .ForMember(u => u.Id, ap => ap.Ignore());
                                    var vendorUnit = new VendorUnit();
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        foreach (var vendor in vendorList)
                                        {
                                            vendor.MapTo(vendorUnit);
                                            vendorUnit.TenantId = newTenantId;
                                            vendorUnit.CreatorUserId = null;
                                            vendorUnit.CreatorUserId = null;
                                            await _vendorUnit.InsertAsync(vendorUnit);
                                        }
                                    }
                                }
                                break;
                            }
                        case "Users":
                            {
                                List<User> userList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _userUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        userList = await _userUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    Mapper.CreateMap<User, User>()
                                      .ForMember(u => u.Id, ap => ap.Ignore());
                                    var userUnit = new User();
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        foreach (var user in userList)
                                        {
                                            if (user.Name != StaticUsers.UserName)
                                            {
                                                user.MapTo(userUnit);
                                                userUnit.TenantId = newTenantId;
                                                userUnit.CreatorUser = null ;
                                                userUnit.CreatorUserId = null;
                                                userUnit.LastModifierUser = null;
                                                userUnit.LastModifierUserId = null;
                                                await _userUnit.InsertAsync(userUnit);
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case "Roles":
                            {
                                List<Role> rollList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _roleUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        rollList = await _roleUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<Role, Role>()
                                  .ForMember(u => u.Id, ap => ap.Ignore());
                                        var roleUnit = new Role();

                                        foreach (var role in rollList)
                                        {
                                            if (role.Name != StaticRoleNames.Tenants.Admin && role.Name != StaticRoleNames.Tenants.User)
                                            {
                                                
                                                role.MapTo(roleUnit);
                                                roleUnit.TenantId = newTenantId;
                                                roleUnit.CreatorUser = null;
                                                roleUnit.CreatorUserId = null;
                                                roleUnit.LastModifierUserId = null;
                                                roleUnit.LastModifierUser = null;
                                                await _roleUnit.InsertAsync(roleUnit);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "ChartofAccounts":
                            {
                                List<CoaUnit> coaList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _coaUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        coaList = await _coaUnit.GetAll().Where(u => u.TenantId == sourceTenantId && u.IsCorporate).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<CoaUnit, CoaUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var coaUnit = new CoaUnit();
                                        foreach (var coa in coaList)
                                        {
                                            
                                            coa.MapTo(coaUnit);
                                            coa.TenantId = newTenantId;
                                            coa.CreatorUserId = null;
                                            await _coaUnit.InsertAsync(coaUnit);
                                        }
                                    }
                                }
                                break;
                            }

                        case "ProjectChartofAccounts":
                            {
                                List<CoaUnit> coaList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _coaUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        coaList = await _coaUnit.GetAll().Where(u => u.TenantId == sourceTenantId && !u.IsCorporate).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<CoaUnit, CoaUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var coaUnit = new CoaUnit();
                                        foreach (var coa in coaList)
                                        {
                                            coa.MapTo(coaUnit);
                                            coaUnit.TenantId = newTenantId;
                                            coaUnit.CreatorUserId = null;
                                            await _coaUnit.InsertAsync(coaUnit);
                                        }
                                    }
                                }
                                break;
                            }

                        case "Employees":
                            {
                                List<EmployeeUnit> empList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _employeeUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        empList = await _employeeUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                    {
                                        Mapper.CreateMap<EmployeeUnit, EmployeeUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var employeeUnit = new EmployeeUnit();
                                        if (!ReferenceEquals(entityList, null))
                                        {
                                            foreach (var emp in empList)
                                            {
                                                emp.MapTo(employeeUnit);
                                                employeeUnit.TenantId = newTenantId;
                                                employeeUnit.CreatorUserId = null;
                                                await _employeeUnit.InsertAsync(employeeUnit);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "Customers":
                            {
                                List<CustomerUnit> customerList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _customerUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        customerList = await _customerUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<CustomerUnit, CustomerUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var customerUnit = new CustomerUnit();
                                        foreach (var customer in customerList)
                                        {
                                           customer.MapTo(customerUnit);
                                            customerUnit.TenantId = newTenantId;
                                            customerUnit.CreatorUserId = null;
                                            await _customerUnit.InsertAsync(customerUnit);
                                        }
                                    }
                                }
                                break;
                            }
                    }

                }
            }
        }
    }
}
