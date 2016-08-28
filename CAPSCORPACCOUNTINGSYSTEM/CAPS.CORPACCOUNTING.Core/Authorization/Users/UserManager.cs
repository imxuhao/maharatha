using System;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Threading;
using Abp.Zero.Configuration;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.MultiTenancy;

namespace CAPS.CORPACCOUNTING.Authorization.Users
{
    /// <summary>
    /// User manager.
    /// Used to implement domain logic for users.
    /// Extends <see cref="AbpUserManager{TTenant,TRole,TUser}"/>.
    /// </summary>
    public class UserManager : AbpUserManager<Tenant, Role, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
            : base(
                userStore,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                userLoginAttemptRepository)
        {

        }


        public async Task<User> GetUserAsync(UserIdentifier userIdentifier)
        {
            using (_unitOfWorkManager.Begin())
            {
                using (_unitOfWorkManager.Current.SetTenantId(userIdentifier.TenantId))
                {
                    var user = await FindByIdAsync(userIdentifier.UserId);
                    if (user == null)
                    {
                        throw new ApplicationException("There is no user: " + userIdentifier);
                    }

                    return user;
                }
            }
        }

        public User GetUser(UserIdentifier userIdentifier)
        {
            return AsyncHelper.RunSync(() => GetUserAsync(userIdentifier));
        }
    }
}