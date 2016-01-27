using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IVendorUnitAppService : IApplicationService
    {
        Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input);

        Task<ListResultOutput<VendorUnitDto>> GetVendorUnits();

        Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input);
        Task DeleteVendorUnit(IdInput input);
    }
}