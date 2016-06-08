using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Payables.Dto;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Payables
{
    /// <summary>
    /// Provide all CRUD operations on InvoiceEntryDocument.
    /// </summary>
    public interface IAPHeaderTransactionsAppService : IApplicationService
    {
        /// <summary>
        /// Create the APHeaderTransaction.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreateAPHeaderTransactionUnit(APHeaderTransactionsInputUnit input);

        /// <summary>
        /// Update the APHeaderTransaction
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAPHeaderTransactionUnit(APHeaderTransactionsInputUnit input);

        /// <summary>
        /// Delete ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAPHeaderTransactionUnit(IdInput<long> input);

        /// <summary>
        /// Get all APHeaderTransactions with Paging and Sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<APHeaderTransactionsUnitDto>> GetAPHeaderTransactionUnits(SearchInputDto input);

        /// <summary>
        /// GetInvoiceEntryDocumentDetails By AccountingDocumentID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<InvoiceEntryDocumentDetailUnitDto>> GetAPHeaderTransactionDetailUnitsByAccountingDocumentId(GetTransactionList input);
    }
}
