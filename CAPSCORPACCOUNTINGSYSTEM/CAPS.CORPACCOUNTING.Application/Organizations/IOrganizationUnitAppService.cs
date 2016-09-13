using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Organizations.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Organizations
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits();

        Task<PagedResultOutput<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

        /// <summary>
        /// Create OrganizationUnits
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);

        /// <summary>
        /// Update OrganizationUnits
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
        /// Creating HostOrganization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateHostOrganizationUnit(CreateHostOrganizationUnitInput input);

        /// <summary>
        /// UpdatingHostOrganization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateHostOrganizationUnit(UpdateHostOrganizationUnitInput input);

        /// <summary>
        /// GetHostOrganization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<HostOrganizationUnitDto>> GetHostOrganizationUnits(SearchInputDto input);
        /// <summary>
        /// Get OrganizationList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetHostOrganizationsList();

        /// <summary>
        /// Get ConnectionStringList
        /// </summary>
        /// <returns></returns>

        Task<List<NameValueDto>> GetConnectionStrings();


        /// <summary>
        /// Get DeleteHostOrganizationUnit
        /// </summary>
        /// <returns></returns>
        Task DeleteHostOrganizationUnit(IdInput<long> input);

        
    }
}
