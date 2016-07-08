using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Organizations.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Organizations
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits();

        Task<PagedResultOutput<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);

        Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);

        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteOrganizationUnit(IdInput<long> input);

        Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input);

        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);

        Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input);

        /// <summary>
        /// Get Company List By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetOrganizationsListByUserId(IdInput<long> input);


        /// <summary>
        /// Get Organization List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<OrganizationUnitDto>> GetOrganizationUnits(SearchInputDto input);

        /// <summary>
        /// Update Default Settings of Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAllSettings(OrganizationManagementSettingsEditDto input);

        /// <summary>
        /// Get default Settings of Organization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationManagementSettingsEditDto> GetAllSettings(IdInput<long> input);

        /// <summary>
        /// Creating HostOrganization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateHostOrganizationUnit(CreateOrganizationUnitInput input);

        /// <summary>
        /// UpdatingHostOrganization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateHostOrganizationUnit(UpdateOrganizationUnitInput input);

        /// <summary>
        /// GetHostOrganization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<OrganizationUnitDto>> GetHostOrganizationUnits(SearchInputDto input);
        /// <summary>
        /// Get OrganizationList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetHostOrganizationsList();
    }
}
