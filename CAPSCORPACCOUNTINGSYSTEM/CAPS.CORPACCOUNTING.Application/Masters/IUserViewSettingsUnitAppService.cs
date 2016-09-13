using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    /// This service will provide all CRUD operations on UserViewSettings.
    /// </summary>
    public interface IUserViewSettingsUnitAppService : IApplicationService
    {

        /// <summary>
        /// Create the UserViewSettings.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserViewSettingsUnitDto> CreateUserViewSettingsUnit(CreateUserViewSettingsUnitInput input);

        /// <summary>
        ///  Update the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserViewSettingsUnitDto> UpdateUserViewSettingsUnit(UpdateUserViewSettingsUnitInput input);

        /// <summary>
        /// Delete the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteUserViewSettingsUnit(IdInput input);

        /// <summary>
        /// Get the list of all UserViewSettings by user Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<UserViewSettingsUnitDto>> GetUserViewSettingsUnitsByUserId(SearchInputDto input);

    }
}
