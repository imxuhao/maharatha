using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Authorization.Roles.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users.Dto;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<PagedResultOutput<UserListDto>> GetUsers(GetUsersInput input);

        Task<FileDto> GetUsersToExcel();

        Task<GetUserForEditOutput> GetUserForEdit(NullableIdInput<long> input);

        Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(IdInput<long> input);

        Task ResetUserSpecificPermissions(IdInput<long> input);

        Task UpdateUserPermissions(UpdateUserPermissionsInput input);

        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

        Task DeleteUser(IdInput<long> input);

        /// <summary>
        /// Get Roles byTenantId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<RoleListDto>> GetRolesByTenant(IdInput input);

        /// <summary>
        /// Get the TenantList of Organization By OrganizationId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<TenantwithRoleDto>> GetTenantListofOrganization(IdInput input);

        /// <summary>
        /// Create or Update User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateUserUnit(CreateOrUpdateUserInput input);

        /// <summary>
        /// Get Permissions for selected Role
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetPermissionsForSelectedRole(RoleTenantInput input);


        Task<GetUserForEditOutput> GetUserUnitForEdit(NullableIdInput<long> input);

        Task<GetUserPermissionsForEditOutput> GetUserAllPermissionsForEdit(IdInput<long> input);

        Task UpdateUserPermissionsUnit(UserPermissionsInput input);

        /// <summary>
        /// Get the list of all Users and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<UserListDto>> GetUserUnits(SearchInputDto input);

    }
}