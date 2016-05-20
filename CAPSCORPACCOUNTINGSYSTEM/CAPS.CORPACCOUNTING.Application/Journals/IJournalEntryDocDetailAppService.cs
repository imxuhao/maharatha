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
    public interface IJournalEntryDocDetailAppService : IApplicationService
    {
        /// <summary>
        /// Create Journal Entry Document Detail.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateJournalEntryDocDetailUnit(List<CreateJournalEntryDocDetailInputUnit> input);

        /// <summary>
        /// Update Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateJournalEntryDocumentUnit(List<UpdateJournalEntryDocDetailInputUnit> input);

        /// <summary>
        /// Delete Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJournalEntryDocDetailUnit(IdInput<long> input);


        /// <summary>
        /// Get Journal Entry Document List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JournalEntryDocDetailUnitDto>> GetJournalEntryDocDetailsByAccountingDocId(GetTransactionList input);

        /// <summary>
        /// Get Jobs or Divisions List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetJobOrDivisionList(AutoSearchInput input);

        /// <summary>
        /// Get accounts List based on JobId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GeAccountsList(AutoSearchInput input);

        /// <summary>
        ///  Get SubAccounts List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task<List<NameValueDto>> GetSubAccountList(AutoSearchInput input);
    }
}
