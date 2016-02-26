using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on VendorPaymentTerms.
    /// </summary>
    public interface IVendorPaymentTermUnitAppService : IApplicationService
    {
        /// <summary>
        /// This is used to create the VendorPaymentTerm.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorPaymentTermUnitDto> CreateVendorPaymentTermUnit(CreateVendorPaymentTermUnitInput input);

        /// <summary>
        /// This is used to get the list of all VendorPaymentTerms and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<VendorPaymentTermUnitDto>> GetVendorPaymentTermUnits(GetVendorPayTermsInput input);

        /// <summary>
        /// This is used to update the VendorPaymentTerm based on VendorPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorPaymentTermUnitDto> UpdateVendorPaymentTermUnit(UpdateVendorPaymentTermUnitInput input);

        /// <summary>
        /// This is used to delete the VendorPaymentTerm based on VendorPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteVendorPaymentTermUnit(IdInput input);

        /// <summary>
        /// This is used to get the VendorPaymentTerm based on VendorPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorPaymentTermUnitDto> GetVendorPayTermUnitsById(IdInput input);
    }
}