using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;

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
        Task<IdOutputDto<long>> CreateJournalEntryDocumentUnit(CreateJournalEntryDocumentInputUnit input);

        /// <summary>
        /// Update Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateJournalEntryDocumentUnit(UpdateJournalEntryDocumentInputUnit input);

        /// <summary>
        /// Delete Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJournalEntryDocumentUnit(IdInput input);


        /// <summary>
        /// Get Journal Entry Document List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JournalEntryDocumentUnitDto>> GetJournalEntryDocumentUnits(SearchInputDto input);

        /// <summary>
        /// Get JournalTypeList
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetJournalTypeList();
    }
}
