using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.AccountReceivable.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.PurchaseOrders.Dto;

namespace CAPS.CORPACCOUNTING.PurchaseOrders
{

    /// <summary>
    /// 
    /// </summary>
  public interface IPurchaseOrderEntryDocumentAppService : IApplicationService
    {
        /// <summary>
        /// Create Purchase Order Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreatePurchaseOrderEntryDocumentUnit(PurchaseOrderEntryDocumentInputUnit input);

        /// <summary>
        /// Update Purchase Order  Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdatePurchaseOrderEntryDocumentUnit(PurchaseOrderEntryDocumentInputUnit input);

        /// <summary>
        /// Delete Purchase Order  Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePurchaseOrderEntryDocumentUnit(IdInput input);

        /// <summary>
        /// Delete Purchase Order Detail Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePurchaseOrderDetailUnit(IdInput<long> input);

        /// <summary>
        /// Get Purchase Order Entry Document List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<PurchaseOrderEntryDocumentUnitDto>> GetPurchaseOrderEntryDocumentUnits(SearchInputDto input);

        /// <summary>
        ///Get Purchase Order Details by AccountingDocumentId List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<PurchaseOrderDetailUnitDto>> GetPurchaseOrdersByAccountingDocumentId(GetTransactionList input);

        /// <summary>
        /// Get CardHolder Information
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AutoFillDto>> GetCardInfoList(AutoSearchInput input);




    }
}
