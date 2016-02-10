using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ICustomerPaymentTermUnitAppService : IApplicationService
    {
        Task<CustomerPaymentTermUnitDto> CreateCustomerPaymentTermUnit(CreateCustomerPaymentTermUnitInput input);

        Task<ListResultOutput<CustomerPaymentTermUnitDto>> GetCustomerPaymentTermUnits(GetCustomerPaymentTermInput input);

        Task<CustomerPaymentTermUnitDto> UpdateCustomerPaymentTermUnit(UpdateCustomerPaymentTermUnitInput input);
        Task DeleteCustomerPaymentTermUnit(IdInput input);
        Task<CustomerPaymentTermUnitDto> GetCustomerPayTermUnitsById(IdInput input);
    }
}