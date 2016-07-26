using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNet.Identity;
using CAPS.CORPACCOUNTING.Authorization.Dto;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users.Exporting;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Notifications;
using CAPS.CORPACCOUNTING.Authorization.Roles.Dto;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.MultiTenancy;


namespace CAPS.CORPACCOUNTING.Authorization.Users
{
    [AbpAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UserAppService : CORPACCOUNTINGAppServiceBase, IUserAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IUserEmailer _userEmailer;
        private readonly IUserListExcelExporter _userListExcelExporter;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Role> _rolesUnitRepository;
        private readonly IRepository<User, long> _userUnitRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IUserLinkManager _userLinkManager;
        private readonly IRepository<UserAccount, long> _userAccountUnitRepository;
        private readonly IRepository<Tenant> _tenantUnitRepository;
        private readonly TenantManager _tenantManager;
        private readonly RoleAppService _roleAppService;
        private List<User> userList = new List<User>();
        private User sourceuser;


        public UserAppService(
            RoleManager roleManager,
            IUserEmailer userEmailer,
            IUserListExcelExporter userListExcelExporter,
            INotificationSubscriptionManager notificationSubscriptionManager,

            IAppNotifier appNotifier, IUnitOfWorkManager unitOfWorkManager, IRepository<Role> roletUnitRepository,
            IRepository<UserRole, long> userRoleRepository, IRepository<User, long> userUnitRepository, IUserLinkManager userLinkManager,
            IRepository<UserAccount, long> userAccountUnitRepository, IRepository<Tenant> tenantUnitRepository, TenantManager tenantManager,
            RoleAppService roleAppService)

        {
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _userListExcelExporter = userListExcelExporter;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
            _unitOfWorkManager = unitOfWorkManager;
            _rolesUnitRepository = roletUnitRepository;
            _userRoleRepository = userRoleRepository;
            _userUnitRepository = userUnitRepository;
            _userLinkManager = userLinkManager;
            _userAccountUnitRepository = userAccountUnitRepository;
            _tenantUnitRepository = tenantUnitRepository;
            _tenantManager = tenantManager;
            _roleAppService = roleAppService;
        }

        public async Task<PagedResultOutput<UserListDto>> GetUsers(GetUsersInput input)
        {
            var query = UserManager.Users
                .Include(u => u.Roles)
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter) ||
                        u.EmailAddress.Contains(input.Filter)
                );

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var userListDtos = users.MapTo<List<UserListDto>>();
            await FillRoleNames(userListDtos);

            return new PagedResultOutput<UserListDto>(
                userCount,
                userListDtos
                );
        }

        public async Task<FileDto> GetUsersToExcel()
        {
            var users = await UserManager.Users.Include(u => u.Roles).ToListAsync();
            var userListDtos = users.MapTo<List<UserListDto>>();
            await FillRoleNames(userListDtos);

            return _userListExcelExporter.ExportToFile(userListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdInput<long> input)
        {
            //Getting all available roles
            var userRoleDtos = (await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync());

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };

            if (!input.Id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto { IsActive = true, ShouldChangePasswordOnNextLogin = true };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);

                output.User = user.MapTo<UserEditDto>();
                output.ProfilePictureId = user.ProfilePictureId;

                foreach (var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(input.Id.Value, userRoleDto.RoleName);
                }
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IdInput<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);

            return new GetUserPermissionsForEditOutput
            {
                Permissions = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task ResetUserSpecificPermissions(IdInput<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            await UserManager.ResetAllPermissionsAsync(user);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var grantedPermissions = PermissionManager.GetPermissionsFromNamesByValidating(input.GrantedPermissionNames);
            await UserManager.SetGrantedPermissionsAsync(user, grantedPermissions);
        }

        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue && input.User.Id != 0)
            {
                await UpdateUserAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }

        [UnitOfWork(IsDisabled = true)]
        public async Task CreateOrUpdateUserUnit(CreateOrUpdateUserInput input)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {

                if (input.User.Id.HasValue && input.User.Id != 0)
                {
                    await UpdateUserUnitAsync(input);
                }
                else
                {
                    await CreateUserUnitAsync(input);
                }
                await uow.CompleteAsync();
            }

            using (var uow = UnitOfWorkManager.Begin())
            {
                using (_unitOfWorkManager.Current.SetTenantId(null))
                {
                    foreach (var userUnit in userList)
                    {
                        await _userLinkManager.Link(sourceuser, userUnit);
                    }
                }
                await uow.CompleteAsync();
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Delete)]
        public async Task DeleteUser(IdInput<long> input)
        {
            if (input.Id == AbpSession.GetUserId())
            {
                throw new UserFriendlyException(L("YouCanNotDeleteOwnAccount"));
            }

            var user = await UserManager.GetUserByIdAsync(input.Id);
            CheckErrors(await UserManager.DeleteAsync(user));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Edit)]
        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "input.User.Id should be set.");

            var user = await UserManager.FindByIdAsync(input.User.Id.Value);

            //Update user properties
            input.User.MapTo(user); //Passwords is not mapped (see mapping configuration)

            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));

            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create)]
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            var user = input.User.MapTo<User>(); //Passwords is not mapped (see mapping configuration)
            user.TenantId = AbpSession.TenantId;

            //Set password
            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
            }
            else
            {
                input.User.Password = User.CreateRandomPassword();
            }

            user.Password = new PasswordHasher().HashPassword(input.User.Password);
            user.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole { RoleId = role.Id });
            }

            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

            //Notifications
            await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(user);

            //Send activation email
            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
            }
        }


        /// <summary>
        /// This is Sumit Method to Create User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create)]
        protected virtual async Task CreateUserUnitAsync(CreateOrUpdateUserInput input)
        {

            sourceuser = input.User.MapTo<User>(); //Passwords is not mapped (see mapping configuration)
            sourceuser.TenantId = AbpSession.TenantId;

            //Set password
            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
            }
            else
            {
                input.User.Password = User.CreateRandomPassword();
            }

            sourceuser.Password = new PasswordHasher().HashPassword(input.User.Password);
            sourceuser.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;

            //Assign roles
            sourceuser.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                sourceuser.Roles.Add(new UserRole { RoleId = role.Id, TenantId = sourceuser.TenantId });
            }

            CheckErrors(await UserManager.CreateAsync(sourceuser));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

            //Notifications
            await
                _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(sourceuser.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(sourceuser);

            //Send activation email
            if (input.SendActivationEmail)
            {
                sourceuser.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(sourceuser, input.User.Password);
            }
            if (!ReferenceEquals(input.TenantList, null))
                await CreateUserforLinkedTenantAsync(input);

        }

        /// <summary>
        /// Sumit Method to Update Userwith Roles 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task UpdateUserUnitAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "input.User.Id should be set.");

            var user = await UserManager.FindByIdAsync(input.User.Id.Value);
            sourceuser = user;

            //Update user properties
            input.User.MapTo(user); //Passwords is not mapped (see mapping configuration)

            if (!input.User.Password.IsNullOrEmpty())
            {
                CheckErrors(await UserManager.ChangePasswordAsync(user, input.User.Password));
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            //Update roles
            CheckErrors(await UserManager.SetRoles(user, input.AssignedRoleNames));

            if (input.SendActivationEmail)
            {
                user.SetNewEmailConfirmationCode();
                await _userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
            }
            if (!ReferenceEquals(input.TenantList, null))
                await CreateUserforLinkedTenantAsync(input);
        }

        private async Task FillRoleNames(List<UserListDto> userListDtos)
        {
            /* This method is optimized to fill role names to given list. */

            var distinctRoleIds = (
                from userListDto in userListDtos
                from userListRoleDto in userListDto.Roles
                select userListRoleDto.RoleId
                ).Distinct();

            var roleNames = new Dictionary<int, string>();
            foreach (var roleId in distinctRoleIds)
            {
                roleNames[roleId] = (await _roleManager.GetRoleByIdAsync(roleId)).DisplayName;
            }

            foreach (var userListDto in userListDtos)
            {
                foreach (var userListRoleDto in userListDto.Roles)
                {
                    userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                }

                userListDto.Roles = userListDto.Roles.OrderBy(r => r.RoleName).ToList();
            }
        }

        public async Task<ListResultOutput<RoleListDto>> GetRolesByTenant(IdInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(input.Id))
            {
                var roles = await _roleManager.Roles.ToListAsync();
                return new ListResultOutput<RoleListDto>(roles.MapTo<List<RoleListDto>>());
            }
        }
        /// <summary>
        /// Input is TenantId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<TenantwithRoleDto>> GetTenantListofOrganization(IdInput input)
        {
            List<TenantListOutputDto> tenantList;
            List<Role> rollList;
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                //to get the OrganizationId from Tenant
                var tenantUnit = await TenantManager.GetByIdAsync(input.Id);
                tenantList = await (from tenant in TenantManager.Tenants
                                    where tenant.OrganizationUnitId == tenantUnit.OrganizationUnitId && tenant.Id != input.Id
                                    select new TenantListOutputDto { TenantName = tenant.TenancyName, TenantId = tenant.Id }).ToListAsync();

            }
            var strTaxCreditNumber = string.Join(",", tenantList.Select(u => u.TenantId).ToArray());
            using (_unitOfWorkManager.Current.SetTenantId(input.Id))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    //to get the OrganizationId from Tenant
                    rollList = _rolesUnitRepository.GetAll().Where(u => strTaxCreditNumber.Contains(u.TenantId.ToString())).ToList();

                }
            }
            //Get The TenantList with Rols except loginUser By OrganizationId
            var rolewithrenant = (from tenant in tenantList
                                  join role in rollList on tenant.TenantId equals role.TenantId
                                  select
                                      new TenantwithRoleDto
                                      {
                                          TenantId = tenant.TenantId,
                                          RoleId = role.Id,
                                          TenantName = tenant.TenantName,
                                          RoleDisplayName = role.DisplayName
                                      }).ToList();
            return rolewithrenant;
        }

        /// <summary>
        /// Creating  theNew user in other tenants of Organization 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task CreateUserforLinkedTenantAsync(CreateOrUpdateUserInput input)
        {
            foreach (var tenant in input.TenantList)
            {
                User user;
                using (_unitOfWorkManager.Current.SetTenantId(tenant.TenantId))
                {

                    user = input.User.MapTo<User>(); //Passwords is not mapped (see mapping configuration)
                    user.TenantId = tenant.TenantId;

                    //Set password
                    if (!input.User.Password.IsNullOrEmpty())
                    {
                        CheckErrors(await UserManager.PasswordValidator.ValidateAsync(input.User.Password));
                    }
                    else
                    {
                        input.User.Password = User.CreateRandomPassword();
                    }

                    user.Password = new PasswordHasher().HashPassword(input.User.Password);
                    user.ShouldChangePasswordOnNextLogin = input.User.ShouldChangePasswordOnNextLogin;

                    //Checking the User exist 
                    var userUnit =
                        await _userUnitRepository.FirstOrDefaultAsync(p => p.UserName == user.UserName);
                    if (!tenant.IsEmptyRoles)
                    {
                        if (ReferenceEquals(userUnit, null))
                        {
                            //Assign roles
                            user.Roles = new Collection<UserRole>();
                            foreach (var roleId in tenant.RoleIds)
                            {
                                user.Roles.Add(new UserRole { RoleId = roleId, TenantId = tenant.TenantId });
                            }

                            CheckErrors(await UserManager.CreateAsync(user));
                            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.

                            userList.Add(user);
                            //Notifications
                            await
                                _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(
                                    user.ToUserIdentifier());
                            await _appNotifier.WelcomeToTheApplicationAsync(user);

                            //Send activation email
                            if (input.SendActivationEmail)
                            {
                                user.SetNewEmailConfirmationCode();
                                await _userEmailer.SendEmailActivationLinkAsync(user, input.User.Password);
                            }

                        }
                        else
                        {
                            //Update roles
                            CheckErrors(await UserManager.SetRoles(userUnit, tenant.RoleNames));

                            userList.Add(userUnit);
                            await CurrentUnitOfWork.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        CheckErrors(await UserManager.SetRoles(userUnit, tenant.RoleNames));
                    }
                }
            }
        }

        /// <summary>
        /// Get the Permissions for selected roles of a Tenant
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<GetRoleForEditOutput> GetPermissionsForSelectedRole(RoleTenantInput input)
        {
            using (_unitOfWorkManager.Current.SetTenantId(input.TenantId))
            {
                var grantedPermissions = new Permission[0];
                var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
                grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();

                var grantedpermissionList = grantedPermissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName)
                    .Select(c => { c.IsPermissionGranted = true; return c; }).ToList();

                return new GetRoleForEditOutput
                {
                    Permissions = grantedpermissionList
                };
            }
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public async Task<GetUserForEditOutput> GetUserUnitForEdit(NullableIdInput<long> input)
        {
            //Getting all available roles
            var userRoleDtos = (await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                })
                .ToArrayAsync());

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos
            };

            if (!input.Id.HasValue)
            {
                //Creating a new user
                output.User = new UserEditDto { IsActive = true, ShouldChangePasswordOnNextLogin = true };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else
            {
                //Editing an existing user
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);
                output.User = user.MapTo<UserEditDto>();
                output.ProfilePictureId = user.ProfilePictureId;

                foreach (var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(input.Id.Value, userRoleDto.RoleName);
                }
                output.TenantwithRoles = await GetTenantListofOrganizationforEdit(user.UserName);
            }
            return output;
        }

        /// <summary>
        /// Get All Tenants with Roles of a Organization
        /// </summary>
        /// <returns></returns>
        public async Task<List<TenantwithRoleDto>> GetTenantListofOrganizationforEdit(string userName)
        {
            int tenatid = AbpSession.TenantId.Value;
            List<TenantListOutputDto> tenantList;
            List<Role> rollList;
            List<UserRole> assignedUserRoles;
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                //to get the OrganizationId from Tenant
                var tenantUnit = await TenantManager.GetByIdAsync(tenatid);
                //Get the ListofTenants of an Organization
                tenantList = await (from tenant in TenantManager.Tenants
                                    where tenant.OrganizationUnitId == tenantUnit.OrganizationUnitId && tenant.Id != tenatid
                                    select new TenantListOutputDto { TenantName = tenant.TenancyName, TenantId = tenant.Id }).ToListAsync();

            }
            var tenantIds = string.Join(",", tenantList.Select(u => u.TenantId).ToArray());

            using (_unitOfWorkManager.Current.SetTenantId(tenatid))
            {
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    //to get the OrganizationId from Tenant
                    rollList = _rolesUnitRepository.GetAll().Where(u => tenantIds.Contains(u.TenantId.ToString())).ToList();

                    //Get Assigned Roles of a User for all companies in a Organization
                    assignedUserRoles = await (from users in _userUnitRepository.GetAll().Where(p => p.UserName == userName && p.TenantId != tenatid)
                                               join userrole in _userRoleRepository.GetAll() on users.Id equals userrole.UserId
                                               select userrole).ToListAsync();
                }
            }
            //Get The TenantList with Rols except loginUser By OrganizationId
            var rolewithrenant = (from tenant in tenantList
                                  join role in rollList on tenant.TenantId equals role.TenantId
                                  select
                                      new TenantwithRoleDto
                                      {
                                          TenantId = tenant.TenantId,
                                          RoleId = role.Id,
                                          TenantName = tenant.TenantName,
                                          RoleDisplayName = role.DisplayName,
                                          RoleName = role.Name,
                                          IsRoleSelected = assignedUserRoles.Any(p => p.RoleId == role.Id)
                                      }).ToList();
            return rolewithrenant;
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Users_ChangePermissions)]
        public async Task<GetUserPermissionsForEditOutput> GetUserAllPermissionsForEdit(IdInput<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            var permissions = PermissionManager.GetAllPermissions();
            var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(user);
            var permissionList = permissions.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList();
            if (grantedPermissions.Count > 0)
            {
                foreach (var grantedPermission in grantedPermissions)
                {
                    permissionList.Where(w => w.Name == grantedPermission.Name)
                    .ToList()
                    .ForEach(s => s.IsPermissionGranted = true);
                }
            }


            return new GetUserPermissionsForEditOutput
            {
                Permissions = permissionList.MapTo<List<FlatPermissionDto>>().OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }

        public async Task UpdateUserPermissionsUnit(UserPermissionsInput input)
        {
            await _userRoleRepository.DeleteAsync(p => p.UserId == input.UserId);

            CreateOrUpdateRoleInput request = new CreateOrUpdateRoleInput();
            request.Role = input.Role;
            request.GrantedPermissionNames = input.GrantedPermissionNames;

            int roleId = await _roleAppService.CreateRoleForUserAsync(request);

            UserRole userrole = new UserRole
            {
                RoleId = roleId,
                UserId = input.UserId.Value
            };

            await _userRoleRepository.InsertAsync(userrole);
            await _unitOfWorkManager.Current.SaveChangesAsync();

        }

    }
}
