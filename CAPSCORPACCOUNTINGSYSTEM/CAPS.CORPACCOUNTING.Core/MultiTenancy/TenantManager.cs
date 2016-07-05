using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp;
using Abp.Application.Features;
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
        IRepository<CountryUnit> countryUnit) :
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

        public async Task<int> CreateWithAdminUserAsync(string tenancyName, string name, string adminPassword, string adminEmailAddress, string connectionString, bool isActive, int? editionId, bool shouldChangePasswordOnNextLogin, bool sendActivationEmail, long? organizationId)
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
                    ConnectionString =
                        connectionString.IsNullOrWhiteSpace()
                            ? null
                            : SimpleStringCipher.Instance.Encrypt(connectionString),
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
    }
}
