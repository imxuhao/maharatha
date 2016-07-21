using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on VendorPaymentTerms.
    /// </summary>
    public interface IVendorPaymentTermUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the VendorPaymentTerm.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorPaymentTermUnitDto> CreateVendorPaymentTermUnit(CreateVendorPaymentTermUnitInput input);

        /// <summary>
        /// Get the list of all VendorPaymentTerms and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<VendorPaymentTermUnitDto>> GetVendorPaymentTermUnits(SearchInputDto input);

        /// <summary>
        /// Update the VendorPaymentTerm based on VendorPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorPaymentTermUnitDto> UpdateVendorPaymentTermUnit(UpdateVendorPaymentTermUnitInput input);

        /// <summary>
        /// Delete the VendorPaymentTerm based on VendorPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteVendorPaymentTermUnit(IdInput input);

        /// <summary>
        /// Get the VendorPaymentTerm based on VendorPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorPaymentTermUnitDto> GetVendorPayTermUnitsById(IdInput input);

        /// <summary>
        /// Get All Vendor PaymentTerms
        /// </summary>
        /// <returns></returns>
        Task<List<VendorPaymentTermUnitDto>> GetVendorPayTerms();
    }
}