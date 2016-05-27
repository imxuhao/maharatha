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

        public JournalEntryDocDetailAppService(
            JournalEntryDocumentDetailUnitManager journalEntryDocumentDetailUnitManager,
            IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitRepository,
            IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<CoaUnit, int> coaUnitRepository,
            IRepository<VendorUnit, int> vendorUnitRepository, CustomAppSession customAppSession,
            IRepository<TaxRebateUnit, int> taxRebateUnitRepository, ICacheManager cacheManager
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
        }

        /// <summary>
        ///  Create Journal Entry Document Detail.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateJournalEntryDocDetailUnit(CreateJournalEntryDocDetailInputList input)
        {
            foreach (var journaldocList in input.CreateJournalEntryDocDetailList)
            {
                var journalDetails = MapJournalDetails(journaldocList);
                long creditParentId = 0;
                foreach (var item in journalDetails)
                {
                    if (creditParentId != 0)
                        item.AccountingItemOrigId = creditParentId;

                    await _journalEntryDocumentDetailUnitManager.CreateAsync(item);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    creditParentId = item.Id;
                }
            }
        }


        /// <summary>
        /// Update Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateJournalEntryDocumentUnit(UpdateJournalEntryDocDetailInputList input)
        {

            foreach (var journaldocDetails in input.UpdateJournalEntryDocDetailList)
            {
                long debitParentId = 0;
                var jouranlDetailsList = await MapJournalDetails(journaldocDetails);

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
                    else if (jouranlDetail.Id < 0)
                    {
                        IdInput<long> idInput = new IdInput<long>() { Id = (jouranlDetail.Id * (-1)) };
                        var creditJournalDetailId = _journalEntryDocumentDetailUnitRepository.GetAsync(idInput.Id).Result.AccountingItemOrigId.Value;
                        if (!ReferenceEquals(creditJournalDetailId, null))
                            await _journalEntryDocumentDetailUnitManager.DeleteAsync(new IdInput<long>() { Id = creditJournalDetailId });


                        await _journalEntryDocumentDetailUnitManager.DeleteAsync(idInput);
                    }
                    else
                    {
                        var journalEntryDocDetailUnit = jouranlDetail.MapTo<JournalEntryDocumentDetailUnit>();
                        await _journalEntryDocumentDetailUnitManager.CreateAsync(journalEntryDocDetailUnit);
                        await CurrentUnitOfWork.SaveChangesAsync();
                        debitParentId = journalEntryDocDetailUnit.Id;
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
                        join subAccount in _subAccountUnitRepository.GetAll() on journals.SubAccountId1 equals subAccount.Id
                            into subAccount
                        from subAccounts in subAccount.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on journals.VendorId equals vendor.Id into vendor
                        from vendors in vendor.DefaultIfEmpty()
                        join taxRebate in _taxRebateUnitRepository.GetAll() on journals.VendorId equals taxRebate.Id into
                            taxRebate
                        from taxRebates in taxRebate.DefaultIfEmpty()
                        select new
                        {
                            JournalDetails = journals,
                            Job = Jobs.JobNumber + " (" + Jobs.Caption + ")",
                            account = Lines.AccountNumber + " (" + Jobs.Caption + ")",
                            subAccount = subAccounts.Description,
                            vendor = vendors.LastName,
                            taxRebate = taxRebates.Description
                        };


            query = query.Where(p => p.JournalDetails.AccountingDocumentId.Value == input.AccountingDocumentId)
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null),
                    p => p.JournalDetails.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .ToListAsync();

            List<JournalEntryDocDetailUnitDto> mapResult = results.Select(item =>
            {


                var dto = item.JournalDetails.MapTo<JournalEntryDocDetailUnitDto>();
                dto.AccountingItemId = item.JournalDetails.Id;
                dto.Job = item.Job;
                dto.Account = item.account;
                dto.SubAccount1 = item.subAccount;
                dto.Vendor = item.vendor;
                dto.TaxRebate = item.taxRebate;
                return dto;
            }).ToList();

            var creditMapResult = MapJournalsDetailsOutPutDto(mapResult.OrderByDescending(u => u.Amount).ToList());

            return new PagedResultOutput<JournalEntryDocDetailUnitDto>(creditMapResult.Count, creditMapResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalEntryDocDetail"></param>
        /// <returns></returns>
        private List<JournalEntryDocumentDetailUnit> MapJournalDetails(CreateJournalEntryDocDetailInputUnit journalEntryDocDetail)
        {

            List<JournalEntryDocumentDetailUnit> journalEntryDetailUnitList = new List<JournalEntryDocumentDetailUnit>();
            JournalEntryDocumentDetailUnit debitJournalEntryDetailUnit = null;
            JournalEntryDocumentDetailUnit creditjournalEntryDetailUnit = null;
            bool isMinusAmmount = false;

            //validate Amount value is zero 
            if (journalEntryDocDetail.Amount.Value == 0)
            {
                throw new UserFriendlyException(L("Amount is Required"));
            }
            else
            //check amount is +/- IVE
            if (Math.Sign(journalEntryDocDetail.Amount.Value) == 1)
            {
                debitJournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                journalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                isMinusAmmount = true;
            }
            if (Math.Sign(journalEntryDocDetail.Amount.Value) == -1 || isMinusAmmount)
            {
                if (!isMinusAmmount)
                {
                    debitJournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                    debitJournalEntryDetailUnit.Amount = Math.Abs(journalEntryDocDetail.Amount.Value);
                    journalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                }

                creditjournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
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
                if (isMinusAmmount)
                    creditjournalEntryDetailUnit.Amount = -Math.Abs(journalEntryDocDetail.Amount.Value);
                journalEntryDetailUnitList.Add(creditjournalEntryDetailUnit);
            }


            if (!ReferenceEquals(debitJournalEntryDetailUnit, null) && !ReferenceEquals(creditjournalEntryDetailUnit, null))
            {
                if ((debitJournalEntryDetailUnit.JobId != 0 && debitJournalEntryDetailUnit.AccountId == 0) || (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Debit Job and Account are Required"));
                }
                if ((creditjournalEntryDetailUnit.JobId != 0 && creditjournalEntryDetailUnit.AccountId == 0) || (creditjournalEntryDetailUnit.JobId == 0 && creditjournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Credit Job and Account are Required"));
                }

                if (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId == 0)
                {
                    journalEntryDetailUnitList.Remove(debitJournalEntryDetailUnit);
                }
                if (creditjournalEntryDetailUnit.JobId == 0 && creditjournalEntryDetailUnit.AccountId == 0)
                {
                    journalEntryDetailUnitList.Remove(creditjournalEntryDetailUnit);
                }
            }
            return journalEntryDetailUnitList;

        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="journalEntryDocDetail"></param>
        /// <returns></returns>
        private async Task<List<JournalEntryDocumentDetailUnit>> MapJournalDetails(UpdateJournalEntryDocDetailInputUnit journalEntryDocDetail)
        {

            List<JournalEntryDocumentDetailUnit> journalEntryDetailUnitList = new List<JournalEntryDocumentDetailUnit>();
            JournalEntryDocumentDetailUnit debitJournalEntryDetailUnit = null;
            JournalEntryDocumentDetailUnit creditjournalEntryDetailUnit = null;
            bool isMinusAmmount = false;

            var journalDetailItem = await _journalEntryDocumentDetailUnitRepository.GetAsync(journalEntryDocDetail.AccountingItemId);

            //validate Amount value is zero 
            if (journalEntryDocDetail.Amount.Value == 0)
            {
                throw new UserFriendlyException(L("Amount is Required"));
            }
            else
            //check amount is +/- IVE
            if (Math.Sign(journalDetailItem.Amount.Value) == 1)
            {
                debitJournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();

                if (journalEntryDocDetail.AccountingItemId != 0)
                {
                    debitJournalEntryDetailUnit.Id = journalDetailItem.Id;
                }

                Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                              .ForMember(u => u.Id, ap => ap.Ignore())
                              .ForMember(u => u.TenantId, ap => ap.Ignore());


                Mapper.Map(debitJournalEntryDetailUnit, journalDetailItem);
                journalDetailItem.Amount = Math.Abs(debitJournalEntryDetailUnit.Amount.Value);
                journalDetailItem.AccountingDocumentId = debitJournalEntryDetailUnit.AccountingDocumentId;
                journalEntryDetailUnitList.Add(journalDetailItem);

                var creditJournalDetailItem = await _journalEntryDocumentDetailUnitRepository.FirstOrDefaultAsync(u => u.AccountingItemOrigId == journalDetailItem.Id);

                if (!ReferenceEquals(creditJournalDetailItem, null))
                {
                    long creditParentId = creditJournalDetailItem.AccountingItemOrigId.Value;
                    creditjournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                    Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                            .ForMember(u => u.Id, ap => ap.Ignore())
                            .ForMember(u => u.TenantId, ap => ap.Ignore());
                    Mapper.Map(creditjournalEntryDetailUnit, creditJournalDetailItem);

                    creditJournalDetailItem.JobId = journalEntryDocDetail.CreditJobId;
                    creditJournalDetailItem.AccountId = journalEntryDocDetail.CreditAccountId;
                    creditJournalDetailItem.SubAccountId1 = journalEntryDocDetail.CreditSubAccountId1;
                    creditJournalDetailItem.SubAccountId2 = journalEntryDocDetail.CreditSubAccountId2;
                    creditJournalDetailItem.SubAccountId3 = journalEntryDocDetail.CreditSubAccountId3;
                    creditJournalDetailItem.SubAccountId4 = journalEntryDocDetail.CreditSubAccountId4;
                    creditJournalDetailItem.SubAccountId5 = journalEntryDocDetail.CreditSubAccountId5;
                    creditJournalDetailItem.SubAccountId6 = journalEntryDocDetail.CreditSubAccountId6;
                    creditJournalDetailItem.SubAccountId7 = journalEntryDocDetail.CreditSubAccountId7;
                    creditJournalDetailItem.SubAccountId8 = journalEntryDocDetail.CreditSubAccountId8;
                    creditJournalDetailItem.SubAccountId9 = journalEntryDocDetail.CreditSubAccountId9;
                    creditJournalDetailItem.SubAccountId10 = journalEntryDocDetail.CreditSubAccountId10;
                    creditJournalDetailItem.AccountingDocumentId = journalEntryDocDetail.AccountingDocumentId;
                    creditJournalDetailItem.AccountingItemOrigId = creditParentId;

                    creditJournalDetailItem.Amount = -Math.Abs(journalEntryDocDetail.Amount.Value);
                    journalEntryDetailUnitList.Add(creditJournalDetailItem);
                }
                else
                    isMinusAmmount = true;


            }
            if (Math.Sign(journalDetailItem.Amount.Value) == -1 || isMinusAmmount)
            {

                if (!isMinusAmmount)
                {
                    debitJournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();

                    if (journalEntryDocDetail.AccountingItemId != 0 && Math.Sign(journalDetailItem.Amount.Value) == 1)
                    {
                        debitJournalEntryDetailUnit.Id = journalDetailItem.Id;
                        debitJournalEntryDetailUnit.AccountingDocumentId = journalDetailItem.AccountingDocumentId;
                    }

                    debitJournalEntryDetailUnit.Amount = Math.Abs(journalEntryDocDetail.Amount.Value);
                    journalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                }

                creditjournalEntryDetailUnit = journalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                        .ForMember(u => u.Id, ap => ap.Ignore())
                        .ForMember(u => u.TenantId, ap => ap.Ignore());
                Mapper.Map(creditjournalEntryDetailUnit, journalDetailItem);
                journalDetailItem.JobId = journalEntryDocDetail.CreditJobId;
                journalDetailItem.AccountId = journalEntryDocDetail.CreditAccountId;
                journalDetailItem.SubAccountId1 = journalEntryDocDetail.CreditSubAccountId1;
                journalDetailItem.SubAccountId2 = journalEntryDocDetail.CreditSubAccountId2;
                journalDetailItem.SubAccountId3 = journalEntryDocDetail.CreditSubAccountId3;
                journalDetailItem.SubAccountId4 = journalEntryDocDetail.CreditSubAccountId4;
                journalDetailItem.SubAccountId5 = journalEntryDocDetail.CreditSubAccountId5;
                journalDetailItem.SubAccountId6 = journalEntryDocDetail.CreditSubAccountId6;
                journalDetailItem.SubAccountId7 = journalEntryDocDetail.CreditSubAccountId7;
                journalDetailItem.SubAccountId8 = journalEntryDocDetail.CreditSubAccountId8;
                journalDetailItem.SubAccountId9 = journalEntryDocDetail.CreditSubAccountId9;
                journalDetailItem.SubAccountId10 = journalEntryDocDetail.CreditSubAccountId10;
                journalDetailItem.AccountingDocumentId = journalEntryDocDetail.AccountingDocumentId;
                if (isMinusAmmount)
                    journalDetailItem.Amount = -Math.Abs(journalEntryDocDetail.Amount.Value);
                journalEntryDetailUnitList.Add(journalDetailItem);
            }
            if (!ReferenceEquals(debitJournalEntryDetailUnit, null) && !ReferenceEquals(creditjournalEntryDetailUnit, null))
            {
                if ((debitJournalEntryDetailUnit.JobId != 0 && debitJournalEntryDetailUnit.AccountId == 0) || (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Debit Job and Account are Required"));
                }
                if ((creditjournalEntryDetailUnit.JobId != 0 && creditjournalEntryDetailUnit.AccountId == 0) || (creditjournalEntryDetailUnit.JobId == 0 && creditjournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Credit Job and Account are Required"));
                }

                if (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId == 0)
                {
                    journalEntryDetailUnitList.Remove(debitJournalEntryDetailUnit);
                }
                if (creditjournalEntryDetailUnit.JobId == 0 && creditjournalEntryDetailUnit.AccountId == 0)
                {
                    journalEntryDetailUnitList.Remove(creditjournalEntryDetailUnit);
                }
            }
            return journalEntryDetailUnitList;

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
                        journalDetail.CreditAccount = creditJournalItem.Account;
                        journalDetail.CreditAccountId = creditJournalItem.AccountId;
                        journalDetail.CreditJob = creditJournalItem.Job;
                        journalDetail.CreditJobId = creditJournalItem.JobId;
                        journalDetail.CreditSubAccount1 = creditJournalItem.SubAccount1;
                        journalDetail.CreditSubAccount10 = creditJournalItem.SubAccount10;
                        journalDetail.CreditSubAccount2 = creditJournalItem.SubAccount2;
                        journalDetail.CreditSubAccount3 = creditJournalItem.SubAccount3;
                        journalDetail.CreditSubAccount4 = creditJournalItem.SubAccount4;
                        journalDetail.CreditSubAccount5 = creditJournalItem.SubAccount5;
                        journalDetail.CreditSubAccount6 = creditJournalItem.SubAccount6;
                        journalDetail.CreditSubAccount7 = creditJournalItem.SubAccount7;
                        journalDetail.CreditSubAccount8 = creditJournalItem.SubAccount8;
                        journalDetail.CreditSubAccount9 = creditJournalItem.SubAccount9;
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
                        journalEntryDocDetailList.Remove(creditJournalItem);
                    }
                    journaList.Add(journalDetail);
                }
                else
                if (Math.Sign(journalEntryDocDetailList[i].Amount.Value) == -1)
                {

                    journalDetail.CreditAccount = journalEntryDocDetailList[i].Account;
                    journalDetail.CreditAccountId = journalEntryDocDetailList[i].AccountId;
                    journalDetail.CreditJob = journalEntryDocDetailList[i].Job;
                    journalDetail.CreditJobId = journalEntryDocDetailList[i].JobId;
                    journalDetail.CreditSubAccount1 = journalEntryDocDetailList[i].SubAccount1;
                    journalDetail.CreditSubAccount10 = journalEntryDocDetailList[i].SubAccount10;
                    journalDetail.CreditSubAccount2 = journalEntryDocDetailList[i].SubAccount2;
                    journalDetail.CreditSubAccount3 = journalEntryDocDetailList[i].SubAccount3;
                    journalDetail.CreditSubAccount4 = journalEntryDocDetailList[i].SubAccount4;
                    journalDetail.CreditSubAccount5 = journalEntryDocDetailList[i].SubAccount5;
                    journalDetail.CreditSubAccount6 = journalEntryDocDetailList[i].SubAccount6;
                    journalDetail.CreditSubAccount7 = journalEntryDocDetailList[i].SubAccount7;
                    journalDetail.CreditSubAccount8 = journalEntryDocDetailList[i].SubAccount8;
                    journalDetail.CreditSubAccount9 = journalEntryDocDetailList[i].SubAccount9;
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
                    journalDetail.Account = string.Empty;
                    journalDetail.AccountId = null;
                    journalDetail.Job = string.Empty;
                    journalDetail.JobId = null;
                    journalDetail.SubAccount1 = string.Empty;
                    journalDetail.SubAccount10 = string.Empty;
                    journalDetail.SubAccount2 = string.Empty;
                    journalDetail.SubAccount3 = string.Empty;
                    journalDetail.SubAccount4 = string.Empty;
                    journalDetail.SubAccount5 = string.Empty;
                    journalDetail.SubAccount6 = string.Empty;
                    journalDetail.SubAccount7 = string.Empty;
                    journalDetail.SubAccount8 = string.Empty;
                    journalDetail.SubAccount9 = string.Empty;
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

    }

}

