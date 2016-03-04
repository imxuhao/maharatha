using CAPS.CORPACCOUNTING.Authorization.Users;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Uow;
using Abp.Domain.Repositories;

namespace CAPS.CORPACCOUNTING.Journals
{
    [AbpAuthorize] //This is to ensure only logged in user has access to this module.
    public class JournalEntryDocumentAppService : CORPACCOUNTINGServiceBase, IJournalEntryDocumentAppService
    {
        private readonly JournalEntryDocumentUnitManager _journalEntryDocumentUnitManager;
        private readonly IRepository<JournalEntryDocumentUnit> _journalEntryDocumentUnitRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JournalEntryDocumentAppService(JournalEntryDocumentUnitManager journalEntryDocumentUnitManager, IRepository<JournalEntryDocumentUnit> journalEntryDocumentUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _journalEntryDocumentUnitManager = journalEntryDocumentUnitManager;
            _journalEntryDocumentUnitRepository = journalEntryDocumentUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Create Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateJournalEntryDocumentUnit(CreateJournalEntryDocumentInputUnit input)
        {
            var journalEntryDocumentUnit = new JournalEntryDocumentUnit(batchid: input.BatchId, isreversingentry: input.IsReversingEntry, dateofreversal: input.DateOfReversal, isrecurringentry: input.IsRecurringEntry,
                datetorecur: input.DateToRecur, finaldate: input.FinalDate, lastpostdate: input.LastPostDate, batchinfo: input.BatchInfo, isbatchremoved: input.IsBatchRemoved,
                     description: input.Description, typeofaccountingdocumentid: input.TypeOfAccountingDocumentId, typeofobjectid: input.TypeOfObjectId, recurdocid: input.RecurDocId, reversedocid: input.ReverseDocId,
                     documentdate: input.DocumentDate, transactiondate: input.TransactionDate, dateposted: input.DatePosted, originaldocumentid: input.OriginalDocumentId,
                     controltotal: input.ControlTotal, documentreference: input.DocumentReference, voucherreference: input.VoucherReference, typeofcurrencyid: input.TypeOfCurrencyId,
                     currencyadjustmentid: input.CurrencyAdjustmentId, postbatchdescription: input.PostBatchDescription, isposted: input.IsPosted, isautoposted: input.IsAutoPosted,
                     ischanged: input.IsChanged, postedbyuserid: input.PostedByUserId, bankreccontrolid: input.BankRecControlId, isselected: input.IsSelected,
                     isactive: input.IsActive, isapproved: input.IsApproved, typeofinactivestatusid: input.TypeOfInactiveStatusId, isbankrecomitted: input.IsBankRecOmitted,
                     isictjournal: input.IsICTJournal, ictcompanyid: input.ICTCompanyId, ictaccountingdocumentid: input.ICTAccountingDocumentId, currencyoverriderate: input.CurrencyOverrideRate,
                     functionalcurrencycontroltotal: input.FunctionalCurrencyControlTotal, typeofcurrencyrateid: input.TypeOfCurrencyRateId, memoline: input.MemoLine, is13period: input.Is13Period,
                    homecurrencyamount: input.HomeCurrencyAmount, customforexrate: input.CustomForexRate, isposubmitforapproval: input.IsPOSubmitForApproval, iscpastran: input.IsCPASTran,
                    cpasprojcloseid: input.CPASProjCloseId, cpasprojid: input.CPASProjId, organizationunitid: input.OrganizationUnitId);

            await _journalEntryDocumentUnitManager.CreateAsync(journalEntryDocumentUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        ///  Update Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateJournalEntryDocumentUnit(UpdateJournalEntryDocumentInputUnit input)
        {
            var journalEntryDocumentUnit = await _journalEntryDocumentUnitRepository.GetAsync(input.AHTID);

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
            journalEntryDocumentUnit.TransactionDate = input.TransactionDate;
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
    }
}
