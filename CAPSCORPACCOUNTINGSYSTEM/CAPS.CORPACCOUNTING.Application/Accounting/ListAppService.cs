using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Helpers;
using System.Data.Entity;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.PettyCash;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Organizations;
using Abp.Runtime.Session;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// 
    /// </summary>
    public class ListAppService : CORPACCOUNTINGAppServiceBase, IListAppService
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
        private readonly IRepository<PettyCashAccountUnit, long> _pettyCashAccountUnitRepository;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaymentTermUnitRepository;
        private readonly IBankAccountCache _bankAccountCache;
        private readonly IRepository<PurchaseOrderEntryDocumentUnit, long> _purchaseOrderEntryDocumentUnitRepository;
        private readonly IRepository<PurchaseOrderEntryDocumentDetailUnit, long> _purchaseOrderEntryDocumentDetailUnitRepository;


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
        /// <param name="subAccountRestrictionCache"></param>
        /// <param name="pettyCashAccountUnitRepository"></param>
        /// <param name="vendorPaymentTermUnitRepository"></param>
        /// <param name="bankAccountCache"></param>
        /// <param name="purchaseOrderEntryDocumentDetailUnitRepository"></param>
        /// <param name="purchaseOrderEntryDocumentUnitRepository"></param>
        public ListAppService(IRepository<SubAccountUnit, long> subAccountUnitRepository, IRepository<JobUnit> jobUnitRepository,
            CustomAppSession customAppSession, IRepository<AccountUnit, long> accountUnitRepository, ICacheManager cacheManager,
            IRepository<VendorUnit> vendorUnitRepository, IRepository<TaxCreditUnit> taxCreditUnitRepository, IVendorCache vendorCache,
            IDivisionCache dividsCache, IAccountCache accountCache, ISubAccountCache subAccountCache, IRepository<CoaUnit> coaUnit,
            ISubAccountRestrictionCache subAccountRestrictionCache,
            IRepository<PettyCashAccountUnit, long> pettyCashAccountUnitRepository,
            IRepository<VendorPaymentTermUnit> vendorPaymentTermUnitRepository,
            IBankAccountCache bankAccountCache,
            IRepository<PurchaseOrderEntryDocumentDetailUnit, long> purchaseOrderEntryDocumentDetailUnitRepository,
            IRepository<PurchaseOrderEntryDocumentUnit, long> purchaseOrderEntryDocumentUnitRepository)
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
            _subAccountRestrictionCache = subAccountRestrictionCache;
            _pettyCashAccountUnitRepository = pettyCashAccountUnitRepository;
            _vendorPaymentTermUnitRepository = vendorPaymentTermUnitRepository;
            _bankAccountCache = bankAccountCache;
            _purchaseOrderEntryDocumentDetailUnitRepository = purchaseOrderEntryDocumentDetailUnitRepository;
            _purchaseOrderEntryDocumentUnitRepository = purchaseOrderEntryDocumentUnitRepository;
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
            return cacheItem.ToList().Where(p => p.TypeOfJobStatusId != ProjectStatus.Closed).
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

            return accountList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.ChartOfAccountId == chartOfAccountId).ToList();


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
            if (input.AccountId == 0 || subaccountRestrictioncacheItem.Count == 0)
            {
                return
                    cacheItem.ToList()
                        .WhereIf(!string.IsNullOrEmpty(input.Query),
                            p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.SubAccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
                                 p.SearchNo.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
            }
            else
            {
                var res = from subaccount in cacheItem.ToList()
                          join subAccountRestriction in subaccountRestrictioncacheItem.Where(p => p.AccountId == input.AccountId)
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
                case "1099T4":
                    list = GetTypeof1099T4ListByNames(input);
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
                          join jobOrdivision in jobOrDivisionList.ToList() on jobNames.Name equals jobOrdivision.JobNumber
                          select new NameValueDto()
                          {
                              Name = jobNames.Name,
                              Value = jobOrdivision?.JobId.ToString() ?? ""
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
                          join account in accountList.ToList() on accNames.Name equals account.AccountNumber
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

            var subAccountRestrictionList = from subaccount in cacheItem.ToList()
                                            join subAccountRestriction in subAccountRestrictioncacheItem
                                            on subaccount.SubAccountId equals subAccountRestriction.SubAccountId
                                            select subaccount;

            var result = (from subaccountNumber in input.NameValueList
                          join subAccount in subAccountRestrictionList.ToList() on subaccountNumber.Name equals subAccount.SubAccountNumber
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
            var strVendorNames = input.NameValueList.Select(u => u.Name).ToArray();

            var vendorFirstNameList = vendorsList.ToList().Where(u => strVendorNames.Contains(u.FirstName))
            .Select(u => new NameValueDto()
            {
                Name = u.FirstName,
                Value = u.VendorId.ToString()
            }).ToList();

            var vendorLastNameList = vendorsList.ToList().Where(u => strVendorNames.Contains(u.LastName))
            .Select(u => new NameValueDto()
            {
                Name = u.LastName,
                Value = u.VendorId.ToString()
            }).ToList();

            var vendorList = vendorFirstNameList.Union(vendorLastNameList).DistinctBy(u => u.Name);
            var result = (from vendorNames in input.NameValueList
                          join vendors in vendorList on vendorNames.Name equals vendors.Name
                          select new NameValueDto()
                          {
                              Name = vendorNames.Name,
                              Value = vendors?.Value ?? ""
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

            var result = (from taxCreditNames in input.NameValueList
                          join taxCredit in taxCreditList on taxCreditNames.Name equals taxCredit.Name
                          select new NameValueDto()
                          {
                              Name = taxCreditNames.Name,
                              Value = taxCredit?.Value ?? ""
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Get Typeof1099T4 List by using Numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<NameValueDto> GetTypeof1099T4ListByNames(NameValueInputList input)
        {
            var result =
                (from value in input.NameValueList
                 join typeof1099T4 in EnumList.GetTypeof1099T4List() on value.Name equals typeof1099T4.Name
                 select new NameValueDto()
                 {
                     Name = value.Name,
                     Value = typeof1099T4?.Value ?? ""
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
            var paymentTermsList = await _vendorPaymentTermUnitRepository.GetAll().WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return paymentTermsList;
        }

        /// <summary>
        /// Get BankAccountList
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankAccountCacheItem>> GetBankAccountList(AutoSearchInput input)
        {

            var bankCacheItemList = await _bankAccountCache.GetBankAccountCacheItemAsync(CacheKeyStores.CalculateCacheKey(CacheKeyStores.BankAccountKey, Convert.ToInt32(_customAppSession.TenantId), input.OrganizationUnitId), input);
            return bankCacheItemList.WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.BankAccountName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.BankAccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

        /// <summary>
        ///Get PurchaseOrders
        /// </summary>
        /// <returns></returns>
        public async Task<List<PurchaseOrderEntyDocumnetwithDetailOutputDto>> GetPurchaseOrderList(GetPurchaseOrderInput input)
        {
            var purchaseOrderReferences = !string.IsNullOrEmpty(input.PurchaseOrderReferences) ? input.PurchaseOrderReferences.Split(',') : null;

            var query = from pounit in _purchaseOrderEntryDocumentUnitRepository.GetAll()
                        join podetails in _purchaseOrderEntryDocumentDetailUnitRepository.GetAll() on pounit.Id equals podetails.AccountingDocumentId.Value
                        select new
                        {
                            Isclosed = pounit.IsClose,
                            Description = pounit.Description,
                            DocumentReference = pounit.DocumentReference,
                            DocumentDate = pounit.DocumentDate,
                            podetails
                        };
            var results = await query.Where(p => p.podetails.Amount > 0 && p.Isclosed != true)
                .WhereIf(!string.IsNullOrEmpty(input.PurchaseOrderReferences), u => purchaseOrderReferences.Contains(u.DocumentReference))
                .WhereIf(input.VendorId != 0, u => u.podetails.VendorId == input.VendorId).ToListAsync();

            return results.Select(item =>
            {
                var dto = item.podetails.MapTo<PurchaseOrderEntyDocumnetwithDetailOutputDto>();
                dto.AccountId = item.podetails.Id;
                dto.Description = item.Description;
                dto.DocumentDate = item.DocumentDate;
                dto.DocumentReference = item.DocumentReference;
                return dto;
            }).ToList();

        }

        /// <summary>
        /// Get CheckType List
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetCheckTypeList()
        {
            return EnumList.GetCheckTypeList();
        }

       private List<OrganizationUnit> GetUserEntityList(string entityType)
        {
            var userId = AbpSession.UserId;
            var entityList = new List<OrganizationUnit>();
            switch (entityType)
            {
                case "Account":

                    break;
            }

            return entityList;
        }



    }


}

