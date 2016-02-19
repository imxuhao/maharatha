using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IRollupCenterUnitAppService : IApplicationService
    {
        Task<RollupCenterUnitDto> CreateRollupCenterUnit(CreateRollupCenterUnitInput input);

        Task<RollupCenterUnitDto> UpdateRollupCenterUnit(UpdateRollupCenterUnitInput input);
        Task DeleteRollupCenterUnit(IdInput input);
        Task<PagedResultOutput<RollupCenterUnitDto>> GetRollupCenterUnits(GetRollupCenterInput input);
    }
}
