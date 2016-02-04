using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IVendorPaymentTermUnitAppService : IApplicationService
    {
        Task<VendorPaymentTermUnitDto> CreateVendorPaymentTermUnit(CreateVendorPaymentTermUnitInput input);

        Task<ListResultOutput<VendorPaymentTermUnitDto>> GetVendorPaymentTermUnits(long? organizationUnitId);

        Task<VendorPaymentTermUnitDto> UpdateVendorPaymentTermUnit(UpdateVendorPaymentTermUnitInput input);
        Task DeleteVendorPaymentTermUnit(IdInput input);
    }
}