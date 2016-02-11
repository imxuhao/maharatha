using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ISalesRepUnitAppService : IApplicationService
    {
        Task<SalesRepUnitDto> CreateSalesRepUnit(CreateSalesRepUnitInput input);

        Task<PagedResultOutput<SalesRepUnitDto>> GetSalesRepUnits(GetSalesRepInput input);

        Task<SalesRepUnitDto> UpdateSalesRepUnit(UpdateSalesRepUnitInput input);
        Task DeleteSalesRepUnit(IdInput input);
        Task<SalesRepUnitDto> GetSalesRepUnitsById(IdInput input);
    }
}