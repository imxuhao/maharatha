using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;

namespace CAPS.CORPACCOUNTING.Accounting
{

    /// <summary>
    /// 
    /// </summary>
   public interface IListAppService : IApplicationService
    {

        /// <summary>
        /// Get Jobs or Divisions List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DivisionCacheItem>> GetJobOrDivisionList(AutoSearchInput input);

        /// <summary>
        /// Get accounts List based on JobId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AccountCacheItem>> GetAccountsList(AutoSearchInput input);

        /// <summary>
        ///  Get SubAccounts List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<SubAccountCacheItem>> GetSubAccountList(AutoSearchInput input);
        /// <summary>
        ///  Get VendorList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<VendorCacheItem>> GetVendorList(AutoSearchInput input);

       /// <summary>
       ///  Get TaxCreditList
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
       Task<List<AutoFillDto>> GetTaxCreditList(AutoSearchInput input);
        /// <summary>
        /// GetTypeof1099List
        /// </summary>
        /// <returns></returns>
       List<NameValueDto> GetTypeof1099T4List();

        /// <summary>
        /// GetLocationList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetLocationList(AutoSearchInput input);

       /// <summary>
       /// Get CheckGrouopList
       /// </summary>
       /// <returns></returns>
       List<NameValueDto> GetCheckGroupList();

        /// <summary>
        /// Get TypeOfInvoiceList
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeOfInvoiceList();

        /// <summary>
        /// Get PettyCashAccountList
        /// </summary>
        /// <returns></returns>
        Task<List<AutoFillDto>> GetPettyCashAccountList(AutoSearchInput input);

       /// <summary>
       /// Get VendorPaymentTermsList
       /// </summary>
       /// <returns></returns>
        Task<List<NameValueDto>> GetVendorPaymentTermsList(AutoSearchInput input);

       /// <summary>
       /// Get GetBankAccountList
       /// </summary>
       /// <returns></returns>
       Task<List<BankAccountCacheItem>> GetBankAccountList(AutoSearchInput input);



        /// <summary>
        /// Get Vendors, Accounts, Job or Division, Tax Credit and Sub Account List values by Type
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetListByNames(NameValueInputList input);

       /// <summary>
       ///Get PurchaseOrders
       /// </summary>
       /// <returns></returns>
       Task<List<PurchaseOrderEntyDocumnetwithDetailOutputDto>> GetPurchaseOrderList(GetPurchaseOrderInput input);

        /// <summary>
        /// get card types
        /// </summary>
        /// <returns></returns>
         List<NameValueDto> GetCRBankTypeList();

        /// <summary>
        /// get Credit Card File Upload Types
        /// </summary>
        /// <returns></returns>

        Task<List<NameValueDto>> FileUploadCRDRList(AutoSearchInput input);


        /// <summary>
        /// Get accounts List By Classification Type
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AccountCacheItem>> GetAccountsListByClassification(AccountSearchInput input);


        /// <summary>
        /// Get Vendors list by Classification Type
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<VendorCacheItem>> GetVendorsListByClassification(VendorSearchInput input);

        /// <summary>
        /// Get BatchList By TypeOfBatch
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetBatchListByType(BatchSearchInput input);

        /// <summary>
        /// Get Jobs lIst by status
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<DivisionCacheItem>> GetJobListByStatus(AutoSearchInput input);

    }
}
