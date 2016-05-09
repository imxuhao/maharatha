using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Vendor.
    /// </summary>
    public interface IVendorUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the Vendor.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input);

        /// <summary>
        /// Update the Vendor based on VendorId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input);

        /// <summary>
        /// Delete the Vendor based on VendorId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteVendorUnit(IdInput input);

        /// <summary>
        /// Get the Vendor based on VendorId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<VendorUnitDto> GetVendorUnitsById(IdInput input);

        /// <summary>
        /// Get the list of all vendors and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<VendorUnitDto>> GetVendorUnits(SearchInputDto input);


        /// <summary>
        /// Get TypeofPaymentMethod 
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofPaymentMethodList();
      

        /// <summary>
        /// Get Typeof1099T4
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeof1099T4List();


        /// <summary>
        /// Get TypeofVendor
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofVendorList();


        /// <summary>
        /// Get TypeofAddress
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofAddressList();


        /// <summary>
        /// Get TypeofObject
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofObjectList();


        /// <summary>
        /// Get TypeOfTax
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeOfTaxList();


        /// <summary>
        /// Get PaymentTerms
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetPaymentTermsList();

        /// <summary>
        /// Get Country list
        /// </summary>
        /// <returns></returns>
         Task<List<NameValueDto>> GetCountryList();

        /// <summary>
        /// Get Country list
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetRegionList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetAccountsList(AutoSearchInput search);

    }
}