using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.Collections.Extensions;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.PettyCash;
using CAPS.CORPACCOUNTING.Sessions;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using CAPS.CORPACCOUNTING.GenericSearch;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// 
    /// </summary>
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
        private readonly IDivisionCache _divisionCache;
        private readonly IAccountCache _accountCache;
        private readonly ISubAccountCache _subAccountCache;
        private readonly ISubAccountRestrictionCache _subAccountRestrictionCache;
        private readonly IRepository<SubAccountRestrictionUnit, long> _subAccountRestrictionRepository;
        private readonly IRepository<PettyCashAccountUnit, long> _pettyCashAccountUnitRepository;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaymentTermUnitRepository;
        private readonly IBankAccountCache _bankAccountCache;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="subAccountUnitRepository"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="customAppSession"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="cacheManager"></param>
        /// <param name="vendorUnitRepository"></param>
        /// <param name="taxCreditUnitRepository"></param>
        /// <param name="vendorCache"></param>
        /// <param name="dividsCache"></param>
        /// <param name="accountCache"></param>
        /// <param name="subAccountCache"></param>
        /// <param name="coaUnit"></param>
        /// <param name="subAccountRestrictionRepository"></param>
        /// <param name="subAccountRestrictionCache"></param>
        public ListAppService(IRepository<SubAccountUnit, long> subAccountUnitRepository, IRepository<JobUnit> jobUnitRepository,
            CustomAppSession customAppSession, IRepository<AccountUnit, long> accountUnitRepository, ICacheManager cacheManager,
            IRepository<VendorUnit> vendorUnitRepository, IRepository<TaxCreditUnit> taxCreditUnitRepository, IVendorCache vendorCache,
            IDivisionCache dividsCache, IAccountCache accountCache, ISubAccountCache subAccountCache, IRepository<CoaUnit> coaUnit,
            IRepository<SubAccountRestrictionUnit, long> subAccountRestrictionRepository, ISubAccountRestrictionCache subAccountRestrictionCache,
            IRepository<PettyCashAccountUnit, long> pettyCashAccountUnitRepository, IRepository<VendorPaymentTermUnit> vendorPaymentTermUnitRepository,
            IBankAccountCache bankAccountCache)
        {
            _subAccountUnitRepository = subAccountUnitRepository;
            _jobUnitRepository = jobUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _customAppSession = customAppSession;
            _cacheManager = cacheManager;
            _vendorUnitRepository = vendorUnitRepository;
            _taxCreditUnitRepository = taxCreditUnitRepository;
            _vendorCache = vendorCache;
            _divisionCache = dividsCache;
            _accountCache = accountCache;
            _subAccountCache = subAccountCache;
            _coaUnit = coaUnit;
            _subAccountRestrictionRepository = subAccountRestrictionRepository;
            _subAccountRestrictionCache = subAccountRestrictionCache;
            _pettyCashAccountUnitRepository = pettyCashAccountUnitRepository;
            _vendorPaymentTermUnitRepository = vendorPaymentTermUnitRepository;
            _bankAccountCache = bankAccountCache;
        }


        /// <summary>
        /// Get Jobs or Divisions List by using OrganizationUnitId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<DivisionCacheItem>> GetJobOrDivisionList(AutoSearchInput input)
        {
            var cacheItem = await _divisionCache.GetDivisionCacheItemAsync(
                  CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return cacheItem.DivisionCacheItemList.ToList().Where(p => p.TypeOfJobStatusId != ProjectStatus.Closed).
                WhereIf(!string.IsNullOrEmpty(input.Query), p => p.JobNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
            p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

        /// <summary>
        /// Get accounts based on Job chartofAccountId
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
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where( p => p.ChartOfAccountId == chartOfAccountId).ToList();


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
            var subaccountRestrictioncacheItem = await _subAccountRestrictionCache.GetSubAccountRestrictionCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountRestrictionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            if (input.AccountId == 0 || subaccountRestrictioncacheItem.SubAccountRestrictionCacheItemList.Count == 0)
            {
                return
                    cacheItem.SubAccountCacheItemList.ToList()
                        .WhereIf(!string.IsNullOrEmpty(input.Query),
                            p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.SubAccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.SearchNo.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
            }
            else
            {
                var res = from subaccount in cacheItem.SubAccountCacheItemList.ToList()
                          join subAccountRestriction in subaccountRestrictioncacheItem.SubAccountRestrictionCacheItemList.Where(p => p.AccountId == input.AccountId)
                          on subaccount.SubAccountId equals subAccountRestriction.SubAccountId
                          select subaccount;
                return res.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                            p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.SubAccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.SearchNo.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();

            }
        }

        /// <summary>
        /// Get Vendors
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<VendorCacheItem>> GetVendorList(AutoSearchInput input)
        {

            var vendorCacheItemList = await _vendorCache.GetVendorsCacheItemAsync(CacheKeyStores.CalculateCacheKey(CacheKeyStores.VendorKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return vendorCacheItemList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query), p => p.LastName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
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

        /// <summary>
        /// Get All Locations List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetLocationList(AutoSearchInput input)
        {
            var locationSets = await _subAccountUnitRepository.GetAll()
                 .Where(p => p.TypeofSubAccountId == TypeofSubAccount.Locations || p.TypeofSubAccountId == TypeofSubAccount.Sets)
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return locationSets;
        }


        /// <summary>
        /// Get Vendors, Accounts, Job or Division, Tax Credit and Sub Account List values by Type
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetListByNames(NameValueInputList input)
        {
            var list = new List<NameValueDto>();
            switch (input.Type)
            {
                case "vendors":
                    list = await GetVendorsListByNames(input);
                    break;

                case "accounts":
                    list = await GetAccountsListByNames(input);
                    break;

                case "jobordivision":
                    list = await GetJobOrDivisionListByNames(input);
                    break;
                case "subaccounts":
                    list = await GetSubAccountsListByNames(input);
                    break;
                case "taxcredit":
                    list = await GetTaxCreditListByNames(input);
                    break;
            }
            return list;
        }

        /// <summary>
        /// Get Jobs or Divisions List by using Job Numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<NameValueDto>> GetJobOrDivisionListByNames(NameValueInputList input)
        {
            var jobOrDivisionList = await _divisionCache.GetDivisionCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId });

            var result = (from jobNames in input.NameValueList
                          join jobOrdivision in jobOrDivisionList.DivisionCacheItemList.ToList() on jobNames.Name equals jobOrdivision.JobNumber
                              into jobOrdivisions
                          from jobList in jobOrdivisions.DefaultIfEmpty()
                          select new NameValueDto()
                          {
                              Name = jobNames.Name,
                              Value = jobList?.JobId.ToString()?? ""
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Get accounts List by using Account Numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<NameValueDto>> GetAccountsListByNames(NameValueInputList input)
        {
            var accountList = await _accountCache.GetAccountCacheItemAsync(
               CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId });

            var result = (from accNames in input.NameValueList
                          join account in accountList.AccountCacheItemList.ToList() on accNames.Name equals account.AccountNumber
                              into accounts
                          from account in accounts.DefaultIfEmpty()
                          select new NameValueDto()
                          {
                              Name = accNames.Name,
                              Value = account?.AccountId.ToString() ?? ""
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Get SubAccounts List by using SubAccount Numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<NameValueDto>> GetSubAccountsListByNames(NameValueInputList input)
        {
            var cacheItem = await _subAccountCache.GetSubAccountCacheItemAsync(
                      CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId });
            var subAccountRestrictioncacheItem = await _subAccountRestrictionCache.GetSubAccountRestrictionCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.SubAccountRestrictionKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId });

            var subAccountRestrictionList = from subaccount in cacheItem.SubAccountCacheItemList.ToList()
                                            join subAccountRestriction in subAccountRestrictioncacheItem.SubAccountRestrictionCacheItemList
                                            on subaccount.SubAccountId equals subAccountRestriction.SubAccountId
                                            select subaccount;

            var result = (from subaccountNumber in input.NameValueList
                          join subAccount in subAccountRestrictionList.ToList() on subaccountNumber.Name equals subAccount.SubAccountNumber
                              into subAccounts
                          from subAccount in subAccounts.DefaultIfEmpty()
                          select new NameValueDto()
                          {
                              Name = subaccountNumber.Name,
                              Value = subAccount?.SubAccountId.ToString() ?? ""
                          }).ToList();
            return result;

        }


        /// <summary>
        /// Get Vendors List by using vendor Numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<NameValueDto>> GetVendorsListByNames(NameValueInputList input)
        {
            var vendorsList = await _vendorCache.GetVendorsCacheItemAsync(CacheKeyStores.CalculateCacheKey(CacheKeyStores.VendorKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), new AutoSearchInput() { OrganizationUnitId = input.OrganizationUnitId });

            var result = (from vendorNames in input.NameValueList
                          join vendor in vendorsList.ToList() on vendorNames.Name equals vendor.VendorNumber
                              into vendors
                          from vendor in vendors.DefaultIfEmpty()
                          select new NameValueDto()
                          {
                              Name = vendorNames.Name,
                              Value = vendor?.VendorId.ToString() ?? ""
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Get TaxCredit List by using Numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<List<NameValueDto>> GetTaxCreditListByNames(NameValueInputList input)
        {
            var strTaxCreditNumber = string.Join(",", input.NameValueList.Select(u => u.Name).ToArray());
            var query = _taxCreditUnitRepository.GetAll()
                .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null),
                    p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                .Where(u => u.IsActive)
                .Where(u => strTaxCreditNumber.Contains(u.Number))
                .Select(u => new { Name = u.Number, Value = u.Id.ToString() });

            var taxCreditList = await query
                .AsNoTracking()
                .ToListAsync();

            var result = (from jobNames in input.NameValueList
                          join taxCredit in taxCreditList on jobNames.Name equals taxCredit.Name
                              into taxCredit
                          from taxCredits in taxCredit.DefaultIfEmpty()
                          select new NameValueDto()
                          {
                              Name = jobNames.Name,
                              Value = taxCredits?.Value ?? ""
                          }).ToList();
            return result;
        }


        /// <summary>
        /// Get CheckGrouopList
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetCheckGroupList()
        {
            return EnumList.GetCheckGrouopList();
        }

        /// <summary>
        /// Get TypeOfInvoiceList
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeOfInvoiceList()
        {
            return EnumList.GetTypeOfInvoiceList();
        }

        /// <summary>
        /// Get PettyCashAccountList
        /// </summary>
        /// <returns></returns>
        public async Task<List<AutoFillDto>> GetPettyCashAccountList(AutoSearchInput input)
        {
            var query = from pcaccount in _pettyCashAccountUnitRepository.GetAll()
                        join account in _accountUnitRepository.GetAll() on pcaccount.AccountId equals account.Id
                        select new { pcaccount, Caption = account.Caption, Description = account.Description };
            var pcAccountList = await query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null),
                        p => p.pcaccount.OrganizationUnitId == input.OrganizationUnitId)
                        .WhereIf(!string.IsNullOrEmpty(input.Query),
                            p => p.Caption.Contains(input.Query) || p.Caption.Contains(input.Query))
                        .Select(u => new AutoFillDto
                        {
                            Name = u.Caption,
                            Value = u.pcaccount.Id.ToString(),
                            Column1 = u.Description,
                            Column2 = u.Caption
                        }).ToListAsync();
            return pcAccountList;
        }

        /// <summary>
        /// Get VendorPaymentTermsList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetVendorPaymentTermsList(AutoSearchInput input)
        {
            var paymentTermsList =await _vendorPaymentTermUnitRepository.GetAll().WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query) )
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString()}).ToListAsync();
            return paymentTermsList;
        }

        /// <summary>
        /// Get GetBankAccountList
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankAccountCacheItem>> GetBankAccountList(AutoSearchInput input)
        {
            var bankCacheItemList = await _bankAccountCache.GetBankAccountCacheItemAsync(CacheKeyStores.CalculateCacheKey(CacheKeyStores.BankAccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return bankCacheItemList.WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.BankAccountName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.BankAccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

    }


}

