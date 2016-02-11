using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ICustomerUnitAppService : IApplicationService
    {
        Task<CustomerUnitDto> CreateCustomerUnit(CreateCustomerUnitInput input);

        Task<PagedResultOutput<CustomerUnitDto>> GetCustomerUnits(GetCustomerInput input);

        Task<CustomerUnitDto> UpdateCustomerUnit(UpdateCustomerUnitInput input);
        Task DeleteCustomerUnit(IdInput input);
        Task InsertCustomerData(CreateCustomerUnitInput input);
        Task UpdatedCustomerData(UpdateCustomerUnitInput input);
        Task<CustomerUnitDto> GetCustomerUnitsById(IdInput input);
    }
}