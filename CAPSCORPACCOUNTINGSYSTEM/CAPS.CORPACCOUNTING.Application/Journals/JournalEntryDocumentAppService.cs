using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using CAPS.CORPACCOUNTING.Authorization.Users;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Auditing;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Dependency;

namespace CAPS.CORPACCOUNTING.Journals
{
    [AbpAuthorize] //This is to ensure only logged in user has access to this module.
    public class JournalEntryDocumentAppService : CORPACCOUNTINGServiceBase, IJournalEntryDocumentAppService, ITransientDependency
    {
        private readonly JournalEntryDocumentUnitManager _journalEntryDocumentUnitManager;
        private readonly IRepository<JournalEntryDocumentUnit, long> _journalEntryDocumentUnitRepository;
        private readonly IRepository<BatchUnit> _batchUnitRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<User, long> _userRepository;
        private IdOutputDto<long> _response = null;
        private readonly CustomAppSession _CustomAppSession;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalEntryDocumentUnitManager"></param>
        /// <param name="journalEntryDocumentUnitRepository"></param>
        /// <param name="userManager"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="batchUnitRepository"></param>
        public JournalEntryDocumentAppService(JournalEntryDocumentUnitManager journalEntryDocumentUnitManager,
            IRepository<JournalEntryDocumentUnit, long> journalEntryDocumentUnitRepository,
            IRepository<User, long> userRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager, IRepository<BatchUnit> batchUnitRepository,
            CustomAppSession CustomAppSession
            )
        {
            _journalEntryDocumentUnitManager = journalEntryDocumentUnitManager;
            _journalEntryDocumentUnitRepository = journalEntryDocumentUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _batchUnitRepository = batchUnitRepository;
            _userRepository = userRepository;
            _CustomAppSession = CustomAppSession;
        }

        /// <summary>
        /// Create Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IdOutputDto<long>> CreateJournalEntryDocumentUnit(CreateJournalEntryDocumentInputUnit input)
        {
            var journalEntryDocumentUnit = input.MapTo<JournalEntryDocumentUnit>();
            _response = new IdOutputDto<long>();
            _response.Id = await _journalEntryDocumentUnitManager.CreateAsync(journalEntryDocumentUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return _response;
        }

        /// <summary>
        ///  Update Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateJournalEntryDocumentUnit(UpdateJournalEntryDocumentInputUnit input)
        {
            var journalEntryDocumentUnit = await _journalEntryDocumentUnitRepository.GetAsync(input.AccountingDocumentId);

            #region Setting the values to be updated
            journalEntryDocumentUnit.BatchId = input.BatchId;
            journalEntryDocumentUnit.IsReversingEntry = input.IsReversingEntry;
            journalEntryDocumentUnit.DateOfReversal = input.DateOfReversal;
            journalEntryDocumentUnit.IsRecurringEntry = input.IsRecurringEntry;
            journalEntryDocumentUnit.DateToRecur = input.DateToRecur;
            journalEntryDocumentUnit.FinalDate = input.FinalDate;
            journalEntryDocumentUnit.LastPostDate = input.LastPostDate;
            journalEntryDocumentUnit.BatchInfo = input.BatchInfo;
            journalEntryDocumentUnit.IsBatchRemoved = input.IsBatchRemoved;
            journalEntryDocumentUnit.Description = input.Description;
            journalEntryDocumentUnit.TypeOfAccountingDocumentId = input.TypeOfAccountingDocumentId;
            journalEntryDocumentUnit.TypeOfObjectId = input.TypeOfObjectId;
            journalEntryDocumentUnit.RecurDocId = input.RecurDocId;
            journalEntryDocumentUnit.ReverseDocId = input.ReverseDocId;
            journalEntryDocumentUnit.DocumentDate = input.DocumentDate;
            journalEntryDocumentUnit.TransactionDate = input.TransactionDate.Value;
            journalEntryDocumentUnit.DatePosted = input.DatePosted;
            journalEntryDocumentUnit.OriginalDocumentId = input.OriginalDocumentId;
            journalEntryDocumentUnit.ControlTotal = input.ControlTotal;
            journalEntryDocumentUnit.DocumentReference = input.DocumentReference;
            journalEntryDocumentUnit.VoucherReference = input.VoucherReference;
            journalEntryDocumentUnit.TypeOfCurrencyId = input.TypeOfCurrencyId;
            journalEntryDocumentUnit.CurrencyAdjustmentId = input.CurrencyAdjustmentId;
            journalEntryDocumentUnit.PostBatchDescription = input.PostBatchDescription;
            journalEntryDocumentUnit.IsPosted = input.IsPosted;
            journalEntryDocumentUnit.IsAutoPosted = input.IsAutoPosted;
            journalEntryDocumentUnit.IsChanged = input.IsChanged;
            journalEntryDocumentUnit.PostedByUserId = input.PostedByUserId;
            journalEntryDocumentUnit.BankRecControlId = input.BankRecControlId;
            journalEntryDocumentUnit.IsSelected = input.IsSelected;
            journalEntryDocumentUnit.IsActive = input.IsActive;
            journalEntryDocumentUnit.IsApproved = input.IsApproved;
            journalEntryDocumentUnit.TypeOfInactiveStatusId = input.TypeOfInactiveStatusId;
            journalEntryDocumentUnit.IsBankRecOmitted = input.IsBankRecOmitted;
            journalEntryDocumentUnit.IsICTJournal = input.IsICTJournal;
            journalEntryDocumentUnit.ICTCompanyId = input.ICTCompanyId;
            journalEntryDocumentUnit.ICTAccountingDocumentId = input.ICTAccountingDocumentId;
            journalEntryDocumentUnit.CurrencyOverrideRate = input.CurrencyOverrideRate;
            journalEntryDocumentUnit.FunctionalCurrencyControlTotal = input.FunctionalCurrencyControlTotal;
            journalEntryDocumentUnit.TypeOfCurrencyRateId = input.TypeOfCurrencyRateId;
            journalEntryDocumentUnit.MemoLine = input.MemoLine;
            journalEntryDocumentUnit.Is13Period = input.Is13Period;
            journalEntryDocumentUnit.HomeCurrencyAmount = input.HomeCurrencyAmount;
            journalEntryDocumentUnit.CustomForexRate = input.CustomForexRate;
            journalEntryDocumentUnit.IsPOSubmitForApproval = input.IsPOSubmitForApproval;
            journalEntryDocumentUnit.IsCPASTran = input.IsCPASTran;
            journalEntryDocumentUnit.CPASProjCloseId = input.CPASProjCloseId;
            journalEntryDocumentUnit.CPASProjId = input.CPASProjId;
            journalEntryDocumentUnit.OrganizationUnitId = input.OrganizationUnitId;
            journalEntryDocumentUnit.JournalTypeId = input.JournalTypeId;
            #endregion

            await _journalEntryDocumentUnitManager.UpdateAsync(journalEntryDocumentUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteJournalEntryDocumentUnit(IdInput input)
        {
            await _journalEntryDocumentUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        [UnitOfWork]
        public async Task<PagedResultOutput<JournalEntryDocumentUnitDto>> GetJournalEntryDocumentUnits(SearchInputDto input)
        {



            var query = from journals in _journalEntryDocumentUnitRepository.GetAll()
                        join batch in _batchUnitRepository.GetAll() on journals.BatchId equals batch.Id
                        into batchunit
                        from batchunits in batchunit.DefaultIfEmpty()
                        select new { Journals = journals, BatchName = batchunits.Description };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.Journals.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Journals.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<JournalEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Journals.MapTo<JournalEntryDocumentUnitDto>();
                dto.BatchName = item.BatchName;
                dto.JournalType = item.Journals.JournalTypeId.ToDisplayName();
                dto.AccountingDocumentId = item.Journals.Id;
                return dto;
            }).ToList());

        }

        /// <summary>
        /// Get JournalTypeList
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetJournalTypeList()
        {
            return EnumList.GetJournalTypeList();
        }
    }
}
