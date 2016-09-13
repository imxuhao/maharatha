using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Payables.Dto;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.PettyCash.Dto;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    /// <summary>
    /// Provide all CRUD operations on InvoiceEntryDocument.
    /// </summary>
    public interface IPettyCashEntryDocumentAppService : IApplicationService
    {
        /// <summary>
        /// Create the APHeaderTransaction.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreatePettyCashEntryDocumentUnit(PettyCashEntryDocumentInput input);

        /// <summary>
        /// Update the APHeaderTransaction
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdatePettyCashEntryDocumentUnit(PettyCashEntryDocumentInput input);

        /// <summary>
        /// Delete ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePettyCashEntryDocumentUnit(IdInput<long> input);

        /// <summary>
        /// Get all APHeaderTransactions with Paging and Sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<PettyCashEntryDocumentUnitDto>> GetPettyCashEntryDocumentUnits(SearchInputDto input);


        /// <summary>
        /// GetInvoiceEntryDocumentDetails By AccountingDocumentID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task<PagedResultOutput<PettyCashEntryDocumentDetailDto>> GetPettyCashEntryDocumentDetailsByAccountingDocumentId(GetTransactionList input);


    }
}
