using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ILocationSetUnitAppService : IApplicationService
    {
        Task<LocationSetUnitDto> CreateLocationSetUnit(CreateLocationSetUnitInput input);   

        Task<LocationSetUnitDto> UpdateLocationSetUnit(UpdateLocationSetUnitInput input);
        Task DeleteLocationSetUnit(IdInput input);
        Task<LocationSetUnitDto> GetLocationSetUnitsById(IdInput input);
    }
}
