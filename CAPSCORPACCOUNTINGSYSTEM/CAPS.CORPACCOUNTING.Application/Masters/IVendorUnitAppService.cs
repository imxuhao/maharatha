using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IVendorUnitAppService : IApplicationService
    {
        Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input);

        Task<ListResultOutput<VendorUnitDto>> GetVendorUnits(long? organizationUnitId);

        Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input);
        Task DeleteVendorUnit(IdInput input);
        Task InsertVendorData(CreateVendorUnitInput input);
        Task UpdatedVendorData(UpdateVendorUnitInput input);
        Task<ListResultOutput<VendorUnitDto>> GetVendorUnitsById(IdInput input);

    }
}