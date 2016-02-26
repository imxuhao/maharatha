using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on RollupCenter.
    /// </summary>
    public interface IRollupCenterUnitAppService : IApplicationService
    {
        /// <summary>
        /// This is used to create the RollupCenter.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RollupCenterUnitDto> CreateRollupCenterUnit(CreateRollupCenterUnitInput input);

        /// <summary>
        /// This is used to update the RollupCenter based on RollupCenterId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RollupCenterUnitDto> UpdateRollupCenterUnit(UpdateRollupCenterUnitInput input);

        /// <summary>
        /// This is used to delete the RollupCenter based on RollupCenterId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteRollupCenterUnit(IdInput input);

        /// <summary>
        /// This is used to get the list of all RollupCenters and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<RollupCenterUnitDto>> GetRollupCenterUnits(GetRollupCenterInput input);

        /// <summary>
        /// This is used to get the RollupCenter based on RollupCenterId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RollupCenterUnitDto> GetRollupCenterUnitById(IdInput input);
    }
}
