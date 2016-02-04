using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ISalesRepUnitAppService : IApplicationService
    {
        Task<SalesRepUnitDto> CreateSalesRepUnit(CreateSalesRepUnitInput input);

        Task<ListResultOutput<SalesRepUnitDto>> GetSalesRepUnits(long? organizationUnitId);

        Task<SalesRepUnitDto> UpdateSalesRepUnit(UpdateSalesRepUnitInput input);
        Task DeleteSalesRepUnit(IdInput input);
    }
}