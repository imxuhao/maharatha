using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
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
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using LinqKit;

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
        private readonly IPermissionManager _permissionManager;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionSettingRepository;
        private readonly IRepository<UserRole, long> _userRolRepository;


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
        IRepository<CoaUnit> coaUnit, IRepository<EmployeeUnit> employeeUnit, IRepository<CustomerUnit> customerUnit, IRepository<ConnectionStringUnit> connectionStringRepository, IRepository<OrganizationExtended, long> organizationRepository, IPermissionManager permissionManager, IRepository<RolePermissionSetting, long> rolePermissionSettingRepository, IRepository<UserRole, long> userRolRepository) :
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
            _permissionManager = permissionManager;
            _rolePermissionSettingRepository = rolePermissionSettingRepository;
            _userRolRepository = userRolRepository;
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
            string strConnectionstring;
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {

                var connectionstringUnit =
                    await (from org in _organizationRepository.GetAll()
                           join constr in _connectionStringRepository.GetAll() on org.ConnectionStringId equals
                               constr.Id
                           where org.Id == organizationId
                           select constr).FirstOrDefaultAsync();

                strConnectionstring = connectionstringUnit.ConnectionString;
                await uow.CompleteAsync();
            }

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {



                //Create tenant
                var tenant = new Tenant(tenancyName, name)
                {
                    IsActive = isActive,
                    EditionId = editionId,
                    ConnectionString = strConnectionstring,
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
                {
                    await CloneTenantData(newTenantId, sourcetenantId, entityList);
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                }
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
                bool isRoleselected = false;
                List<Role> roles = null;
                foreach (string entityName in entityList.OrderBy(p => p))
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
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        foreach (var vendor in vendorList)
                                        {
                                            var vendorUnit = new VendorUnit();
                                            vendor.MapTo(vendorUnit);
                                            vendorUnit.TenantId = newTenantId;
                                            vendorUnit.CreatorUserId = null;
                                            vendorUnit.CreatorUserId = null;
                                            vendorUnit.LastModifierUserId = null;
                                            await _vendorUnit.InsertAsync(vendorUnit);
                                        }
                                    }
                                }
                                break;
                            }
                        case "Users":
                            {
                                var userRoleList = new Dictionary<User, List<Role>>();
                                Dictionary<Role, IReadOnlyList<Permission>> rolePermissionList = new Dictionary<Role, IReadOnlyList<Permission>>();
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _userUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        //getting the userd except default user(AppSupport)
                                        var userList = await _userManager.Users.Include(p => p.Roles)
                                            .Where(u => u.TenantId == sourceTenantId && u.UserName != StaticUsers.UserName).ToListAsync();
                                        //getting all Rles
                                        var allRoleList = await _roleUnit.GetAll().ToListAsync();
                                        foreach (var user in userList)
                                        {
                                            var roleNumbers = string.Join(",", user.Roles.Select(p => p.RoleId).ToArray());
                                            var rolelist = allRoleList
                                                .Where(p => roleNumbers.Contains(p.Id.ToString())).ToList();
                                            userRoleList.Add(user, rolelist);
                                            var rolepermission = await GetRoleswithPermissions(rolelist);
                                            if (!rolePermissionList.ContainsKey(rolepermission.FirstOrDefault().Key))
                                                rolePermissionList.Add(rolepermission.FirstOrDefault().Key, rolepermission.FirstOrDefault().Value);
                                        }

                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        List<Role> newRoles = null;
                                        if (!isRoleselected)
                                            newRoles = await InsertRoles(rolePermissionList, newTenantId);
                                        else
                                            newRoles = roles;

                                        foreach (var user in userRoleList)
                                        {
                                            var userUnit = new User();
                                            user.Key.MapTo(userUnit);
                                            userUnit.TenantId = newTenantId;
                                            userUnit.CreatorUser = null;
                                            userUnit.CreatorUserId = null;
                                            userUnit.LastModifierUser = null;
                                            userUnit.LastModifierUserId = null;
                                            var userroles = (from p in user.Value
                                                             join newrole in newRoles
                                                                 on p.Name equals newrole.Name
                                                             select newrole).ToList();
                                            userUnit.Roles = null;
                                            long userId = await _userUnit.InsertAndGetIdAsync(userUnit);
                                            foreach (var userRole in userroles)
                                            {
                                                await _userRolRepository.InsertAsync(new UserRole
                                                {
                                                    RoleId = userRole.Id,
                                                    UserId = userId,
                                                    TenantId = newTenantId
                                                });
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "Roles":
                            {
                                isRoleselected = true;
                                Dictionary<Role, IReadOnlyList<Permission>> rolePermissionList = new Dictionary<Role, IReadOnlyList<Permission>>();
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _roleUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        //getting the roles except DefaultRoles(User,AppSupport)
                                        var rollList = await _roleUnit.GetAll().Where(u => u.TenantId == sourceTenantId
                                        && u.Name != StaticRoleNames.Tenants.Admin &&
                                                u.Name != StaticRoleNames.Tenants.User).ToListAsync();

                                        rolePermissionList = await GetRoleswithPermissions(rollList);
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        roles = await InsertRoles(rolePermissionList, newTenantId);
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
                                        //getting the coaList
                                        coaList = await _coaUnit.GetAll().Where(u => u.TenantId == sourceTenantId && u.IsCorporate).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        foreach (var coa in coaList)
                                        {
                                            var coaUnit = new CoaUnit();
                                            coa.MapTo(coaUnit);
                                            coa.TenantId = newTenantId;
                                            coa.CreatorUserId = null;
                                            coa.LastModifierUserId = null;
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
                                        foreach (var coa in coaList)
                                        {
                                            var coaUnit = new CoaUnit();
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
                                        var employeeUnit = new EmployeeUnit();
                                        if (!ReferenceEquals(entityList, null))
                                        {
                                            foreach (var emp in empList)
                                            {
                                                emp.MapTo(employeeUnit);
                                                employeeUnit.TenantId = newTenantId;
                                                employeeUnit.CreatorUserId = null;
                                                employeeUnit.LastModifierUserId = null;
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
                                        var customerUnit = new CustomerUnit();
                                        foreach (var customer in customerList)
                                        {
                                            customer.MapTo(customerUnit);
                                            customerUnit.TenantId = newTenantId;
                                            customerUnit.CreatorUserId = null;
                                            customerUnit.LastModifierUserId = null;
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
        /// <summary>
        /// Inserting RolePermissions
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionlist"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        private async Task InsertPermission(int roleId, IReadOnlyList<Permission> permissionlist, int tenantId)
        {
            foreach (var permission in permissionlist)
            {
                await _rolePermissionSettingRepository.InsertAsync(new RolePermissionSetting
                {
                    RoleId = roleId,
                    Name = permission.Name,
                    IsGranted = permission.IsGrantedByDefault,
                    TenantId = tenantId
                });
            }

        }

        private async Task<Dictionary<Role, IReadOnlyList<Permission>>> GetRoleswithPermissions(List<Role> rollList)
        {
            var rolePermissionList = new Dictionary<Role, IReadOnlyList<Permission>>();
            var allPermissions = await _rolePermissionSettingRepository.GetAll().ToListAsync();
            foreach (var role in rollList)
            {
                var grantedPermision = allPermissions.Where(p => p.RoleId == role.Id).Select(p =>
                new Permission(p.Name, isGrantedByDefault: p.IsGranted)).ToList();
                rolePermissionList.Add(role, grantedPermision);
            }
            return rolePermissionList;
        }

        /// <summary>
        /// Inserting Roles
        /// </summary>
        /// <param name="rolePermissionList"></param>
        /// <param name="newTenantId"></param>
        /// <returns></returns>
        private async Task<List<Role>> InsertRoles(Dictionary<Role, IReadOnlyList<Permission>> rolePermissionList, int newTenantId)
        {
            List<Role> roles = new List<Role>();
            foreach (var role in rolePermissionList)
            {
                var roleUnit = new Role();
                role.Key.MapTo(roleUnit);
                roleUnit.TenantId = newTenantId;
                roleUnit.CreatorUser = null;
                roleUnit.CreatorUserId = null;
                roleUnit.LastModifierUserId = null;
                roleUnit.LastModifierUser = null;
                roleUnit.Permissions = null;
                int roleId = await _roleUnit.InsertAndGetIdAsync(roleUnit);
                roleUnit.Id = roleId;
                roles.Add(roleUnit);
                await InsertPermission(roleUnit.Id, role.Value, newTenantId);
            }
            return roles;

        }

    }
}
