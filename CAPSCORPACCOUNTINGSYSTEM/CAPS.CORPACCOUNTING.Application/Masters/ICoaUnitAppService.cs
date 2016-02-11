using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ICoaUnitAppService : IApplicationService
    {
        Task<CoaUnitDto> CreateCoaUnit(CreateCoaUnitInput input);

        Task<PagedResultOutput<CoaUnitDto>> GetCoaUnits(GetCoaInput input);

        Task<CoaUnitDto> UpdateCoaUnit(UpdateCoaUnitInput input);
        Task DeleteCoaUnit(IdInput input);
    }
}