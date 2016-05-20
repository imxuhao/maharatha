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
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Journals.dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Common;

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

        public JournalEntryDocDetailAppService(JournalEntryDocumentDetailUnitManager journalEntryDocumentDetailUnitManager,
            IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitRepository,
            IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<CoaUnit, int> coaUnitRepository,
             IRepository<VendorUnit, int> vendorUnitRepository,
               IRepository<TaxRebateUnit, int> taxRebateUnitRepository
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
        }

        /// <summary>
        ///  Create Journal Entry Document Detail.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateJournalEntryDocDetailUnit(List<CreateJournalEntryDocDetailInputUnit> input)
        {
            foreach (var journaldocDetails in input)
            {
                var journalEntryDocDetailUnit = journaldocDetails.MapTo<JournalEntryDocumentDetailUnit>();
                await _journalEntryDocumentDetailUnitManager.CreateAsync(journalEntryDocDetailUnit);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Update Journal Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateJournalEntryDocumentUnit(List<UpdateJournalEntryDocDetailInputUnit> input)
        {
            foreach (var journaldocDetails in input)
            {

                //If AccountingItemId > 0 records will updated
                //If AccountingItemId < 0 records will deleted
                //Otherwise journal Details Added.
                if (journaldocDetails.AccountingItemId > 0)
                {
                    var journalEntryDocDetailUnit = journaldocDetails.MapTo<JournalEntryDocumentDetailUnit>();
                    journalEntryDocDetailUnit.Id = journaldocDetails.AccountingItemId;
                    await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalEntryDocDetailUnit);

                    await CurrentUnitOfWork.SaveChangesAsync();
                }
                else if (journaldocDetails.AccountingItemId < 0)
                {
                    IdInput<long> idInput = new IdInput<long>() { Id = (journaldocDetails.AccountingItemId * (-1)) };
                    await _journalEntryDocumentDetailUnitManager.DeleteAsync(idInput);
                }
                else
                {
                    var journalEntryDocDetailUnit = journaldocDetails.MapTo<JournalEntryDocumentDetailUnit>();
                    await _journalEntryDocumentDetailUnitManager.CreateAsync(journalEntryDocDetailUnit);
                    await CurrentUnitOfWork.SaveChangesAsync();
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
                        join subAccount in _subAccountUnitRepository.GetAll() on journals.SubAccountId1 equals subAccount.Id into subAccount
                        from subAccounts in subAccount.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on journals.VendorId equals vendor.Id into vendor
                        from vendors in vendor.DefaultIfEmpty()
                        join taxRebate in _taxRebateUnitRepository.GetAll() on journals.VendorId equals taxRebate.Id into taxRebate
                        from taxRebates in taxRebate.DefaultIfEmpty()
                        select new
                        {
                            JournalDetails = journals,
                            Job = Jobs.JobNumber + " (" + Jobs.Caption + ")",
                            account = Lines.AccountNumber + " (" + Jobs.Caption + ")",
                            subAccount = subAccounts.Description,
                            vendor = vendors.LastName,
                            taxRebate= taxRebates.Description
                        };



            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(p => p.JournalDetails.AccountingDocumentId.Value == input.AccountingDocumentId)
                         .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.JournalDetails.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("JournalDetails.ItemMemo ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

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
        /// Get Jobs or Divisions List by using OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetJobOrDivisionList(AutoSearchInput input)
        {
            var Joblist = await (from job in _jobUnitRepository.GetAll()
                                    .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.Contains(input.Query))
                                    .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                                 select new NameValueDto { Name = job.Caption, Value = job.Id.ToString() })
                              .ToListAsync();

            return Joblist;
        }

        /// <summary>
        /// Get accounts List based on JobId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GeAccountsList(AutoSearchInput input)
        {

            var chartOfAccountId = (from job in _jobUnitRepository.GetAll().WhereIf(!ReferenceEquals(input.JobId, null), p => p.Id == input.JobId)
                                    select job.ChartOfAccountId).FirstOrDefault();

            var Accountlist = await (from account in _accountUnitRepository.GetAll()
                                         .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.Contains(input.Query))
                                         .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                                         .Where(p => p.ChartOfAccountId == chartOfAccountId)
                                     select new NameValueDto
                                     {
                                         Name = account.Caption,
                                         Value = account.Id.ToString()
                                     }
                         ).ToListAsync();
            return Accountlist;
        }

        /// <summary>
        /// Get SubAccounts List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetSubAccountList(AutoSearchInput input)
        {
            var subAccountlist = await (from subaccount in _subAccountUnitRepository.GetAll()
                                    .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.Contains(input.Query))
                                    .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                                        select new NameValueDto { Name = subaccount.Description, Value = subaccount.Id.ToString() })
                              .ToListAsync();
            return subAccountlist;
        }




    }
}
