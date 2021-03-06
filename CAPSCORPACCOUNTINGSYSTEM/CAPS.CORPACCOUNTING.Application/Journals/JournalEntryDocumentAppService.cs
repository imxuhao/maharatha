﻿using System.Collections.Generic;
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
using CAPS.CORPACCOUNTING.Authorization;
using AutoMapper;
using CAPS.CORPACCOUNTING.Journals.dto;
using System;
using Abp.BackgroundJobs;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;
using Castle.Core.Logging;
using CAPS.CORPACCOUNTING.BackgroundJobs;
using Hangfire;
using CAPS.CORPACCOUNTING.BackgroundJobs.Dto;

namespace CAPS.CORPACCOUNTING.Journals
{

    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize] //This is to ensure only logged in user has access to this module.
    public class JournalEntryDocumentAppService : CORPACCOUNTINGServiceBase, IJournalEntryDocumentAppService
    {
        private readonly JournalEntryDocumentUnitManager _journalEntryDocumentUnitManager;
        private readonly IRepository<JournalEntryDocumentUnit, long> _journalEntryDocumentUnitRepository;
        private readonly IRepository<BatchUnit> _batchUnitRepository;
        private readonly IRepository<User, long> _userUnitRepository;
        private readonly IRepository<JournalEntryDocumentDetailUnit, long> _journalEntryDocumentDetailUnitRepository;
        private readonly JournalEntryDocumentDetailUnitManager _journalEntryDocumentDetailUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<VendorUnit, int> _vendorUnitRepository;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;
        private readonly ILogger _logger;
        private readonly HangfireRecurringJobManager _recurringJobManager;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalEntryDocumentUnitManager"></param>
        /// <param name="journalEntryDocumentUnitRepository"></param>
        /// <param name="batchUnitRepository"></param>
        /// <param name="userUnitRepository"></param>
        /// <param name="journalEntryDocumentDetailUnitRepository"></param>
        /// <param name="journalEntryDocumentDetailUnitManager"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="subAccountUnitRepository"></param>
        /// <param name="vendorUnitRepository"></param>
        /// <param name="taxCreditUnitRepository"></param>
        /// <param name="logger"></param>
        /// <param name="recurringJobManager"></param>
        public JournalEntryDocumentAppService(JournalEntryDocumentUnitManager journalEntryDocumentUnitManager,
            IRepository<JournalEntryDocumentUnit, long> journalEntryDocumentUnitRepository,
            IRepository<BatchUnit> batchUnitRepository,
            IRepository<User, long> userUnitRepository,
            IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitRepository,
            JournalEntryDocumentDetailUnitManager journalEntryDocumentDetailUnitManager,
            IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<VendorUnit, int> vendorUnitRepository,
            IRepository<TaxCreditUnit> taxCreditUnitRepository,
            ILogger logger,
            HangfireRecurringJobManager recurringJobManager)
        {
            _journalEntryDocumentUnitManager = journalEntryDocumentUnitManager;
            _journalEntryDocumentUnitRepository = journalEntryDocumentUnitRepository;
            _batchUnitRepository = batchUnitRepository;
            _userUnitRepository = userUnitRepository;
            _journalEntryDocumentDetailUnitRepository = journalEntryDocumentDetailUnitRepository;
            _journalEntryDocumentDetailUnitManager = journalEntryDocumentDetailUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _taxCreditUnitRepository = taxCreditUnitRepository;
            _logger = logger;
            _recurringJobManager = recurringJobManager;
        }

        /// <summary>
        /// Create Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Journals_Entry_Create)]
        [UnitOfWork]
        public async Task<IdOutputDto<long>> CreateJournalEntryDocumentUnit(JournalEntryDocumentInputUnit input)
        {

            var journalEntryDocumentUnit = new JournalEntryDocumentUnit();
            Mapper.Map(input, journalEntryDocumentUnit);
            var accountDocumentId = await _journalEntryDocumentUnitManager.CreateAsync(journalEntryDocumentUnit);

            if (!ReferenceEquals(input.JournalEntryDetailList, null))
            {
                input.JournalEntryDetailList.ForEach(u => u.AccountingDocumentId = accountDocumentId);
                await JournalEntryDetails(input.JournalEntryDetailList.OrderByDescending(u => u.Amount).ToList());
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            if (journalEntryDocumentUnit.JournalTypeId == JournalType.RecurringEntries)
            {
                //await Helper.GetCron("x");
                await _recurringJobManager.AddOrUpdateAsync<JournalEntryBackGroundJob, BackgroundJobInput<long>>(
                    $"RecurringJournalID{accountDocumentId}",
                    new BackgroundJobInput<long>() { Id = accountDocumentId, tenantId = journalEntryDocumentUnit.TenantId }, input.CronExpression,
                    BackgroundJobPriority.Normal);
            }
            return new IdOutputDto<long>() { Id = accountDocumentId };
        }

        /// <summary>
        ///  Update Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Journals_Entry_Edit)]
        public async Task UpdateJournalEntryDocumentUnit(JournalEntryDocumentInputUnit input)
        {
            var journalEntryDocumentUnit =
                await _journalEntryDocumentUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, journalEntryDocumentUnit);
            await _journalEntryDocumentUnitManager.UpdateAsync(journalEntryDocumentUnit);

            if (!ReferenceEquals(input.JournalEntryDetailList, null))
                await JournalEntryDetails(input.JournalEntryDetailList.OrderByDescending(u => u.Amount).ToList());
            await CurrentUnitOfWork.SaveChangesAsync();

            if (journalEntryDocumentUnit.JournalTypeId == JournalType.RecurringEntries)
            {
                await _recurringJobManager.AddOrUpdateAsync<JournalEntryBackGroundJob, BackgroundJobInput<long>>(
                      $"RecurringJournalID{input.AccountingDocumentId}",
                      new BackgroundJobInput<long>() { Id = input.AccountingDocumentId, tenantId = journalEntryDocumentUnit.TenantId }, input.CronExpression,
                      BackgroundJobPriority.Normal);
            }
            else
            {
                _recurringJobManager.DeleteJob($"RecurringJournalID{journalEntryDocumentUnit.Id}");
            }
        }

        /// <summary>
        /// Delete Journal Entry Document
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Journals_Entry_Delete)]
        [UnitOfWork]
        public async Task DeleteJournalEntryDocumentUnit(IdInput input)
        {
            JournalEntryDocumentUnit journalEntryDocumentUnit = _journalEntryDocumentUnitRepository.Get(input.Id);
            await _journalEntryDocumentUnitManager.DeleteAsync(input);
            await _journalEntryDocumentDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (journalEntryDocumentUnit.JournalTypeId == JournalType.RecurringEntries && !journalEntryDocumentUnit.IsPosted)
            {
                if (journalEntryDocumentUnit.OriginalDocumentId != null)
                {
                    _recurringJobManager.DeleteJob($"RecurringJournalID{journalEntryDocumentUnit.OriginalDocumentId}");
                }
                else
                {
                    _recurringJobManager.DeleteJob($"RecurringJournalID{journalEntryDocumentUnit.Id}");
                }
            }


        }



        /// <summary>
        /// Delete Journal Details By Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Journals_Entry_Delete)]
        [UnitOfWork]
        public async Task DeleteJournalDetailUnit(IdInput<long> input)
        {



            await _journalEntryDocumentDetailUnitManager.DeleteAsync(input);
            var creditJournal =
                await
                    _journalEntryDocumentDetailUnitRepository.FirstOrDefaultAsync(
                        u => u.DebitAccountingItemId == input.Id);
            if (!ReferenceEquals(creditJournal, null))
            {
                await _journalEntryDocumentDetailUnitManager.DeleteAsync(new IdInput<long>() { Id = creditJournal.Id });
            }


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Journals_Entry)]
        public async Task<PagedResultOutput<JournalEntryDocumentUnitDto>> GetJournalEntryDocumentUnits(
            SearchInputDto input)
        {

            bool unPosted = false;
            var query = from journals in _journalEntryDocumentUnitRepository.GetAll()
                        join user in _userUnitRepository.GetAll() on journals.CreatorUserId equals user.Id into users
                        from userunits in users.DefaultIfEmpty()
                        join batch in _batchUnitRepository.GetAll() on journals.BatchId equals batch.Id
                            into batchunit
                        from batchunits in batchunit.DefaultIfEmpty()
                        select new { Journals = journals, BatchName = batchunits.Description, CreatedUser = userunits.Name };

            if (!ReferenceEquals(input.Filters, null))
            {
                var mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }
            query = query
                 .Where(u => u.Journals.TypeOfAccountingDocumentId == TypeOfAccountingDocument.GeneralLedger &&
                        u.Journals.IsPosted == unPosted);


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Journals.DocumentReference ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<JournalEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Journals.MapTo<JournalEntryDocumentUnitDto>();
                dto.BatchName = item.BatchName;
                dto.CreatedUser = item.CreatedUser;
                dto.JournalType = item.Journals.JournalTypeId.ToDisplayName();
                dto.AccountingDocumentId = item.Journals.Id;
                return dto;
            }).ToList());

        }

        /// <summary>
        /// Get Journal Entry Details by AccountingDocumentId List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<JournalEntryDetailUnitDto>> GetJournalDetailsByDocumentId(
            GetTransactionList input)
        {
            var query = from journals in _journalEntryDocumentDetailUnitRepository.GetAll()
                        join job in _jobUnitRepository.GetAll() on journals.JobId equals job.Id into job
                        from jobs in job.DefaultIfEmpty()
                        join line in _accountUnitRepository.GetAll() on journals.AccountId equals line.Id into line
                        from lines in line.DefaultIfEmpty()
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
                            jobs.JobNumber,
                            lines.AccountNumber,
                            SubAccountNumber1 = subAccounts1.SubAccountNumber,
                            SubAccountNumber2 = subAccounts2.SubAccountNumber,
                            SubAccountNumber3 = subAccounts3.SubAccountNumber,
                            vendorLastName = vendors.LastName,
                            taxCreditNumber = taxCredits.Number
                        };

            query = query.Where(p => p.JournalDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query
                .AsNoTracking()
                .ToListAsync();

            var mapResult = results.Select(item =>
            {
                var dto = item.JournalDetails.MapTo<JournalEntryDetailUnitDto>();
                dto.AccountingItemId = item.JournalDetails.Id;
                dto.JobNumber = item.JobNumber;
                dto.AccountNumber = item.AccountNumber;
                dto.SubAccountNumber1 = item.SubAccountNumber1;
                dto.SubAccountNumber2 = item.SubAccountNumber2;
                dto.SubAccountNumber3 = item.SubAccountNumber3;
                dto.VendorName = item.vendorLastName;
                dto.TaxRebateNumber = item.taxCreditNumber;
                return dto;
            }).ToList();

            return new PagedResultOutput<JournalEntryDetailUnitDto>(mapResult.Count, mapResult);
        }


        /// <summary>
        /// Get Journal Entry Details by AccountingDocumentId List.
        /// (This method is created for WEB UI
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<JournalCreditEntryDetailUnitDto>> GetJournalDetailsByAccountingDocumentId(
            GetTransactionList input)
        {
            var query = from journals in _journalEntryDocumentDetailUnitRepository.GetAll()
                        join job in _jobUnitRepository.GetAll() on journals.JobId equals job.Id into job
                        from jobs in job.DefaultIfEmpty()
                        join line in _accountUnitRepository.GetAll() on journals.AccountId equals line.Id into line
                        from lines in line.DefaultIfEmpty()
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
                            jobs.JobNumber,
                            lines.AccountNumber,
                            SubAccountNumber1 = subAccounts1.SubAccountNumber,
                            SubAccountNumber2 = subAccounts2.SubAccountNumber,
                            SubAccountNumber3 = subAccounts3.SubAccountNumber,
                            vendorLastName = vendors.LastName,
                            taxCreditNumber = taxCredits.Number
                        };


            query = query.Where(p => p.JournalDetails.AccountingDocumentId.Value == input.AccountingDocumentId);
               

            var results = await query
                .AsNoTracking()
                .ToListAsync();

            var mapResult = new List<JournalCreditEntryDetailUnitDto>();
            foreach (var item in results)
            {
                var dto = item.JournalDetails.MapTo<JournalCreditEntryDetailUnitDto>();
                dto.AccountingItemId = item.JournalDetails.Id;
                dto.JobNumber = item.JobNumber;
                dto.AccountNumber = item.AccountNumber;
                dto.SubAccountNumber1 = item.SubAccountNumber1;
                dto.SubAccountNumber2 = item.SubAccountNumber2;
                dto.SubAccountNumber3 = item.SubAccountNumber3;
                dto.VendorName = item.vendorLastName;
                dto.TaxRebateNumber = item.taxCreditNumber;
                mapResult.Add(dto);
            }

            var creditMapResult = MapJournalsDetailsOutPutDto(mapResult.OrderByDescending(u => u.Amount).ToList());

            return new PagedResultOutput<JournalCreditEntryDetailUnitDto>(creditMapResult.Count, creditMapResult);
        }

        private List<JournalCreditEntryDetailUnitDto> MapJournalsDetailsOutPutDto(List<JournalCreditEntryDetailUnitDto> journalEntryDocDetailList)
        {
            var journaList = new List<JournalCreditEntryDetailUnitDto>();
            for (int i = 0; i < journalEntryDocDetailList.Count; i++)
            {
                JournalCreditEntryDetailUnitDto journalDetail = new JournalCreditEntryDetailUnitDto();

                //Mapper.CreateMap<JournalCreditEntryDetailUnitDto, JournalCreditEntryDetailUnitDto>();
                Mapper.Map(journalEntryDocDetailList[i], journalDetail);

                switch (Math.Sign(journalEntryDocDetailList[i].Amount.Value))
                {
                    case 1:
                        var creditJournalItem =
                            journalEntryDocDetailList.Find(
                                u => u.DebitAccountingItemId == journalEntryDocDetailList[i].AccountingItemId);
                        if (!ReferenceEquals(creditJournalItem, null))
                        {
                            journalDetail.CreditAccountingItemId = creditJournalItem.AccountingItemId;
                            journalDetail.CreditJobId = creditJournalItem.JobId;
                            journalDetail.CreditJobNumber = creditJournalItem.JobNumber;
                            journalDetail.CreditAccountId = creditJournalItem.AccountId;
                            journalDetail.CreditAccountNumber = creditJournalItem.AccountNumber;
                            journalDetail.CreditSubAccountId1 = creditJournalItem.SubAccountId1;
                            journalDetail.CreditSubAccountId2 = creditJournalItem.SubAccountId2;
                            journalDetail.CreditSubAccountId3 = creditJournalItem.SubAccountId3;
                            journalDetail.CreditSubAccountNumber1 = creditJournalItem.SubAccountNumber1;
                            journalDetail.CreditSubAccountNumber2 = creditJournalItem.SubAccountNumber2;
                            journalDetail.CreditSubAccountNumber3 = creditJournalItem.SubAccountNumber3;
                            journalDetail.DebitAccountingItemId = creditJournalItem.DebitAccountingItemId;
                            journalEntryDocDetailList.Remove(creditJournalItem);
                        }
                        journaList.Add(journalDetail);
                        break;
                    case -1:
                        journalDetail.CreditAccountingItemId = journalEntryDocDetailList[i].AccountingItemId;
                        journalDetail.CreditJobId = journalEntryDocDetailList[i].JobId;
                        journalDetail.CreditJobNumber = journalEntryDocDetailList[i].JobNumber;
                        journalDetail.CreditAccountId = journalEntryDocDetailList[i].AccountId;
                        journalDetail.CreditAccountNumber = journalEntryDocDetailList[i].AccountNumber;
                        journalDetail.CreditSubAccountId1 = journalEntryDocDetailList[i].SubAccountId1;
                        journalDetail.CreditSubAccountId2 = journalEntryDocDetailList[i].SubAccountId2;
                        journalDetail.CreditSubAccountId3 = journalEntryDocDetailList[i].SubAccountId3;
                        journalDetail.CreditSubAccountNumber1 = journalEntryDocDetailList[i].SubAccountNumber1;
                        journalDetail.CreditSubAccountNumber2 = journalEntryDocDetailList[i].SubAccountNumber2;
                        journalDetail.CreditSubAccountNumber3 = journalEntryDocDetailList[i].SubAccountNumber3;
                        journalDetail.DebitAccountingItemId = journalEntryDocDetailList[i].DebitAccountingItemId;
                        journalDetail.AccountId = null;
                        journalDetail.AccountNumber = string.Empty;
                        journalDetail.JobId = null;
                        journalDetail.JobNumber = string.Empty;
                        journalDetail.SubAccountNumber1 = string.Empty;
                        journalDetail.SubAccountNumber2 = string.Empty;
                        journalDetail.SubAccountNumber3 = string.Empty;
                        journalDetail.SubAccountId1 = null;
                        journalDetail.SubAccountId2 = null;
                        journalDetail.SubAccountId3 = null;
                        journalDetail.AccountingItemId = 0;
                        journaList.Add(journalDetail);
                        break;
                }
            }
            return journaList;
        }


        /// <summary>
        /// Get JournalTypeList
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetJournalTypeList()
        {
            return EnumList.GetJournalTypeList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalEntryDetails"></param>
        /// <returns></returns>
        [UnitOfWork]
        private async Task JournalEntryDetails(List<JournalEntryDetailInputUnit> journalEntryDetails)
        {
            //adding/Update journalDocDetails
            for (var i = 0; i < journalEntryDetails.Count; i++)
            {
                JournalEntryDocumentDetailUnit journalDetail;
                long debitParentId = 0;
                if (journalEntryDetails[i].AccountingItemId == 0)
                {
                    if (journalEntryDetails[i].Amount.Value > 0)
                    {
                        //Debit Journal Entry
                        journalDetail = journalEntryDetails[i].MapTo<JournalEntryDocumentDetailUnit>();
                        if (journalDetail.Id == 0)
                            journalDetail.AccountingItemOrigAmount = journalDetail.Amount;
                        journalDetail.DebitAccountingItemId = null;
                        await _journalEntryDocumentDetailUnitManager.CreateAsync(journalDetail);
                        debitParentId = journalDetail.Id;

                        JournalEntryDetailInputUnit creditJournalItem = !string.IsNullOrEmpty(journalEntryDetails[i].DebitCreditGroup)
                               ? journalEntryDetails.Find(u => u.DebitCreditGroup.Trim() == journalEntryDetails[i].DebitCreditGroup.Trim() && u.Amount < 0)
                               : null;

                        //credit Journal Entry
                        if (!ReferenceEquals(creditJournalItem, null))
                        {
                            await CreditEntry(journalEntryDetails, debitParentId, creditJournalItem);
                        }
                        else
                        {
                            //delete credit Entry
                            if (journalDetail.Id != 0)
                                _journalEntryDocumentDetailUnitRepository.Delete(
                                    u => u.DebitAccountingItemId == journalDetail.Id);
                        }
                    }
                    else
                    {
                        //credit Journal Entry
                        journalDetail = journalEntryDetails[i].MapTo<JournalEntryDocumentDetailUnit>();
                        if (journalEntryDetails[i].AccountingItemId == 0)
                        {
                            journalDetail.AccountingItemOrigAmount = journalDetail.Amount;
                            await _journalEntryDocumentDetailUnitManager.CreateAsync(journalDetail);
                        }
                    }
                }
                else//AccountingItemId is not zero
                {
                    journalDetail = await _journalEntryDocumentDetailUnitRepository.GetAsync(journalEntryDetails[i].AccountingItemId);

                    if (!ReferenceEquals(journalDetail, null) && journalDetail.Amount.Value > 0)
                    {
                        if (journalEntryDetails[i].Amount.Value > 0)
                        {
                            debitParentId = 0;
                            Mapper.Map(journalEntryDetails[i], journalDetail);
                            journalDetail.DebitAccountingItemId = null;
                            await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalDetail);
                            debitParentId = journalDetail.Id;

                            JournalEntryDetailInputUnit creditJournalItem = !string.IsNullOrEmpty(journalEntryDetails[i].DebitCreditGroup)
                                    ? journalEntryDetails.Find(u => u.DebitCreditGroup.Trim() == journalEntryDetails[i].DebitCreditGroup.Trim() && u.Amount < 0)
                                    : null;

                            //credit Journal Entry
                            if (!ReferenceEquals(creditJournalItem, null))
                            {
                                await CreditEntry(journalEntryDetails, debitParentId, creditJournalItem);
                            }
                            else
                            {
                                //delete credit Entry
                                if (journalDetail.Id != 0)
                                    _journalEntryDocumentDetailUnitRepository.Delete(
                                        u => u.DebitAccountingItemId == journalDetail.Id);

                            }
                        }
                        else if (journalEntryDetails[i].Amount.Value < 0)
                        {
                            Mapper.Map(journalEntryDetails[i], journalDetail);
                            journalDetail.DebitAccountingItemId = null;
                            await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalDetail);
                        }
                    }
                    else if (!ReferenceEquals(journalDetail, null) && journalDetail.Amount.Value < 0)
                    {
                        //delete debit Entry
                        if (journalEntryDetails[i].Amount.Value < 0)
                        {
                            Mapper.Map(journalEntryDetails[i], journalDetail);
                            if (journalDetail.DebitAccountingItemId != null && journalDetail.DebitAccountingItemId != 0)
                            {
                                _journalEntryDocumentDetailUnitRepository.Delete(
                                    u => u.Id == journalDetail.DebitAccountingItemId);
                                journalDetail.DebitAccountingItemId = null;
                            }
                            await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalDetail);
                        }
                        else
                        {
                            journalDetail.DebitAccountingItemId = null;
                            Mapper.Map(journalEntryDetails[i], journalDetail);
                            await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalDetail);
                        }
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
        }

        private async Task<JournalEntryDocumentDetailUnit> CreditEntry(List<JournalEntryDetailInputUnit> journalEntryDetails, long debitParentId, JournalEntryDetailInputUnit creditJournalItem)
        {
            JournalEntryDocumentDetailUnit journalDetail = new JournalEntryDocumentDetailUnit();
            if (debitParentId != 0)
                creditJournalItem.DebitAccountingItemId = debitParentId;
            else
                creditJournalItem.DebitAccountingItemId = null;

            if (creditJournalItem.AccountingItemId != 0)
            {
                journalDetail = await _journalEntryDocumentDetailUnitRepository.GetAsync(creditJournalItem.AccountingItemId);
                Mapper.Map(creditJournalItem, journalDetail);
                await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalDetail);
            }
            else
            {
                journalDetail = creditJournalItem.MapTo<JournalEntryDocumentDetailUnit>();
                journalDetail.AccountingItemOrigAmount = journalDetail.Amount;
                await _journalEntryDocumentDetailUnitManager.CreateAsync(journalDetail);
            }

            journalEntryDetails.Remove(creditJournalItem);
            return journalDetail;
        }
    }
}
