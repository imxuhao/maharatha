using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Journals.dto;
using CAPS.CORPACCOUNTING.Journals.Dto;

namespace CAPS.CORPACCOUNTING.Journals
{
    /// <summary>
    ///  Provide all CRUD operations for JournalEntryDocument.
    /// </summary>
    public interface IJournalEntryDocumentAppService : IApplicationService
    {
        /// <summary>
        /// Create Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreateJournalEntryDocumentUnit(JournalEntryDocumentInputUnit input);

        /// <summary>
        /// Update Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateJournalEntryDocumentUnit(JournalEntryDocumentInputUnit input);

        /// <summary>
        /// Delete Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJournalEntryDocumentUnit(IdInput input);

        /// <summary>
        /// Delete Journal Detail Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJournalDetailUnit(IdInput<long> input);

        /// <summary>
        /// Get Journal Entry Document List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JournalEntryDocumentUnitDto>> GetJournalEntryDocumentUnits(SearchInputDto input);




        /// <summary>
        /// Get Journal Details by AccountingDocumentId List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        Task<PagedResultOutput<JournalEntryDetailUnitDto>> GetJournalDetailsByDocumentId(GetTransactionList input);

        /// <summary>
        /// Get Journal Details by AccountingDocumentId List.
        /// (This method is created for WEB UI
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JournalCreditEntryDetailUnitDto>> GetJournalDetailsByAccountingDocumentId(GetTransactionList input);

        /// <summary>
        /// Get JournalTypeList
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetJournalTypeList();
    }
}
