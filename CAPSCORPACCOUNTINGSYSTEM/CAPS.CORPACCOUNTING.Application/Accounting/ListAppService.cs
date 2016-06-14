using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.Collections.Extensions;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Accounting
{
    public class ListAppService : CORPACCOUNTINGServiceBase, IListAppService
    {
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnit;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;
        private readonly ICacheManager _cacheManager;
        private readonly CustomAppSession _customAppSession;
        private readonly IVendorCache _vendorCache;
        private readonly IDivisionCache _dividsCache;
        private readonly IAccountCache _accountCache;
        private readonly ISubAccountCache _subAccountCache;



        public ListAppService(IRepository<SubAccountUnit, long> subAccountUnitRepository, IRepository<JobUnit> jobUnitRepository,
            CustomAppSession customAppSession, IRepository<AccountUnit, long> accountUnitRepository, ICacheManager cacheManager,
            IRepository<VendorUnit> vendorUnitRepository, IRepository<TaxCreditUnit> taxCreditUnitRepository, IVendorCache vendorCache,
            IDivisionCache dividsCache, IAccountCache accountCache, ISubAccountCache subAccountCache, IRepository<CoaUnit> coaUnit)
        {
            _subAccountUnitRepository = subAccountUnitRepository;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _customAppSession = customAppSession;
            _cacheManager = cacheManager;
            _vendorUnitRepository = vendorUnitRepository;
            _taxCreditUnitRepository = taxCreditUnitRepository;
            _vendorCache = vendorCache;
            _dividsCache = dividsCache;
            _accountCache = accountCache;
            _subAccountCache = subAccountCache;
            _coaUnit = coaUnit;
        }


        /// <summary>
        /// Get Jobs or Divisions List by using OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<DivisionCacheItem>> GetJobOrDivisionList(AutoSearchInput input)
        {
            var cacheItem = await _dividsCache.GetDivisionCacheItemAsync(
                  CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return cacheItem.DivisionCacheItemList.ToList().Where(p => p.TypeOfJobStatusId != ProjectStatus.Closed).
                WhereIf(!string.IsNullOrEmpty(input.Query), p => p.JobNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
            p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

        /// <summary>
        /// Get accounts 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AccountCacheItem>> GetAccountsList(AutoSearchInput input)
        {
            var chartOfAccountId = (from job in _jobUnitRepository.GetAll().Where(p => p.Id == input.JobId)
                                    select job.ChartOfAccountId).FirstOrDefault();

            var accountList = await _accountCache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);

            return accountList.AccountCacheItemList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).WhereIf(chartOfAccountId != 0, p => p.ChartOfAccountId == chartOfAccountId).ToList();


        }

        /// <summary>
        /// Get SubAccounts List based on OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<SubAccountCacheItem>> GetSubAccountList(AutoSearchInput input)
        {
            var cacheItem = await _subAccountCache.GetSubAccountCacheItemAsync(
              CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return cacheItem.SubAccountCacheItemList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                        p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.SubAccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                        p.SearchNo.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

        /// <summary>
        /// Get Vendors
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<VendorCacheItem>> GetVendorList(AutoSearchInput input)
        {
            var cacheItem = await _vendorCache.GetVendorsCacheItemAsync(CacheKeyStores.CalculateCacheKey(CacheKeyStores.VendorKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return cacheItem.VendorCacheItemList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), p => p.LastName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.FirstName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.VendorAccountInfo.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.VendorNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();

        }

        /// <summary>
        /// Get TaxCreditList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AutoFillDto>> GetTaxCreditList(AutoSearchInput input)
        {
            var taxCreditList = await _taxCreditUnitRepository.GetAll()
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query) || p.Number.Contains(input.Query))
                 .Select(u => new AutoFillDto { Name = u.Number, Value = u.Id.ToString(), Column1 = u.Description }).ToListAsync();
            return taxCreditList;
        }


        /// <summary>
        /// Get Typeof1099T4
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeof1099T4List()
        {
            return EnumList.GetTypeof1099T4List();
        }
    }
}
