using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Journals.dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Common;
using System;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.UI;
using Abp.Domain.Uow;
using AutoMapper;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class JournalEntryDocDetailAppService : CORPACCOUNTINGServiceBase, IJournalEntryDocDetailAppService
    {
        private readonly JournalEntryDocumentDetailUnitManager _journalEntryDocumentDetailUnitManager;
        private readonly IRepository<JournalEntryDocumentDetailUnit, long> _journalEntryDocumentDetailUnitRepository;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<CoaUnit, int> _coaUnitRepository;
        private readonly IRepository<VendorUnit, int> _vendorUnitRepository;
        private readonly IRepository<TaxRebateUnit, int> _taxRebateUnitRepository;
        private readonly ICacheManager _cacheManager;
        private readonly CustomAppSession _customAppSession;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;

        public JournalEntryDocDetailAppService(
            JournalEntryDocumentDetailUnitManager journalEntryDocumentDetailUnitManager,
            IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitRepository,
            IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<CoaUnit, int> coaUnitRepository,
            IRepository<VendorUnit, int> vendorUnitRepository, CustomAppSession customAppSession,
            IRepository<TaxRebateUnit, int> taxRebateUnitRepository, ICacheManager cacheManager,
             IRepository<TaxCreditUnit> taxCreditUnitRepository
            )

        {
            _journalEntryDocumentDetailUnitManager = journalEntryDocumentDetailUnitManager;
            _journalEntryDocumentDetailUnitRepository = journalEntryDocumentDetailUnitRepository;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
            _coaUnitRepository = coaUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _taxRebateUnitRepository = taxRebateUnitRepository;
            _customAppSession = customAppSession;
            _cacheManager = cacheManager;
            _taxCreditUnitRepository = taxCreditUnitRepository;
        }

        /// <summary>
        /// Update Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task JournalEntryDocumentTransactionUnit(List<JournalEntryDocDetailInputUnit> input)
        {
            //adding journalDocDetails
            foreach (var journaldocDetails in input)
            {
                if (Math.Sign(journaldocDetails.AccountingItemId) == 0)
                {
                    var journalDetails = await MapJournalDetails(journaldocDetails);
                    long creditParentId = 0;
                    foreach (var item in journalDetails)
                    {
                        if (creditParentId != 0)
                            item.AccountingItemOrigId = creditParentId;

                        await _journalEntryDocumentDetailUnitManager.CreateAsync(item);
                        await CurrentUnitOfWork.SaveChangesAsync();
                        creditParentId = item.Id;
                    }

                }//updating journalDocdetails
                else if (Math.Sign(journaldocDetails.AccountingItemId) == 1)
                {
                    long debitParentId = 0;
                    var jouranlDetailsList = await MapJournalDetails(journaldocDetails, isJournalAdd: false);

                    foreach (var jouranlDetail in jouranlDetailsList)
                    {

                        //If AccountingItemId > 0 records will updated
                        //If AccountingItemId < 0 records will deleted
                        //Otherwise journal Details Added.

                        if (jouranlDetail.Id > 0)
                        {
                            var journalEntryDocDetailUnit = await _journalEntryDocumentDetailUnitRepository.GetAsync(jouranlDetail.Id);

                            Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                                    .ForMember(u => u.Id, ap => ap.Ignore())
                                    .ForMember(u => u.TenantId, ap => ap.Ignore());
                            Mapper.Map(jouranlDetail, journalEntryDocDetailUnit);

                            if (debitParentId != 0)
                                journalEntryDocDetailUnit.AccountingItemOrigId = debitParentId;

                            await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalEntryDocDetailUnit);

                            await CurrentUnitOfWork.SaveChangesAsync();
                        }
                        else
                        {
                            var journalEntryDocDetailUnit = jouranlDetail.MapTo<JournalEntryDocumentDetailUnit>();
                            await _journalEntryDocumentDetailUnitManager.CreateAsync(journalEntryDocDetailUnit);
                            await CurrentUnitOfWork.SaveChangesAsync();
                            debitParentId = journalEntryDocDetailUnit.Id;
                        }
                    }
                }//delete JournalDocDetails 
                else
                {

                    IdInput<long> idInput = new IdInput<long>() { Id = (journaldocDetails.AccountingItemId * (-1)) };
                    var journalDetail = await _journalEntryDocumentDetailUnitRepository.GetAsync(idInput.Id);
                    if (!ReferenceEquals(journalDetail, null))
                    {
                        await _journalEntryDocumentDetailUnitManager.DeleteAsync(new IdInput<long>() { Id = journalDetail.Id });
                        await _journalEntryDocumentDetailUnitManager.DeleteAsync(idInput);

                        var creditJournal = await _journalEntryDocumentDetailUnitRepository.FirstOrDefaultAsync(u => u.AccountingItemOrigId == journalDetail.Id);
                        if (!ReferenceEquals(creditJournal, null))
                        {
                            await _journalEntryDocumentDetailUnitManager.DeleteAsync(new IdInput<long>() { Id = creditJournal.Id });
                            await _journalEntryDocumentDetailUnitManager.DeleteAsync(idInput);
                        }
                    }

                }
            }
        }




        /// <summary>
        /// Delete Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteJournalEntryDocDetailUnit(IdInput<long> input)
        {
            await _journalEntryDocumentDetailUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }


        /// <summary>
        /// Get Journal Entry Document List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<JournalEntryDocDetailUnitDto>> GetJournalEntryDocDetailsByAccountingDocId(GetTransactionList input)
        {
            var query = from journals in _journalEntryDocumentDetailUnitRepository.GetAll()
                        join Job in _jobUnitRepository.GetAll() on journals.JobId equals Job.Id into Job
                        from Jobs in Job.DefaultIfEmpty()
                        join Line in _accountUnitRepository.GetAll() on journals.AccountId equals Line.Id into Line
                        from Lines in Line.DefaultIfEmpty()
                        join subAccount1 in _subAccountUnitRepository.GetAll() on journals.SubAccountId1 equals subAccount1.Id
                        into subAccount1
                        from subAccounts1 in subAccount1.DefaultIfEmpty()
                        join subAccount2 in _subAccountUnitRepository.GetAll() on journals.SubAccountId2 equals subAccount2.Id
                        into subAccount2
                        from subAccounts2 in subAccount2.DefaultIfEmpty()
                        join subAccount3 in _subAccountUnitRepository.GetAll() on journals.SubAccountId3 equals subAccount3.Id
                        into subAccount3
                        from subAccounts3 in subAccount3.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on journals.VendorId equals vendor.Id into vendor
                        from vendors in vendor.DefaultIfEmpty()
                        join taxCredit in _taxCreditUnitRepository.GetAll() on journals.TaxRebateId equals taxCredit.Id into
                            taxCredit
                        from taxCredits in taxCredit.DefaultIfEmpty()
                        select new
                        {
                            JournalDetails = journals,
                            JobDesc = Jobs.JobNumber,
                            accountDesc = Lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            subAccount2 = subAccounts2.Description,
                            subAccount3 = subAccounts3.Description,
                            vendor = vendors.LastName,
                            taxCredit = taxCredits.Number
                        };


            query = query.Where(p => p.JournalDetails.AccountingDocumentId.Value == input.AccountingDocumentId)
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.JournalDetails.OrganizationUnitId == input.OrganizationUnitId);

            var results = await query
                .AsNoTracking()
                .ToListAsync();

            List<JournalEntryDocDetailUnitDto> mapResult = results.Select(item =>
            {
                var dto = item.JournalDetails.MapTo<JournalEntryDocDetailUnitDto>();
                dto.AccountingItemId = item.JournalDetails.Id;
                dto.JobDesc = item.JobDesc;
                dto.AccountDesc = item.accountDesc;
                dto.SubAccount1Desc = item.subAccount1;
                dto.SubAccount2Desc = item.subAccount2;
                dto.SubAccount3Desc = item.subAccount3;
                dto.VendorName = item.vendor;
                dto.TaxRebateDesc = item.taxCredit;
                return dto;
            }).ToList();

            var creditMapResult = MapJournalsDetailsOutPutDto(mapResult.OrderByDescending(u => u.Amount).ToList());

            return new PagedResultOutput<JournalEntryDocDetailUnitDto>(creditMapResult.Count, creditMapResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalEntryDocDetail"></param>
        /// <param name="isJournalAdd"></param>
        /// <returns></returns>
        private async Task<List<JournalEntryDocumentDetailUnit>> MapJournalDetails(JournalEntryDocDetailInputUnit journalEntryDocDetail, bool isJournalAdd = true)
        {
            List<JournalEntryDocumentDetailUnit> journalEntryDetailUnitList = new List<JournalEntryDocumentDetailUnit>();
            JournalEntryDocumentDetailUnit debitJournalEntryDetailUnit = null;
            JournalEntryDocumentDetailUnit creditjournalEntryDetailUnit = null;
            bool isParentDelete = false;

            //Insert New Journal Detail Entry
            if (isJournalAdd)
            {
                //check amount is +/- IVE
                if (journalEntryDocDetail.JobId != 0 && journalEntryDocDetail.JobId != 0)
                {
                    debitJournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                    debitJournalEntryDetailUnit.Amount = Math.Abs(journalEntryDocDetail.Amount.Value);
                    journalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                }
                if (journalEntryDocDetail.CreditJobId != 0 && journalEntryDocDetail.CreditAccountId != 0)
                {
                    creditjournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                    creditjournalEntryDetailUnit = CreditJournalDetailMapping(journalEntryDocDetail, creditjournalEntryDetailUnit);
                    creditjournalEntryDetailUnit.AccountingItemOrigId = null;
                    journalEntryDetailUnitList.Add(creditjournalEntryDetailUnit);
                }
            }//Update Journal Detail Entry
            else
            {
                var journalDetailItem = await _journalEntryDocumentDetailUnitRepository.GetAsync(journalEntryDocDetail.AccountingItemId);

                //check amount is +/- IVE
                if (!ReferenceEquals(journalDetailItem, null) && Math.Sign(journalDetailItem.Amount.Value) == 1)
                {
                    if (journalEntryDocDetail.JobId != 0 && journalEntryDocDetail.AccountId != 0)
                    {
                        debitJournalEntryDetailUnit = new JournalEntryDocumentDetailUnit();
                        debitJournalEntryDetailUnit.Id = journalDetailItem.Id;

                        Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                                      .ForMember(u => u.Id, ap => ap.Ignore())
                                      .ForMember(u => u.TenantId, ap => ap.Ignore());
                        Mapper.Map(journalEntryDocDetail, journalDetailItem);

                        journalDetailItem.Amount = Math.Abs(journalEntryDocDetail.Amount.Value);
                        journalDetailItem.AccountingDocumentId = journalEntryDocDetail.AccountingDocumentId;
                        debitJournalEntryDetailUnit = journalDetailItem;

                        if (journalEntryDocDetail.CreditJobId == 0 && journalEntryDocDetail.CreditAccountId == 0)
                            debitJournalEntryDetailUnit.AccountingItemOrigId = null;

                        journalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                    }

                    //delete Debit Journal Entry Detail
                    if (journalEntryDocDetail.JobId == 0 && journalEntryDocDetail.AccountId == 0
                        && journalEntryDocDetail.AccountingItemId != 0 && !ValidateJobAndAccount(journalEntryDocDetail)
                          && journalEntryDocDetail.CreditJobId != 0 && journalEntryDocDetail.CreditAccountId != 0
                        )
                    {
                        isParentDelete = true;
                        await _journalEntryDocumentDetailUnitRepository.DeleteAsync(journalDetailItem.Id);
                    }

                    //get credit information on accountItemOrgId
                    var creditJournalDetailItem = await _journalEntryDocumentDetailUnitRepository.FirstOrDefaultAsync(u => u.AccountingItemOrigId == journalDetailItem.Id);
                    if (!ReferenceEquals(creditJournalDetailItem, null))
                    {
                        long creditParentId = creditJournalDetailItem.AccountingItemOrigId.Value;
                        if (journalEntryDocDetail.CreditJobId != 0 && journalEntryDocDetail.CreditAccountId != 0)
                        {
                            creditjournalEntryDetailUnit = new JournalEntryDocumentDetailUnit();

                            Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                                    .ForMember(u => u.Id, ap => ap.Ignore())
                                    .ForMember(u => u.TenantId, ap => ap.Ignore());
                            Mapper.Map(journalEntryDocDetail, creditJournalDetailItem);

                            creditjournalEntryDetailUnit = CreditJournalDetailMapping(journalEntryDocDetail, creditJournalDetailItem);
                            if (!isParentDelete)
                                creditjournalEntryDetailUnit.AccountingItemOrigId = creditParentId;
                            else
                                creditjournalEntryDetailUnit.AccountingItemOrigId = null;

                            journalEntryDetailUnitList.Add(creditjournalEntryDetailUnit);
                        }

                        //delete Credit Journal Entry Detail
                        if (journalEntryDocDetail.CreditJobId == 0 && journalEntryDocDetail.CreditAccountId == 0
                            && creditJournalDetailItem.Id != 0 && !ValidateJobAndAccount(journalEntryDocDetail, isDebit: false)
                            && journalEntryDocDetail.JobId != 0 && journalEntryDocDetail.AccountId != 0)
                            await _journalEntryDocumentDetailUnitRepository.DeleteAsync(creditJournalDetailItem.Id);
                    }
                    else if (journalEntryDocDetail.CreditJobId != 0 && journalEntryDocDetail.CreditAccountId != 0)
                    {
                        creditjournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                        creditjournalEntryDetailUnit = CreditJournalDetailMapping(journalEntryDocDetail, creditjournalEntryDetailUnit);
                        journalEntryDetailUnitList.Add(creditjournalEntryDetailUnit);
                    }
                }
                if (!ReferenceEquals(journalDetailItem, null) && Math.Sign(journalDetailItem.Amount.Value) == -1)
                {
                    if (journalEntryDocDetail.JobId != 0 && journalEntryDocDetail.AccountId != 0)
                    {
                        debitJournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                        debitJournalEntryDetailUnit.AccountingDocumentId = journalDetailItem.AccountingDocumentId;
                        debitJournalEntryDetailUnit.Amount = Math.Abs(journalEntryDocDetail.Amount.Value);
                        journalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                    }
                    if (journalEntryDocDetail.CreditJobId != 0 && journalEntryDocDetail.CreditAccountId != 0)
                    {
                        creditjournalEntryDetailUnit = new JournalEntryDocumentDetailUnit();
                        Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                               .ForMember(u => u.Id, ap => ap.Ignore())
                               .ForMember(u => u.TenantId, ap => ap.Ignore());
                        Mapper.Map(journalEntryDocDetail, journalDetailItem);
                        creditjournalEntryDetailUnit = CreditJournalDetailMapping(journalEntryDocDetail, journalDetailItem);
                        journalEntryDetailUnitList.Add(creditjournalEntryDetailUnit);
                    }
                    //delete Credit Journal Entry Detail
                    if (journalEntryDocDetail.CreditJobId == 0 && journalEntryDocDetail.CreditAccountId == 0 && journalDetailItem.Id != 0 && !ValidateJobAndAccount(journalEntryDocDetail, isDebit: false))
                        await _journalEntryDocumentDetailUnitRepository.DeleteAsync(journalDetailItem.Id);
                }
            }
            return journalEntryDetailUnitList;
        }

        private JournalEntryDocumentDetailUnit CreditJournalDetailMapping(JournalEntryDocDetailInputUnit journalEntryDocDetail, JournalEntryDocumentDetailUnit creditjournalEntryDetailUnit)
        {
            creditjournalEntryDetailUnit.JobId = journalEntryDocDetail.CreditJobId;
            creditjournalEntryDetailUnit.AccountId = journalEntryDocDetail.CreditAccountId;
            creditjournalEntryDetailUnit.SubAccountId1 = journalEntryDocDetail.CreditSubAccountId1;
            creditjournalEntryDetailUnit.SubAccountId2 = journalEntryDocDetail.CreditSubAccountId2;
            creditjournalEntryDetailUnit.SubAccountId3 = journalEntryDocDetail.CreditSubAccountId3;
            creditjournalEntryDetailUnit.SubAccountId4 = journalEntryDocDetail.CreditSubAccountId4;
            creditjournalEntryDetailUnit.SubAccountId5 = journalEntryDocDetail.CreditSubAccountId5;
            creditjournalEntryDetailUnit.SubAccountId6 = journalEntryDocDetail.CreditSubAccountId6;
            creditjournalEntryDetailUnit.SubAccountId7 = journalEntryDocDetail.CreditSubAccountId7;
            creditjournalEntryDetailUnit.SubAccountId8 = journalEntryDocDetail.CreditSubAccountId8;
            creditjournalEntryDetailUnit.SubAccountId9 = journalEntryDocDetail.CreditSubAccountId9;
            creditjournalEntryDetailUnit.SubAccountId10 = journalEntryDocDetail.CreditSubAccountId10;
            creditjournalEntryDetailUnit.AccountingDocumentId = journalEntryDocDetail.AccountingDocumentId;
            creditjournalEntryDetailUnit.Amount = -Math.Abs(journalEntryDocDetail.Amount.Value);
            creditjournalEntryDetailUnit.AccountingItemOrigId = journalEntryDocDetail.AccountingItemId;
            return creditjournalEntryDetailUnit;
        }


        private List<JournalEntryDocDetailUnitDto> MapJournalsDetailsOutPutDto(List<JournalEntryDocDetailUnitDto> journalEntryDocDetailList)
        {
            List<JournalEntryDocDetailUnitDto> journaList = new List<JournalEntryDocDetailUnitDto>();
            for (int i = 0; i < journalEntryDocDetailList.Count; i++)
            {
                JournalEntryDocDetailUnitDto journalDetail = new JournalEntryDocDetailUnitDto();
                Mapper.CreateMap<JournalEntryDocDetailUnitDto, JournalEntryDocDetailUnitDto>();
                Mapper.Map(journalEntryDocDetailList[i], journalDetail);

                if (Math.Sign(journalEntryDocDetailList[i].Amount.Value) == 1)
                {
                    var creditJournalItem = journalEntryDocDetailList.Find(u => u.AccountingItemOrigId == journalEntryDocDetailList[i].AccountingItemId);
                    if (!ReferenceEquals(creditJournalItem, null))
                    {
                        journalDetail.CreditAccountDesc = creditJournalItem.AccountDesc;
                        journalDetail.CreditAccountId = creditJournalItem.AccountId;
                        journalDetail.CreditJobDesc = creditJournalItem.JobDesc;
                        journalDetail.CreditJobId = creditJournalItem.JobId;
                        journalDetail.CreditSubAccount1Desc = creditJournalItem.SubAccount1Desc;
                        journalDetail.CreditSubAccount10Desc = creditJournalItem.SubAccount10Desc;
                        journalDetail.CreditSubAccount2Desc = creditJournalItem.SubAccount2Desc;
                        journalDetail.CreditSubAccount3Desc = creditJournalItem.SubAccount3Desc;
                        journalDetail.CreditSubAccount4Desc = creditJournalItem.SubAccount4Desc;
                        journalDetail.CreditSubAccount5Desc = creditJournalItem.SubAccount5Desc;
                        journalDetail.CreditSubAccount6Desc = creditJournalItem.SubAccount6Desc;
                        journalDetail.CreditSubAccount7Desc = creditJournalItem.SubAccount7Desc;
                        journalDetail.CreditSubAccount8Desc = creditJournalItem.SubAccount8Desc;
                        journalDetail.CreditSubAccount9Desc = creditJournalItem.SubAccount9Desc;
                        journalDetail.CreditSubAccountId1 = creditJournalItem.SubAccountId1;
                        journalDetail.CreditSubAccountId10 = creditJournalItem.SubAccountId10;
                        journalDetail.CreditSubAccountId2 = creditJournalItem.SubAccountId2;
                        journalDetail.CreditSubAccountId3 = creditJournalItem.SubAccountId3;
                        journalDetail.CreditSubAccountId4 = creditJournalItem.SubAccountId4;
                        journalDetail.CreditSubAccountId5 = creditJournalItem.SubAccountId5;
                        journalDetail.CreditSubAccountId6 = creditJournalItem.SubAccountId6;
                        journalDetail.CreditSubAccountId7 = creditJournalItem.SubAccountId7;
                        journalDetail.CreditSubAccountId8 = creditJournalItem.SubAccountId8;
                        journalDetail.CreditSubAccountId9 = creditJournalItem.SubAccountId9;
                        journalDetail.AccountingItemOrigId = creditJournalItem.AccountingItemOrigId;
                        journalEntryDocDetailList.Remove(creditJournalItem);
                    }
                    journaList.Add(journalDetail);
                }
                else
                if (Math.Sign(journalEntryDocDetailList[i].Amount.Value) == -1)
                {

                    journalDetail.CreditAccountDesc = journalEntryDocDetailList[i].AccountDesc;
                    journalDetail.CreditAccountId = journalEntryDocDetailList[i].AccountId;
                    journalDetail.CreditJobDesc = journalEntryDocDetailList[i].JobDesc;
                    journalDetail.CreditJobId = journalEntryDocDetailList[i].JobId;
                    journalDetail.CreditSubAccount1Desc = journalEntryDocDetailList[i].SubAccount1Desc;
                    journalDetail.CreditSubAccount10Desc = journalEntryDocDetailList[i].SubAccount10Desc;
                    journalDetail.CreditSubAccount2Desc = journalEntryDocDetailList[i].SubAccount2Desc;
                    journalDetail.CreditSubAccount3Desc = journalEntryDocDetailList[i].SubAccount3Desc;
                    journalDetail.CreditSubAccount4Desc = journalEntryDocDetailList[i].SubAccount4Desc;
                    journalDetail.CreditSubAccount5Desc = journalEntryDocDetailList[i].SubAccount5Desc;
                    journalDetail.CreditSubAccount6Desc = journalEntryDocDetailList[i].SubAccount6Desc;
                    journalDetail.CreditSubAccount7Desc = journalEntryDocDetailList[i].SubAccount7Desc;
                    journalDetail.CreditSubAccount8Desc = journalEntryDocDetailList[i].SubAccount8Desc;
                    journalDetail.CreditSubAccount9Desc = journalEntryDocDetailList[i].SubAccount9Desc;
                    journalDetail.CreditSubAccountId1 = journalEntryDocDetailList[i].SubAccountId1;
                    journalDetail.CreditSubAccountId10 = journalEntryDocDetailList[i].SubAccountId10;
                    journalDetail.CreditSubAccountId2 = journalEntryDocDetailList[i].SubAccountId2;
                    journalDetail.CreditSubAccountId3 = journalEntryDocDetailList[i].SubAccountId3;
                    journalDetail.CreditSubAccountId4 = journalEntryDocDetailList[i].SubAccountId4;
                    journalDetail.CreditSubAccountId5 = journalEntryDocDetailList[i].SubAccountId5;
                    journalDetail.CreditSubAccountId6 = journalEntryDocDetailList[i].SubAccountId6;
                    journalDetail.CreditSubAccountId7 = journalEntryDocDetailList[i].SubAccountId7;
                    journalDetail.CreditSubAccountId8 = journalEntryDocDetailList[i].SubAccountId8;
                    journalDetail.CreditSubAccountId9 = journalEntryDocDetailList[i].SubAccountId9;
                    journalDetail.AccountingItemOrigId = journalEntryDocDetailList[i].AccountingItemOrigId;
                    journalDetail.AccountDesc = string.Empty;
                    journalDetail.AccountId = null;
                    journalDetail.JobDesc = string.Empty;
                    journalDetail.JobId = null;
                    journalDetail.SubAccount1Desc = string.Empty;
                    journalDetail.SubAccount10Desc = string.Empty;
                    journalDetail.SubAccount2Desc = string.Empty;
                    journalDetail.SubAccount3Desc = string.Empty;
                    journalDetail.SubAccount4Desc = string.Empty;
                    journalDetail.SubAccount5Desc = string.Empty;
                    journalDetail.SubAccount6Desc = string.Empty;
                    journalDetail.SubAccount7Desc = string.Empty;
                    journalDetail.SubAccount8Desc = string.Empty;
                    journalDetail.SubAccount9Desc = string.Empty;
                    journalDetail.SubAccountId1 = null;
                    journalDetail.SubAccountId10 = null;
                    journalDetail.SubAccountId2 = null;
                    journalDetail.SubAccountId3 = null;
                    journalDetail.SubAccountId4 = null;
                    journalDetail.SubAccountId5 = null;
                    journalDetail.SubAccountId6 = null;
                    journalDetail.SubAccountId7 = null;
                    journalDetail.SubAccountId8 = null;
                    journalDetail.SubAccountId9 = null;
                    journaList.Add(journalDetail);
                }
            }
            return journaList;
        }

        public async Task ValidateJournalDetails(List<JournalEntryDocDetailInputUnit> journalEntryDocDetailList)
        {
            foreach (var journaldocDetails in journalEntryDocDetailList)
            {

                if (Math.Sign(journaldocDetails.AccountingItemId) != -1)
                {

                    if (journaldocDetails.Amount.Value == 0)
                    {
                        throw new UserFriendlyException(L("Amount is Required"));
                    }

                    if (journaldocDetails.JobId == 0 && journaldocDetails.AccountId == 0 && journaldocDetails.CreditJobId == 0 && journaldocDetails.CreditAccountId == 0)
                    {
                        if (string.IsNullOrEmpty(journaldocDetails.JobDesc) && string.IsNullOrEmpty(journaldocDetails.AccountDesc) &&
                            string.IsNullOrEmpty(journaldocDetails.CreditJobDesc) && string.IsNullOrEmpty(journaldocDetails.CreditAccountDesc))
                            throw new UserFriendlyException(L("Either Debit/Credit Job and Account are Required"));
                    }

                    if (journaldocDetails.JobId == 0 && !string.IsNullOrEmpty(journaldocDetails.JobDesc))
                    {
                        journaldocDetails.JobId = await GetJobId(journaldocDetails.JobDesc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.JobId == 0)
                            throw new UserFriendlyException(L("Debit Job is Required"));
                    }

                    if (journaldocDetails.AccountId == 0 && !string.IsNullOrEmpty(journaldocDetails.AccountDesc))
                    {
                        journaldocDetails.AccountId = await GetAccountId(journaldocDetails.AccountDesc, journaldocDetails.JobId, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.AccountId == 0)
                            throw new UserFriendlyException(L("Debit Account is Required"));
                    }

                    if (journaldocDetails.CreditJobId == 0 && !string.IsNullOrEmpty(journaldocDetails.CreditJobDesc))
                    {
                        journaldocDetails.CreditJobId = await GetJobId(journaldocDetails.CreditJobDesc, journaldocDetails.OrganizationUnitId);
                        if (journaldocDetails.CreditJobId == 0)
                            throw new UserFriendlyException(L("Credit Job is Required"));
                    }

                    if (journaldocDetails.CreditAccountId == 0 && !string.IsNullOrEmpty(journaldocDetails.CreditAccountDesc))
                    {
                        journaldocDetails.CreditAccountId = await GetAccountId(journaldocDetails.CreditAccountDesc, journaldocDetails.CreditJobId, journaldocDetails.OrganizationUnitId);
                        if (journaldocDetails.CreditAccountId == 0)
                            throw new UserFriendlyException(L("Credit Account is Required"));
                    }

                    if ((journaldocDetails.JobId != 0 && journaldocDetails.AccountId == 0) ||
                        (journaldocDetails.JobId == 0 && journaldocDetails.AccountId != 0) ||
                             (journaldocDetails.JobId == 0 && journaldocDetails.AccountId == 0 & ValidateJobAndAccount(journaldocDetails)))
                    {
                        throw new UserFriendlyException(L("Debit Job and Account are Required"));
                    }

                    if ((journaldocDetails.CreditJobId != 0 && journaldocDetails.CreditAccountId == 0) ||
                       (journaldocDetails.CreditJobId == 0 && journaldocDetails.CreditAccountId != 0) ||
                            (journaldocDetails.CreditJobId == 0 && journaldocDetails.CreditAccountId == 0 & ValidateJobAndAccount(journaldocDetails, isDebit: false)))
                    {
                        throw new UserFriendlyException(L("Credit Job and Account are Required"));
                    }

                    if (journaldocDetails.SubAccountId1 == null && !string.IsNullOrEmpty(journaldocDetails.SubAccount1Desc))
                    {
                        journaldocDetails.SubAccountId1 = await GetSubAccountId(journaldocDetails.SubAccount1Desc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.SubAccountId1 == 0)
                            throw new UserFriendlyException(L("Debit SubAccount1 is not existed"));
                    }
                    if (journaldocDetails.SubAccountId2 == null && !string.IsNullOrEmpty(journaldocDetails.SubAccount2Desc))
                    {
                        journaldocDetails.SubAccountId2 = await GetSubAccountId(journaldocDetails.SubAccount2Desc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.JobId == 0)
                            throw new UserFriendlyException(L("Debit SubAccount2 is not existed"));
                    }
                    if (journaldocDetails.SubAccountId3 == null && !string.IsNullOrEmpty(journaldocDetails.SubAccount3Desc))
                    {
                        journaldocDetails.SubAccountId3 = await GetSubAccountId(journaldocDetails.SubAccount3Desc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.SubAccountId3 == 0)
                            throw new UserFriendlyException(L("Debit SubAccount3 is not existed"));
                    }
                    if (journaldocDetails.CreditSubAccountId1 == null && !string.IsNullOrEmpty(journaldocDetails.CreditSubAccount1Desc))
                    {
                        journaldocDetails.CreditSubAccountId1 = await GetSubAccountId(journaldocDetails.CreditSubAccount1Desc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.CreditSubAccountId1 == 0)
                            throw new UserFriendlyException(L("Credit SubAccount1 is not existed"));
                    }
                    if (journaldocDetails.CreditSubAccountId2 == null && !string.IsNullOrEmpty(journaldocDetails.CreditSubAccount2Desc))
                    {
                        journaldocDetails.CreditSubAccountId2 = await GetSubAccountId(journaldocDetails.CreditSubAccount2Desc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.CreditSubAccountId2 == 0)
                            throw new UserFriendlyException(L("Credit SubAccount2 is not existed"));
                    }
                    if (journaldocDetails.CreditSubAccountId3 == null && !string.IsNullOrEmpty(journaldocDetails.CreditSubAccount3Desc))
                    {
                        journaldocDetails.CreditSubAccountId3 = await GetSubAccountId(journaldocDetails.CreditSubAccount3Desc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.CreditSubAccountId3 == 0)
                            throw new UserFriendlyException(L("Credit SubAccount3 is not existed"));
                    }
                    if (journaldocDetails.VendorId == null && !string.IsNullOrEmpty(journaldocDetails.VendorName))
                    {
                        journaldocDetails.VendorId = await GetVendorId(journaldocDetails.VendorName, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.VendorId == 0)
                            throw new UserFriendlyException(L("Vendor Name is not existed"));
                    }

                    if (journaldocDetails.TaxRebateId == null && !string.IsNullOrEmpty(journaldocDetails.TaxRebateDesc))
                    {
                        journaldocDetails.TaxRebateId = await TaxCreditId(journaldocDetails.TaxRebateDesc, journaldocDetails.OrganizationUnitId);

                        if (journaldocDetails.TaxRebateId == 0)
                            throw new UserFriendlyException(L("TaxCredit Number is not existed"));
                    }

                }
            }
        }
        private bool ValidateJobAndAccount(JournalEntryDocDetailInputUnit journalDetailUnit, bool isDebit = true)
        {
            bool validateJobandAccount = false;
            if (isDebit)
            {
                if ((journalDetailUnit.SubAccountId1.HasValue && journalDetailUnit.SubAccountId1 != 0) || (journalDetailUnit.SubAccountId2.HasValue && journalDetailUnit.SubAccountId2 != 0) ||
                    (journalDetailUnit.SubAccountId3.HasValue && journalDetailUnit.SubAccountId3 != 0))
                    validateJobandAccount = true;
            }
            else
            {
                if ((journalDetailUnit.CreditSubAccountId1.HasValue && journalDetailUnit.CreditSubAccountId1 != 0) || (journalDetailUnit.CreditSubAccountId2.HasValue && journalDetailUnit.CreditSubAccountId2 != 0) ||
                   (journalDetailUnit.CreditSubAccountId3.HasValue && journalDetailUnit.CreditSubAccountId3 != 0))
                    validateJobandAccount = true;

            }
            return validateJobandAccount;
        }
        private async Task<int> GetJobId(string jobDesc, long organizationUnitId)
        {
            int jobId = 0;
            var job = await (from debitjob in _jobUnitRepository.GetAll()
                                  .Where(p => p.TypeOfJobStatusId != ProjectStatus.Closed)
                                  .Where(p => p.Caption == jobDesc || p.JobNumber == jobDesc)
                                  .WhereIf(!ReferenceEquals(organizationUnitId, null), p => p.OrganizationUnitId == organizationUnitId)
                             select new
                             {
                                 jobId = debitjob.Id
                             }
                                  ).FirstOrDefaultAsync();

            if (!ReferenceEquals(job, null))
                jobId = job.jobId;

            return jobId;
        }

        private async Task<long> GetSubAccountId(string subAccountDesc, long organizationUnitId)
        {
            long subAccountId = 0;
            var subAccount = await (from subaccounts in _subAccountUnitRepository.GetAll()
                           .Where(p => p.Caption == subAccountDesc || p.SubAccountNumber == subAccountDesc)
                           .WhereIf(!ReferenceEquals(organizationUnitId, null), p => p.OrganizationUnitId == organizationUnitId)
                                    select new
                                    {
                                        Id = subaccounts.Id
                                    }).FirstOrDefaultAsync();


            if (!ReferenceEquals(subAccount, null))
                subAccountId = subAccount.Id;

            return subAccountId;
        }


        private async Task<int> GetVendorId(string vendorName, long organizationUnitId)
        {
            int vendorId = 0;
            var vendors = await (from vendor in _vendorUnitRepository.GetAll()
                          .Where(p => p.LastName == vendorName)
                           .WhereIf(!ReferenceEquals(organizationUnitId, null), p => p.OrganizationUnitId == organizationUnitId)
                                 select new
                                 {
                                     vendorId = vendor.Id
                                 }).FirstOrDefaultAsync();


            if (!ReferenceEquals(vendors, null))
                vendorId = vendors.vendorId;

            return vendorId;
        }

        private async Task<int> TaxCreditId(string taxCreditDesc, long organizationUnitId)
        {
            int taxcreditId = 0;
            var taxcredits = await (from taxcredit in _taxCreditUnitRepository.GetAll()
                          .Where(p => p.Description == taxCreditDesc || p.Number == taxCreditDesc)
                           .WhereIf(!ReferenceEquals(organizationUnitId, null), p => p.OrganizationUnitId == organizationUnitId)
                                    select new
                                    {
                                        taxcreditId = taxcredit.Id
                                    }).FirstOrDefaultAsync();


            if (!ReferenceEquals(taxcredits, null))
                taxcreditId = taxcredits.taxcreditId;

            return taxcreditId;
        }

        private async Task<long> GetAccountId(string accDesc, int jobId, long organizationUnitId)
        {
            long accountId = 0;

            var chartOfAccountId = (from job in _jobUnitRepository.GetAll().WhereIf(!ReferenceEquals(jobId, null), p => p.Id == jobId)
                                    select job.ChartOfAccountId).FirstOrDefault();


            var account = await (from debitaccount in _accountUnitRepository.GetAll()
                                         .Where(p => p.Caption == accDesc || p.AccountNumber == accDesc)
                                         .WhereIf(!ReferenceEquals(organizationUnitId, null), p => p.OrganizationUnitId == organizationUnitId)
                                         .WhereIf(chartOfAccountId != 0, p => p.ChartOfAccountId == chartOfAccountId)
                                 select new
                                 {
                                     accountId = debitaccount.Id,
                                 }).FirstOrDefaultAsync();

            if (!ReferenceEquals(account, null))
                accountId = account.accountId;

            return accountId;
        }


    }

}

