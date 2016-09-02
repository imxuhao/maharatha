using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Payroll.Dto;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Payroll
{
    /// <summary>
    /// PayrollEntryDocument AppService
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Payroll_Entry)]
    public class PayrollEntryDocumentAppService : CORPACCOUNTINGServiceBase, IPayrollEntryDocumentAppService
    {

        private readonly PayrollEntryDocumentUnitManager _payrollEntryDocumentUnitManager;
        private readonly IRepository<PayrollEntryDocumentUnit, long> _payrollEntryDocumentUnitRepository;
        private readonly IRepository<PayrollEntryDocumentDetailUnit, long> _payrollEntryDocumentDetailUnitRepository;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IRepository<BatchUnit> _batchUnitRepository;
        private readonly PayrollEntryDocumentDetailUnitManager _payrollEntryDocumentDetailUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;
        private readonly IVendorCache _vendorCache;
        private readonly CustomAppSession _customAppSession;

        public PayrollEntryDocumentAppService(PayrollEntryDocumentUnitManager payrollEntryDocumentUnitManager,
            IRepository<PayrollEntryDocumentUnit, long> payrollEntryDocumentUnitRepository, IRepository<VendorUnit> vendorUnitRepository,
            IRepository<BatchUnit> batchUnitRepository, IRepository<PayrollEntryDocumentDetailUnit, long> payrollEntryDocumentDetailUnit,
             PayrollEntryDocumentDetailUnitManager payrollEntryDocumentDetailUnitManager, IRepository<JobUnit> jobUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository, IRepository<SubAccountUnit, long> subAccountUnitRepository,
            IRepository<TaxCreditUnit> taxCreditUnitRepository, IVendorCache vendorCache, CustomAppSession customAppSession)
        {
            _payrollEntryDocumentUnitManager = payrollEntryDocumentUnitManager;
            _payrollEntryDocumentUnitRepository = payrollEntryDocumentUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _batchUnitRepository = batchUnitRepository;
            _payrollEntryDocumentDetailUnitRepository = payrollEntryDocumentDetailUnit;
            _payrollEntryDocumentDetailUnitManager = payrollEntryDocumentDetailUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _jobUnitRepository = jobUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
            _taxCreditUnitRepository = taxCreditUnitRepository;
            _vendorCache = vendorCache;
            _customAppSession = customAppSession;
        }

        /// <summary>
        /// Create the PayrollEntryDocument.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payroll_Entry_Create)]
        public async Task<IdOutputDto<long>> CreatePayrollEntryDocumentUnit(PayrollEntryDocumentInputUnit input)
        {
            var payrollEntryDocumentUnit = input.MapTo<PayrollEntryDocumentUnit>();
            IdOutputDto<long> response = new IdOutputDto<long>
            {
                Id = await _payrollEntryDocumentUnitManager.CreateAsync(payrollEntryDocumentUnit)
            };

            //Null Checking of PayrollEntryDocumentDetailInputList
            if (!ReferenceEquals(input.PayrollEntryDocumentDetailInputList, null))
            {
                //Bulk Insertion of payrollEntryDocumentDetail
                foreach (var payrollEntryDocumentDetail in input.PayrollEntryDocumentDetailInputList)
                {
                    payrollEntryDocumentDetail.AccountingDocumentId = response.Id;
                    await CreatePayrollEntryDocumentDetailsUnit(payrollEntryDocumentDetail);
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return response;
        }

        /// <summary>
        ///  Update the PayrollEntryDocument
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payroll_Entry_Edit)]
        public async Task UpdatePayrollEntryDocumentUnit(PayrollEntryDocumentInputUnit input)
        {
            var payrollEntryDocumentUnit = await _payrollEntryDocumentUnitRepository.GetAsync(input.AccountingDocumentId);
            Mapper.Map(input, payrollEntryDocumentUnit);
            await _payrollEntryDocumentUnitManager.UpdateAsync(payrollEntryDocumentUnit);

            if (!ReferenceEquals(input.PayrollEntryDocumentDetailInputList, null))
            {
                //Bulk CRUD operations of PayrollEntryDocumentDetailInputList
                foreach (var payrollEntryDocumentDetail in input.PayrollEntryDocumentDetailInputList)
                {
                    if (payrollEntryDocumentDetail.AccountingItemId == 0)
                    {
                        payrollEntryDocumentDetail.AccountingDocumentId = input.AccountingDocumentId;
                        await CreatePayrollEntryDocumentDetailsUnit(payrollEntryDocumentDetail);
                      
                    }
                    else if (payrollEntryDocumentDetail.AccountingItemId > 0)
                    {
                        await UpdatePayrollEntryDocumentDetailsUnit(payrollEntryDocumentDetail);
                    }
                    else
                    {
                        IdInput<long> idInput = new IdInput<long>()
                        {
                            Id = (payrollEntryDocumentDetail.AccountingItemId * (-1))
                        };
                        await _payrollEntryDocumentDetailUnitManager.DeleteAsync(idInput);
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Delete PayrollEntryDocument
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payroll_Entry_Delete)]
        public async Task DeletePayrollEntryDocumentUnit(IdInput<long> input)
        {
            await _payrollEntryDocumentDetailUnitRepository.DeleteAsync(p => p.AccountingDocumentId == input.Id);
            await _payrollEntryDocumentUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync(); ;
        }

        /// <summary>
        /// Get all PayrollEntryDocument with Paging and Sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payroll_Entry)]
        public async Task<PagedResultOutput<PayrollEntryDocumentUnitDto>> GetPayrollEntryDocumentUnits(SearchInputDto input)
        {
            bool unPosted = false;
            var query = from payrolls in _payrollEntryDocumentUnitRepository.GetAll()
                        join batch in _batchUnitRepository.GetAll() on payrolls.BatchId equals batch.Id
                           into batchunit
                        from batchunits in batchunit.DefaultIfEmpty()
                        join vendor in _vendorUnitRepository.GetAll() on payrolls.VendorId equals vendor.Id
                            into vendorunit
                        from vendors in vendorunit.DefaultIfEmpty()
                        select new { Payrolls = payrolls, BatchName = batchunits.Description, VendorName = vendors.LastName };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }
            query = query
                 .Where(u => u.Payrolls.TypeOfAccountingDocumentId == TypeOfAccountingDocument.Payroll &&
                       u.Payrolls.IsPosted == unPosted); ;


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Payrolls.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();


            return new PagedResultOutput<PayrollEntryDocumentUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Payrolls.MapTo<PayrollEntryDocumentUnitDto>();
                dto.VendorName = item.VendorName;
                dto.AccountingDocumentId = item.Payrolls.Id;
                return dto;
            }).ToList());
        }


        /// <summary>
        /// Get PayrollEntryDocumentDetails By AccountingDocumentID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payroll_Entry)]
        public async Task<PagedResultOutput<PayrollEntryDocumentDetailUnitDto>> GetPayrollEntryDocumentDetailsByAccountingDocumentId(GetTransactionList input)
        {
            var query = from payrolls in _payrollEntryDocumentDetailUnitRepository.GetAll()
                        join job in _jobUnitRepository.GetAll() on payrolls.JobId equals job.Id
                        into jobunit
                        from jobs in jobunit.DefaultIfEmpty()
                        join line in _accountUnitRepository.GetAll() on payrolls.AccountId equals line.Id
                        into account
                        from lines in account.DefaultIfEmpty()
                        join subAccount1 in _subAccountUnitRepository.GetAll() on payrolls.SubAccountId1 equals subAccount1.Id
                        into subAccountunit1
                        from subAccounts1 in subAccountunit1.DefaultIfEmpty()
                        join subAccount2 in _subAccountUnitRepository.GetAll() on payrolls.SubAccountId2 equals subAccount2.Id
                        into subAccountunit2
                        from subAccounts2 in subAccountunit2.DefaultIfEmpty()
                        join taxCredit in _taxCreditUnitRepository.GetAll() on payrolls.TaxRebateId equals taxCredit.Id
                        into taxCreditunit
                        from taxCredits in taxCreditunit.DefaultIfEmpty()
                        select new
                        {
                            PayrollDetails = payrolls,
                            JobDesc = jobs.JobNumber,
                            accountDesc = lines.AccountNumber,
                            subAccount1 = subAccounts1.Description,
                            subAccount2 = subAccounts2.Description,
                            taxCredit = taxCredits.Number
                        };

            query = query.Where(p => p.PayrollDetails.AccountingDocumentId.Value == input.AccountingDocumentId);

            var results = await query.AsNoTracking().ToListAsync();
            return new PagedResultOutput<PayrollEntryDocumentDetailUnitDto>(results.Count, results.Select(item =>
            {
                var dto = item.PayrollDetails.MapTo<PayrollEntryDocumentDetailUnitDto>();
                dto.AccountNumber = item.accountDesc;
                dto.SubAccountNumber1 = item.subAccount1;
                dto.JobLocation = item.subAccount2;
                dto.TaxRebateNumber = item.taxCredit;
                dto.AccountingDocumentId = item.PayrollDetails.Id;
                return dto;
            }).ToList());
        }


        /// <summary>
        /// Get Payroll Vendors
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<VendorCacheItem>> GetPayRollVendorList(AutoSearchInput input)
        {
            var vendorCacheItemList = await _vendorCache.GetVendorsCacheItemAsync(CacheKeyStores.CalculateCacheKey(CacheKeyStores.VendorKey, 
                Convert.ToInt32(_customAppSession.TenantId)));
            return vendorCacheItemList.ToList().Where(p=>p.TypeofVendorId == TypeofVendor.PayrollCompany)
               .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.LastName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.FirstName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.VendorAccountInfo.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.VendorNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();

        }


        private async Task CreatePayrollEntryDocumentDetailsUnit(PayrollEntryDocumentDetailInputUnit payrollEntryDocumentDetail)
        {
            var payrollEntryDocumentDetailUnit = payrollEntryDocumentDetail.MapTo<PayrollEntryDocumentDetailUnit>();
            payrollEntryDocumentDetailUnit.OriginalAccountId = payrollEntryDocumentDetail.AccountId;
            payrollEntryDocumentDetailUnit.OriginalJobId = payrollEntryDocumentDetail.JobId;
            await _payrollEntryDocumentDetailUnitManager.CreateAsync(payrollEntryDocumentDetailUnit);
        }

        private async Task UpdatePayrollEntryDocumentDetailsUnit(PayrollEntryDocumentDetailInputUnit payrollEntryDocumentDetail)
        {
            var payrollEntryDocumentDetailUnit = await _payrollEntryDocumentDetailUnitRepository.GetAsync(payrollEntryDocumentDetail.AccountingItemId);
            Mapper.Map(payrollEntryDocumentDetail, payrollEntryDocumentDetailUnit);
            await _payrollEntryDocumentDetailUnitManager.UpdateAsync(payrollEntryDocumentDetailUnit);
        }

    }
}
