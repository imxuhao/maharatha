using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Helpers;
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
using Abp.Collections.Extensions;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.UI;
using Abp.Domain.Uow;
using AutoMapper;
using CAPS.CORPACCOUNTING.Journals;

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
        private IdOutputDto<long> _response = null;
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

                long debitParentId = 0;
                foreach (var item in journalDetails)
                {
                    if (debitParentId != 0)
                        item.AccountingItemOrigId = debitParentId;

                    await _journalEntryDocumentDetailUnitManager.CreateAsync(item);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    debitParentId = item.Id;
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
                var jouranlDetailsList = MapJournalDetails(journaldocDetails);

                foreach (var jouranlDetail in jouranlDetailsList)
                {

                    //If AccountingItemId > 0 records will updated
                    //If AccountingItemId < 0 records will deleted
                    //Otherwise journal Details Added.
                    if (jouranlDetail.Id > 0)
                    {
                        var journalEntryDocDetailUnit = _journalEntryDocumentDetailUnitRepository.Get(jouranlDetail.Id);
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
        public async Task<PagedResultOutput<JournalEntryDocDetailUnitDto>> GetJournalEntryDocDetailsByAccountingDocId(
            GetTransactionList input)
        {
            var query = from journals in _journalEntryDocumentDetailUnitRepository.GetAll()
                            //join creditJournal in _journalEntryDocumentDetailUnitRepository.GetAll() on journals.AccountingItemOrigId equals creditJournal.Id into creditJournals
                            //from creditJournal in creditJournals.DefaultIfEmpty()
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

            return new PagedResultOutput<JournalEntryDocDetailUnitDto>(resultCount, results.Select(item =>
            {


                var dto = item.JournalDetails.MapTo<JournalEntryDocDetailUnitDto>();
                dto.AccountId = item.JournalDetails.Id;
                dto.Job = item.Job;
                dto.Account = item.account;
                dto.SubAccount1 = item.subAccount;
                dto.Vendor = item.vendor;
                dto.TaxRebate = item.taxRebate;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JournalEntryDocDetail"></param>
        /// <returns></returns>
        private List<JournalEntryDocumentDetailUnit> MapJournalDetails(CreateJournalEntryDocDetailInputUnit JournalEntryDocDetail)
        {

            List<JournalEntryDocumentDetailUnit> JournalEntryDetailUnitList = new List<JournalEntryDocumentDetailUnit>();
            JournalEntryDocumentDetailUnit debitJournalEntryDetailUnit = null;
            JournalEntryDocumentDetailUnit CreditjournalEntryDetailUnit = null;
            bool isMinusAmmount = false;



            //validate Amount value is zero 
            if (JournalEntryDocDetail.Amount.Value == 0)
            {
                throw new UserFriendlyException(L("Error - Input Required"));
            }
            else
            //check amount is +/- IVE
            if (Math.Sign(JournalEntryDocDetail.Amount.Value) == 1)
            {
                debitJournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                JournalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                isMinusAmmount = true;
            }
            if (Math.Sign(JournalEntryDocDetail.Amount.Value) == -1 || isMinusAmmount)
            {
                if (!isMinusAmmount)
                {
                    debitJournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                    debitJournalEntryDetailUnit.Amount = Math.Sign(JournalEntryDocDetail.Amount.Value);
                    JournalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                }

                CreditjournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                CreditjournalEntryDetailUnit.JobId = JournalEntryDocDetail.CreditJobId;
                CreditjournalEntryDetailUnit.AccountId = JournalEntryDocDetail.CreditAccountId;
                CreditjournalEntryDetailUnit.SubAccountId1 = JournalEntryDocDetail.CreditSubAccountId1;
                CreditjournalEntryDetailUnit.SubAccountId2 = JournalEntryDocDetail.CreditSubAccountId2;
                CreditjournalEntryDetailUnit.SubAccountId3 = JournalEntryDocDetail.CreditSubAccountId3;
                CreditjournalEntryDetailUnit.SubAccountId4 = JournalEntryDocDetail.CreditSubAccountId4;
                CreditjournalEntryDetailUnit.SubAccountId5 = JournalEntryDocDetail.CreditSubAccountId5;
                CreditjournalEntryDetailUnit.SubAccountId6 = JournalEntryDocDetail.CreditSubAccountId6;
                CreditjournalEntryDetailUnit.SubAccountId7 = JournalEntryDocDetail.CreditSubAccountId7;
                CreditjournalEntryDetailUnit.SubAccountId8 = JournalEntryDocDetail.CreditSubAccountId8;
                CreditjournalEntryDetailUnit.SubAccountId9 = JournalEntryDocDetail.CreditSubAccountId9;
                CreditjournalEntryDetailUnit.SubAccountId10 = JournalEntryDocDetail.CreditSubAccountId10;
                if (isMinusAmmount)
                    CreditjournalEntryDetailUnit.Amount = Math.Sign(JournalEntryDocDetail.Amount.Value) == 1 ? -Math.Abs(JournalEntryDocDetail.Amount.Value) : -Math.Abs(JournalEntryDocDetail.Amount.Value);
                JournalEntryDetailUnitList.Add(CreditjournalEntryDetailUnit);
            }


            if (!ReferenceEquals(debitJournalEntryDetailUnit, null) && !ReferenceEquals(CreditjournalEntryDetailUnit, null))
            {
                if ((debitJournalEntryDetailUnit.JobId != 0 && debitJournalEntryDetailUnit.AccountId == 0) || (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Error - Input Required"));
                }
                if ((CreditjournalEntryDetailUnit.JobId != 0 && CreditjournalEntryDetailUnit.AccountId == 0) || (CreditjournalEntryDetailUnit.JobId == 0 && CreditjournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Error - Input Required"));
                }

                if (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId == 0)
                {
                    JournalEntryDetailUnitList.Remove(debitJournalEntryDetailUnit);
                }
                if (CreditjournalEntryDetailUnit.JobId == 0 && CreditjournalEntryDetailUnit.AccountId == 0)
                {
                    JournalEntryDetailUnitList.Remove(CreditjournalEntryDetailUnit);
                }
            }
            return JournalEntryDetailUnitList;

        }



        ///// <summary>
        ///// update
        ///// </summary>
        ///// <param name="JournalEntryDocDetail"></param>
        ///// <returns></returns>
        //private List<JournalEntryDocumentDetailUnit> MapJournalDetails(UpdateJournalEntryDocDetailInputUnit JournalEntryDocDetail)
        //{

        //    List<JournalEntryDocumentDetailUnit> JournalEntryDetailUnitList = new List<JournalEntryDocumentDetailUnit>();
        //    JournalEntryDocumentDetailUnit debitJournalEntryDetailUnit = null;
        //    JournalEntryDocumentDetailUnit CreditjournalEntryDetailUnit = null;
        //    bool isMinusAmmount = false;

        //    var JournalDetailItem = _journalEntryDocumentDetailUnitRepository.Get(JournalEntryDocDetail.AccountingItemId);


        //    //validate Amount value is zero 
        //    if (JournalEntryDocDetail.Amount.Value == 0)
        //    {
        //        throw new UserFriendlyException(L("Error - Input Required"));
        //    }
        //    else
        //    //check amount is +/- IVE
        //    if (Math.Sign(JournalEntryDocDetail.Amount.Value) == 1)
        //    {
        //        debitJournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();


        //        if (JournalEntryDocDetail.AccountingItemId != 0 && Math.Sign(JournalDetailItem.Amount.Value) == 1)
        //        {
        //            debitJournalEntryDetailUnit.Id = JournalDetailItem.Id;
        //        }

        //        JournalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
        //        isMinusAmmount = true;
        //    }
        //    if (Math.Sign(JournalEntryDocDetail.Amount.Value) == -1 || isMinusAmmount)
        //    {

        //        if (!isMinusAmmount)
        //        {
        //            debitJournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();

        //            if (JournalEntryDocDetail.AccountingItemId != 0 && Math.Sign(JournalDetailItem.Amount.Value) == 1)
        //            {
        //                debitJournalEntryDetailUnit.Id = JournalDetailItem.Id;
        //            }

        //            debitJournalEntryDetailUnit.Amount = Math.Sign(JournalEntryDocDetail.Amount.Value) == 1 ? Math.Abs(JournalEntryDocDetail.Amount.Value) : -Math.Abs(JournalEntryDocDetail.Amount.Value);
        //            JournalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
        //        }



        //        CreditjournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
        //        CreditjournalEntryDetailUnit.JobId = JournalEntryDocDetail.CreditJobId;
        //        CreditjournalEntryDetailUnit.AccountId = JournalEntryDocDetail.CreditAccountId;
        //        CreditjournalEntryDetailUnit.SubAccountId1 = JournalEntryDocDetail.CreditSubAccountId1;
        //        CreditjournalEntryDetailUnit.SubAccountId2 = JournalEntryDocDetail.CreditSubAccountId2;
        //        CreditjournalEntryDetailUnit.SubAccountId3 = JournalEntryDocDetail.CreditSubAccountId3;
        //        CreditjournalEntryDetailUnit.SubAccountId4 = JournalEntryDocDetail.CreditSubAccountId4;
        //        CreditjournalEntryDetailUnit.SubAccountId5 = JournalEntryDocDetail.CreditSubAccountId5;
        //        CreditjournalEntryDetailUnit.SubAccountId6 = JournalEntryDocDetail.CreditSubAccountId6;
        //        CreditjournalEntryDetailUnit.SubAccountId7 = JournalEntryDocDetail.CreditSubAccountId7;
        //        CreditjournalEntryDetailUnit.SubAccountId8 = JournalEntryDocDetail.CreditSubAccountId8;
        //        CreditjournalEntryDetailUnit.SubAccountId9 = JournalEntryDocDetail.CreditSubAccountId9;
        //        CreditjournalEntryDetailUnit.SubAccountId10 = JournalEntryDocDetail.CreditSubAccountId10;

        //        if (JournalEntryDocDetail.AccountingItemId != 0 && Math.Sign(JournalDetailItem.Amount.Value) == -1)
        //        {
        //            CreditjournalEntryDetailUnit.Id = JournalDetailItem.Id;
        //        }



        //        if (isMinusAmmount)
        //            CreditjournalEntryDetailUnit.Amount = Math.Sign(JournalEntryDocDetail.Amount.Value) == 1 ? -Math.Abs(JournalEntryDocDetail.Amount.Value) : Math.Abs(JournalEntryDocDetail.Amount.Value);
        //        JournalEntryDetailUnitList.Add(CreditjournalEntryDetailUnit);

        //    }


        //    if (!ReferenceEquals(debitJournalEntryDetailUnit, null) && !ReferenceEquals(CreditjournalEntryDetailUnit, null))
        //    {
        //        if ((debitJournalEntryDetailUnit.JobId != 0 && debitJournalEntryDetailUnit.AccountId == 0) || (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId != 0))
        //        {
        //            throw new UserFriendlyException(L("Error - Input Required"));
        //        }
        //        if ((CreditjournalEntryDetailUnit.JobId != 0 && CreditjournalEntryDetailUnit.AccountId == 0) || (CreditjournalEntryDetailUnit.JobId == 0 && CreditjournalEntryDetailUnit.AccountId != 0))
        //        {
        //            throw new UserFriendlyException(L("Error - Input Required"));
        //        }

        //        if (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId == 0)
        //        {
        //            JournalEntryDetailUnitList.Remove(debitJournalEntryDetailUnit);
        //        }
        //        if (CreditjournalEntryDetailUnit.JobId == 0 && CreditjournalEntryDetailUnit.AccountId == 0)
        //        {
        //            JournalEntryDetailUnitList.Remove(CreditjournalEntryDetailUnit);
        //        }
        //    }
        //    return JournalEntryDetailUnitList;

        //}


        /// <summary>
        /// update
        /// </summary>
        /// <param name="JournalEntryDocDetail"></param>
        /// <returns></returns>
        private List<JournalEntryDocumentDetailUnit> MapJournalDetails(UpdateJournalEntryDocDetailInputUnit JournalEntryDocDetail)
        {

            List<JournalEntryDocumentDetailUnit> JournalEntryDetailUnitList = new List<JournalEntryDocumentDetailUnit>();
            JournalEntryDocumentDetailUnit debitJournalEntryDetailUnit = null;
            JournalEntryDocumentDetailUnit CreditjournalEntryDetailUnit = null;
            bool isMinusAmmount = false;

            var JournalDetailItem = _journalEntryDocumentDetailUnitRepository.GetAll().Where(u => u.Id == JournalEntryDocDetail.AccountingItemId).FirstOrDefault();


            //validate Amount value is zero 
            if (JournalEntryDocDetail.Amount.Value == 0)
            {
                throw new UserFriendlyException(L("Error - Input Required"));
            }
            else
            //check amount is +/- IVE
            if (Math.Sign(JournalDetailItem.Amount.Value) == 1)
            {
                debitJournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();

                if (JournalEntryDocDetail.AccountingItemId != 0 && Math.Sign(JournalDetailItem.Amount.Value) == 1)
                {
                    debitJournalEntryDetailUnit.Id = JournalDetailItem.Id;
                }

                Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                              .ForMember(u => u.Id, ap => ap.Ignore())
                              .ForMember(u => u.TenantId, ap => ap.Ignore());


                Mapper.Map(debitJournalEntryDetailUnit, JournalDetailItem);
                JournalDetailItem.Amount = Math.Abs(debitJournalEntryDetailUnit.Amount.Value);
                JournalDetailItem.AccountingDocumentId = debitJournalEntryDetailUnit.AccountingDocumentId;
                JournalEntryDetailUnitList.Add(JournalDetailItem);

                var CreditJournalDetailItem = _journalEntryDocumentDetailUnitRepository.GetAll().Where(u => u.AccountingItemOrigId == JournalDetailItem.Id).FirstOrDefault();

                if (!ReferenceEquals(CreditJournalDetailItem, null))
                {
                    long debitParentId = CreditJournalDetailItem.AccountingItemOrigId.Value;
                    CreditjournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                    Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                            .ForMember(u => u.Id, ap => ap.Ignore())
                            .ForMember(u => u.TenantId, ap => ap.Ignore());
                    Mapper.Map(CreditjournalEntryDetailUnit, CreditJournalDetailItem);

                    CreditJournalDetailItem.JobId = JournalEntryDocDetail.CreditJobId;
                    CreditJournalDetailItem.AccountId = JournalEntryDocDetail.CreditAccountId;
                    CreditJournalDetailItem.SubAccountId1 = JournalEntryDocDetail.CreditSubAccountId1;
                    CreditJournalDetailItem.SubAccountId2 = JournalEntryDocDetail.CreditSubAccountId2;
                    CreditJournalDetailItem.SubAccountId3 = JournalEntryDocDetail.CreditSubAccountId3;
                    CreditJournalDetailItem.SubAccountId4 = JournalEntryDocDetail.CreditSubAccountId4;
                    CreditJournalDetailItem.SubAccountId5 = JournalEntryDocDetail.CreditSubAccountId5;
                    CreditJournalDetailItem.SubAccountId6 = JournalEntryDocDetail.CreditSubAccountId6;
                    CreditJournalDetailItem.SubAccountId7 = JournalEntryDocDetail.CreditSubAccountId7;
                    CreditJournalDetailItem.SubAccountId8 = JournalEntryDocDetail.CreditSubAccountId8;
                    CreditJournalDetailItem.SubAccountId9 = JournalEntryDocDetail.CreditSubAccountId9;
                    CreditJournalDetailItem.SubAccountId10 = JournalEntryDocDetail.CreditSubAccountId10;
                    CreditJournalDetailItem.AccountingDocumentId= JournalEntryDocDetail.AccountingDocumentId;
                    CreditJournalDetailItem.AccountingItemOrigId = debitParentId;


                    if (isMinusAmmount)
                        CreditJournalDetailItem.Amount = -Math.Sign(JournalEntryDocDetail.Amount.Value);
                    JournalEntryDetailUnitList.Add(CreditJournalDetailItem);
                }
                else
                    isMinusAmmount = true;


            }
            if (Math.Sign(JournalDetailItem.Amount.Value) == -1 || isMinusAmmount)
            {

                if (!isMinusAmmount)
                {
                    debitJournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();

                    if (JournalEntryDocDetail.AccountingItemId != 0 && Math.Sign(JournalDetailItem.Amount.Value) == 1)
                    {
                        debitJournalEntryDetailUnit.Id = JournalDetailItem.Id;
                        debitJournalEntryDetailUnit.AccountingDocumentId = JournalDetailItem.AccountingDocumentId;
                    }

                    debitJournalEntryDetailUnit.Amount = Math.Abs(JournalEntryDocDetail.Amount.Value);
                    JournalEntryDetailUnitList.Add(debitJournalEntryDetailUnit);
                }



                CreditjournalEntryDetailUnit = JournalEntryDocDetail.MapTo<JournalEntryDocumentDetailUnit>();
                Mapper.CreateMap<JournalEntryDocumentDetailUnit, JournalEntryDocumentDetailUnit>()
                        .ForMember(u => u.Id, ap => ap.Ignore())
                        .ForMember(u => u.TenantId, ap => ap.Ignore());
                Mapper.Map(CreditjournalEntryDetailUnit, JournalDetailItem);


                JournalDetailItem.JobId = JournalEntryDocDetail.CreditJobId;
                JournalDetailItem.AccountId = JournalEntryDocDetail.CreditAccountId;
                JournalDetailItem.SubAccountId1 = JournalEntryDocDetail.CreditSubAccountId1;
                JournalDetailItem.SubAccountId2 = JournalEntryDocDetail.CreditSubAccountId2;
                JournalDetailItem.SubAccountId3 = JournalEntryDocDetail.CreditSubAccountId3;
                JournalDetailItem.SubAccountId4 = JournalEntryDocDetail.CreditSubAccountId4;
                JournalDetailItem.SubAccountId5 = JournalEntryDocDetail.CreditSubAccountId5;
                JournalDetailItem.SubAccountId6 = JournalEntryDocDetail.CreditSubAccountId6;
                JournalDetailItem.SubAccountId7 = JournalEntryDocDetail.CreditSubAccountId7;
                JournalDetailItem.SubAccountId8 = JournalEntryDocDetail.CreditSubAccountId8;
                JournalDetailItem.SubAccountId9 = JournalEntryDocDetail.CreditSubAccountId9;
                JournalDetailItem.SubAccountId10 = JournalEntryDocDetail.CreditSubAccountId10;
                JournalDetailItem.AccountingDocumentId = JournalEntryDocDetail.AccountingDocumentId;
                if (isMinusAmmount)
                    JournalDetailItem.Amount = Math.Sign(JournalEntryDocDetail.Amount.Value) == 1 ? -Math.Abs(JournalEntryDocDetail.Amount.Value) : -Math.Abs(JournalEntryDocDetail.Amount.Value);
                JournalEntryDetailUnitList.Add(JournalDetailItem);

            }


            if (!ReferenceEquals(debitJournalEntryDetailUnit, null) && !ReferenceEquals(CreditjournalEntryDetailUnit, null))
            {
                if ((debitJournalEntryDetailUnit.JobId != 0 && debitJournalEntryDetailUnit.AccountId == 0) || (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Error - Input Required"));
                }
                if ((CreditjournalEntryDetailUnit.JobId != 0 && CreditjournalEntryDetailUnit.AccountId == 0) || (CreditjournalEntryDetailUnit.JobId == 0 && CreditjournalEntryDetailUnit.AccountId != 0))
                {
                    throw new UserFriendlyException(L("Error - Input Required"));
                }

                if (debitJournalEntryDetailUnit.JobId == 0 && debitJournalEntryDetailUnit.AccountId == 0)
                {
                    JournalEntryDetailUnitList.Remove(debitJournalEntryDetailUnit);
                }
                if (CreditjournalEntryDetailUnit.JobId == 0 && CreditjournalEntryDetailUnit.AccountId == 0)
                {
                    JournalEntryDetailUnitList.Remove(CreditjournalEntryDetailUnit);
                }
            }
            return JournalEntryDetailUnitList;

        }

        private List<JournalEntryDocDetailUnitDto> MapJournalsDetailsOutPutDto(List<JournalEntryDocDetailUnitDto> JournalEntryDocDetailList)
        {

            List<JournalEntryDocDetailUnitDto> JournaList = new List<JournalEntryDocDetailUnitDto>();
            try
            {

                //List<JournalEntryDocDetailUnitDto> JournaListCopy = new List<JournalEntryDocDetailUnitDto>();
                // JournaListCopy = JournalEntryDocDetailList;


                for (int i = 0; i < JournalEntryDocDetailList.Count; i++)
                {
                    JournalEntryDocDetailUnitDto journalDetail = new JournalEntryDocDetailUnitDto();
                    Mapper.CreateMap<JournalEntryDocDetailUnitDto, JournalEntryDocDetailUnitDto>();
                    Mapper.Map(JournalEntryDocDetailList[i], journalDetail);

                    if (Math.Sign(JournalEntryDocDetailList[i].Amount.Value) == 1)
                    {
                        var creditJournalItem = JournalEntryDocDetailList.Find(u => u.AccountingItemOrigId == JournalEntryDocDetailList[i].AccountingItemId);
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
                            JournalEntryDocDetailList.Remove(creditJournalItem);
                        }
                        JournaList.Add(journalDetail);
                    }
                    else
                    if (Math.Sign(JournalEntryDocDetailList[i].Amount.Value) == -1)
                    {

                        journalDetail.CreditAccount = JournalEntryDocDetailList[i].Account;
                        journalDetail.CreditAccountId = JournalEntryDocDetailList[i].AccountId;
                        journalDetail.CreditJob = JournalEntryDocDetailList[i].Job;
                        journalDetail.CreditJobId = JournalEntryDocDetailList[i].JobId;
                        journalDetail.CreditSubAccount1 = JournalEntryDocDetailList[i].SubAccount1;
                        journalDetail.CreditSubAccount10 = JournalEntryDocDetailList[i].SubAccount10;
                        journalDetail.CreditSubAccount2 = JournalEntryDocDetailList[i].SubAccount2;
                        journalDetail.CreditSubAccount3 = JournalEntryDocDetailList[i].SubAccount3;
                        journalDetail.CreditSubAccount4 = JournalEntryDocDetailList[i].SubAccount4;
                        journalDetail.CreditSubAccount5 = JournalEntryDocDetailList[i].SubAccount5;
                        journalDetail.CreditSubAccount6 = JournalEntryDocDetailList[i].SubAccount6;
                        journalDetail.CreditSubAccount7 = JournalEntryDocDetailList[i].SubAccount7;
                        journalDetail.CreditSubAccount8 = JournalEntryDocDetailList[i].SubAccount8;
                        journalDetail.CreditSubAccount9 = JournalEntryDocDetailList[i].SubAccount9;
                        journalDetail.CreditSubAccountId1 = JournalEntryDocDetailList[i].SubAccountId1;
                        journalDetail.CreditSubAccountId10 = JournalEntryDocDetailList[i].SubAccountId10;
                        journalDetail.CreditSubAccountId2 = JournalEntryDocDetailList[i].SubAccountId2;
                        journalDetail.CreditSubAccountId3 = JournalEntryDocDetailList[i].SubAccountId3;
                        journalDetail.CreditSubAccountId4 = JournalEntryDocDetailList[i].SubAccountId4;
                        journalDetail.CreditSubAccountId5 = JournalEntryDocDetailList[i].SubAccountId5;
                        journalDetail.CreditSubAccountId6 = JournalEntryDocDetailList[i].SubAccountId6;
                        journalDetail.CreditSubAccountId7 = JournalEntryDocDetailList[i].SubAccountId7;
                        journalDetail.CreditSubAccountId8 = JournalEntryDocDetailList[i].SubAccountId8;
                        journalDetail.CreditSubAccountId9 = JournalEntryDocDetailList[i].SubAccountId9;


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
                        JournaList.Add(journalDetail);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return JournaList;


        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public IEnumerable<T> GetRemoveSafeEnumerator<T>(List<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                // Reset the value of i if it is invalid.
                // This occurs when more than one item
                // is removed from the list during the enumeration.
                if (i >= list.Count)
                {
                    if (list.Count == 0)
                        yield break;

                    i = list.Count - 1;
                }

                yield return list[i];
            }
        }
    }

}

