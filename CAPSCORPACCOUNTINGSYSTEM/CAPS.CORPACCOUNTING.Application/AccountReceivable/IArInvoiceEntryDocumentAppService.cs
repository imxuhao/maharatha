using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.AccountReceivable.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{

    /// <summary>
    /// 
    /// </summary>
  public interface IArInvoiceEntryDocumentAppService : IApplicationService
    {
        /// <summary>
        /// Create ArInvoice Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreateArInvoiceEntryDocumentUnit(ArInvoiceEntryDocumentInputUnit input);

        /// <summary>
        /// Update ArInvoice Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateArInvoiceEntryDocumentUnit(ArInvoiceEntryDocumentInputUnit input);

        /// <summary>
        /// Delete ArInvoice Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteArInvoiceEntryDocumentUnit(IdInput input);

        /// <summary>
        /// Delete ArInvoice Detail Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteArInvoiceDetailUnit(IdInput<long> input);

        /// <summary>
        /// Get ArInvoice Entry Document List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<ArInvoiceEntryDocumentUnitDto>> GetArInvoiceEntryDocumentUnits(SearchInputDto input);
        
        /// <summary>
        ///Get ArInvoice Details by AccountingDocumentId List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<ArInvoiceEntryDetailUnitDto>> GetArInvoiceByAccountingDocumentId(GetTransactionList input);
        
        /// <summary>
        /// Get GetArBillingTypeList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetBillingTypeList(AutoSearchInput input);

        /// <summary>
        /// Get CustomerPaymentTermsList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetCustomerPaymentTermsList(AutoSearchInput input);

        /// <summary>
        /// Get Sales Rep List
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetSalesRepList();


    }
}
