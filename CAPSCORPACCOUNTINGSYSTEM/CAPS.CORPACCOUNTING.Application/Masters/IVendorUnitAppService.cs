using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IVendorUnitAppService : IApplicationService
    {
        Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input);
        Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input);
        Task DeleteVendorUnit(IdInput input);       
        Task<VendorUnitDto> GetVendorUnitsById(IdInput input);
        Task<PagedResultOutput<VendorUnitDto>> GetVendorUnits(GetVendorInput input);
    }
}