using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Threading.Tasks;

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
        [UnitOfWork]
        Task CreateJournalEntryDocumentUnit(CreateJournalEntryDocumentInputUnit input);

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
    }
}
