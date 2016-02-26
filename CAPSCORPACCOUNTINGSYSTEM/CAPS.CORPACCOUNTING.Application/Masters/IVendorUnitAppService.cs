using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Vendor.
    /// </summary>
    public interface IVendorUnitAppService : IApplicationService
    {
        /// <summary>
        /// This is used to create the Vendor.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input);

        /// <summary>
        /// This is used to update the Vendor based on VendorId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input);

        /// <summary>
        /// This is used to delete the Vendor based on VendorId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteVendorUnit(IdInput input);

        /// <summary>
        /// This is used to get the Vendor based on VendorId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorUnitDto> GetVendorUnitsById(IdInput input);

        /// <summary>
        /// This is used to get the list of all vendors and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<VendorUnitDto>> GetVendorUnits(GetVendorInput input);
    }
}