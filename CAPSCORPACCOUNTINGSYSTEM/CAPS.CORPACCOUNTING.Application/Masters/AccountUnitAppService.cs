using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Authorization.Users;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using CAPS.CORPACCOUNTING.Helpers;
using System.Collections.Generic;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Runtime.Session;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Masters.CustomRepository;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Runtime.Caching;


namespace CAPS.CORPACCOUNTING.Accounts
{
    public class AccountUnitAppService : CORPACCOUNTINGAppServiceBase, IAccountUnitAppService
    {
        private readonly AccountUnitManager _accountUnitManager;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<TypeOfAccountUnit, int> _typeOfAccountRepository;
        private readonly IRepository<TypeOfCurrencyRateUnit, short> _typeOfCurrencyRateRepository;
        private readonly IRepository<TypeOfCurrencyUnit, short> _typeOfCurrencyRepository;
        private readonly IRepository<CoaUnit, int> _coaRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomAppSession _customAppSession;
        private readonly AccountCache _accountcache;
        private readonly ICustomAccountRepository _customAccountRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<AccountLinks, long> _accountLinkRepository;
        private readonly AccountLinksManager _accountLinksManager;

        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager, IRepository<TypeOfAccountUnit, int> typeOfAccountRepository,
            IRepository<TypeOfCurrencyRateUnit, short> typeOfCurrencyRateRepository, IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyRepository,
            IRepository<CoaUnit, int> coaRepository, AccountCache accountcache, CustomAppSession customAppSession, ICustomAccountRepository customAccountRepository, ICacheManager cacheManager, IRepository<AccountLinks, long> accountLinkRepository, AccountLinksManager accountLinksManager)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _typeOfAccountRepository = typeOfAccountRepository;
            _typeOfCurrencyRateRepository = typeOfCurrencyRateRepository;
            _typeOfCurrencyRepository = typeOfCurrencyRepository;
            _coaRepository = coaRepository;
            _accountcache = accountcache;
            _customAppSession = customAppSession;
            _customAccountRepository = customAccountRepository;
            _cacheManager = cacheManager;
            _accountLinkRepository = accountLinkRepository;
            _accountLinksManager = accountLinksManager;
        }

        /// <summary>
        /// Get the Accounts by CoaId with paging sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts)]
        public async Task<PagedResultOutput<AccountUnitDto>> GetAccountUnitsByCoaId(GetAccountInput input)
        {
            var query =
                from au in _accountUnitRepository.GetAll()
                join linkAu in _accountUnitRepository.GetAll() on au.LinkAccountId equals linkAu.Id
                into linkAccount
                from linkAccounts in linkAccount.DefaultIfEmpty()
                join typeOfAccount in _typeOfAccountRepository.GetAll() on au.TypeOfAccountId equals typeOfAccount.Id
                into accounts
                from typeofaccounts in accounts.DefaultIfEmpty()
                join currencytype in _typeOfCurrencyRepository.GetAll() on au.TypeOfCurrencyId equals currencytype.Id
                into currencyt
                from currency in currencyt.DefaultIfEmpty()
                join currencyrate in _typeOfCurrencyRateRepository.GetAll() on au.TypeOfCurrencyRateId equals currencyrate.Id
                into ratecurrency
                from accountresults in ratecurrency.DefaultIfEmpty()
                select new
                {
                    Account = au,
                    TypeOfAccount = typeofaccounts.Description,
                    TypeOfAccountRate = accountresults.Description,
                    TypeOfCurrency = currency.Description,
                    LinkAccount = linkAccounts.AccountNumber

                };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.Account.ChartOfAccountId == input.CoaId);


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Account.AccountNumber ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<AccountUnitDto>(resultCount, results.Select(item =>
                 {
                     var dto = item.Account.MapTo<AccountUnitDto>();
                     dto.AccountId = item.Account.Id;
                     dto.TypeofConsolidation = item.Account.TypeofConsolidationId != null ? item.Account.TypeofConsolidationId.ToDisplayName() : "";
                     dto.TypeOfAccount = item.TypeOfAccount;
                     dto.TypeOfCurrency = item.TypeOfCurrency;
                     dto.TypeOfCurrencyRate = item.TypeOfAccountRate;
                     dto.LinkAccount = item.LinkAccount;
                     return dto;
                 }).ToList());
        }

        /// <summary>
        /// Crating the AccountUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts_Create)]
        public async Task<IdOutputDto<long>> CreateAccountUnit(CreateAccountUnitInput input)
        {
            var accountUnit = input.MapTo<AccountUnit>();
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            IdOutputDto<long> responseDto = new IdOutputDto<long>
            {
                AccountId = await _accountUnitManager.CreateAsync(accountUnit)
            };
            await CurrentUnitOfWork.SaveChangesAsync();
            _unitOfWorkManager.Current.Completed += (sender, args) => { };
            return responseDto;
        }
        /// <summary>
        /// Updating the AccountUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts_Edit)]
        public async Task<IdOutputDto<long>> UpdateAccountUnit(UpdateAccountUnitInput input)
        {
            var accountUnit = await _accountUnitRepository.GetAsync(input.AccountId);

            #region Setting the values to be updated

            accountUnit.AccountNumber = input.AccountNumber;
            accountUnit.Caption = input.Caption;
            accountUnit.ChartOfAccountId = input.ChartOfAccountId;
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            accountUnit.OrganizationUnitId = input.OrganizationUnitId;
            accountUnit.BalanceSheetName = input.BalanceSheetName;
            accountUnit.CashFlowName = input.CashFlowName;
            accountUnit.Description = input.Description;
            accountUnit.IsActive = input.IsActive;
            accountUnit.IsApproved = input.IsApproved;
            accountUnit.IsBalanceSheet = input.IsBalanceSheet;
            accountUnit.IsCashFlow = input.IsCashFlow;
            accountUnit.IsDescriptionLocked = input.IsDescriptionLocked;
            accountUnit.IsDocControlled = input.IsDocControlled;
            accountUnit.IsElimination = input.IsElimination;
            accountUnit.IsEnterable = input.IsEnterable;
            accountUnit.IsProfitLoss = input.IsProfitLoss;
            accountUnit.IsRollupAccount = input.IsRollupAccount;
            accountUnit.IsRollupOverridable = input.IsRollupOverridable;
            accountUnit.IsSummaryAccount = input.IsSummaryAccount;
            accountUnit.IsUs1120BalanceSheet = input.IsUs1120BalanceSheet;
            accountUnit.IsUs1120IncomeStmt = input.IsUs1120IncomeStmt;
            accountUnit.LinkAccountId = input.LinkAccountId;
            accountUnit.LinkJobId = input.LinkJobId;
            accountUnit.ProfitLossName = input.ProfitLossName;
            accountUnit.RollupAccountId = input.RollupAccountId;
            accountUnit.IsAccountRevalued = input.IsAccountRevalued;
            accountUnit.TypeOfCurrencyId = input.TypeOfCurrencyId;
            accountUnit.TypeOfCurrencyRateId = input.TypeOfCurrencyRateId;
            accountUnit.TypeofConsolidationId = input.TypeofConsolidationId;
            accountUnit.RollupJobId = input.RollupJobId;
            accountUnit.TypeOfAccountId = input.TypeOfAccountId;
            accountUnit.Us1120BalanceSheetName = input.Us1120BalanceSheetName;
            accountUnit.DisplaySequence = input.DisplaySequence;
            accountUnit.Us1120IncomeStmtName = input.Us1120IncomeStmtName;
            #endregion

            await _accountUnitManager.UpdateAsync(accountUnit);

            await CurrentUnitOfWork.SaveChangesAsync();
            IdOutputDto<long> responseDto = new IdOutputDto<long>
            {
                AccountId = accountUnit.Id
            };
            return responseDto;
        }
        /// <summary>
        /// Deleting the Account by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts_Delete)]
        public async Task DeleteAccountUnit(IdInput<long> input)
        {
            await _accountUnitManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// Get the AccountDetails by AccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountUnitDto> GetAccountUnitsById(IdInput input)
        {
            AccountUnit accountUnit = await _accountUnitRepository.GetAsync(input.Id);
            AccountUnitDto result = accountUnit.MapTo<AccountUnitDto>();
            result.AccountId = accountUnit.Id;
            return result;

        }


        /// <summary>
        /// Get TypeofConsolidation
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofConsolidationList()
        {
            return EnumList.GetTypeofConsolidationList();
        }

        /// <summary>
        /// Get TypeOfCurrency
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfCurrencyList()
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var typeOfCurrency =
                    await
                        _typeOfCurrencyRepository.GetAll()
                            .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).OrderBy(p => p.Name)
                            .ToListAsync();
                return typeOfCurrency;
            }
        }

        /// <summary>
        /// Get TypeOfAccount List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfAccountList()
        {
            var typeOfAccounts = await _typeOfAccountRepository.GetAll()
                        .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                        .OrderBy(p => p.Name).ToListAsync();
            return typeOfAccounts;
        }

        /// <summary>
        /// Get TypeOfCurrencyRate List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfCurrencyRateList()
        {

            var typeOfCurrencyRates = await _typeOfCurrencyRateRepository.GetAll()
                        .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                        .OrderBy(p => p.Name).ToListAsync();
            return typeOfCurrencyRates;

        }
        /// <summary>
        /// Get LinkAccount List By CoaId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetLinkAccountListByCoaId(AutoSearchInput input)
        {
            var linkCoaId = _coaRepository.GetAll().Where(u => u.Id == input.Id).Select(u => u.LinkChartOfAccountID).FirstOrDefault();

            var linkAccountList = _accountUnitRepository.GetAll().Where(u => u.ChartOfAccountId == linkCoaId);
            if (!string.IsNullOrEmpty(input.Query))
                linkAccountList = linkAccountList.Where(u => u.Caption.Contains(input.Query));

            var result = await linkAccountList.Select(u => new NameValueDto { Name = u.Caption, Value = u.Id.ToString() })
             .OrderBy(p => p.Name).ToListAsync();
            return result;
        }
        /// <summary>
        /// Get new mapping accounts by Convert to new coa id
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Account units</returns>
        public async Task<List<AccountUnitDto>> GetAccountsForMapping(AutoSearchInput input)
        {
            var linkCoaId = _coaRepository.GetAll().Where(u => u.Id == input.Id).Select(u => u.LinkChartOfAccountID).FirstOrDefault();

            var linkAccountList = _accountUnitRepository.GetAll().Where(u => u.ChartOfAccountId == linkCoaId);
            if (!string.IsNullOrEmpty(input.Query))
                linkAccountList =
                    linkAccountList.Where(u => u.AccountNumber.Contains(input.Query) || u.Caption.Contains(input.Query));

            var result = await linkAccountList.Select(
                u => new AccountUnitDto { Caption = u.Caption, Description = u.Description, AccountNumber = u.AccountNumber, AccountId = u.Id, ChartOfAccountId = u.ChartOfAccountId }
                ).OrderBy(p => p.Caption).ToListAsync();
            return result;
        }

        /// <summary>
        /// Get CorporateRollupAccountsList 
        /// </summary>
        /// <returns></returns>
        public async Task<List<AccountCacheItem>> GetRollupAccountsList(AutoSearchInput input)
        {
            var accountList = await _accountcache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId)));

            return accountList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.IsCorporate && p.IsRollupAccount).ToList();

        }

        /// <summary>
        /// Get Account by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountUnitDto> GetAccountById(IdInput<long> input)
        {
            AccountUnit accountUnit = await _accountUnitRepository.GetAsync(input.Id);
            AccountUnitDto result = accountUnit.MapTo<AccountUnitDto>();
            result.AccountId = accountUnit.Id;
            return result;
        }
        /// <summary>
        /// Inserting BulkAccount
        /// </summary>
        /// <param name="listAccountUnitDtos"></param>
        /// <returns></returns>

        public async Task<List<AccountUnitDto>> BulkAccountInsert(CreateAccountListInput listAccountUnitDtos)
        {
            List<AccountUnit> accountList = new List<AccountUnit>();
            var createAccountList = listAccountUnitDtos.AccountList.Select((item, index) => { item.ExcelRowNumber = index; return item; }).ToList();

            //ErrorAccountList
            var erroredAccountList = await ValidateDuplicateRecords(createAccountList);

            //ValidAccountList
            var accounts = listAccountUnitDtos.AccountList.Where(p => erroredAccountList.All(p2 => p2.AccountNumber != p.AccountNumber)).ToList();
            int accountCode = Convert.ToInt32(await _accountUnitManager.GetNextChildCodeAsync(parentId: null, coaId: listAccountUnitDtos.AccountList[0].ChartOfAccountId));
            foreach (var accountUnit in accounts)
            {
                accountUnit.ParentId = accountUnit.ParentId != 0 ? accountUnit.ParentId : null;
                var account = accountUnit.MapTo<AccountUnit>();
                account.TenantId = AbpSession.GetTenantId();
                account.CreatorUserId = AbpSession.GetUserId();
                account.Code = AccountUnit.CreateCode(accountCode);
                accountList.Add(account);
                accountCode++;
            }
            if (accountList.Count > 0)
            {
                await _customAccountRepository.BulkInsertAccountUnits(accountList: accountList);
                _cacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheAccountStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(AbpSession.GetTenantId())));
            }
            return erroredAccountList;
        }

        /// <summary>
        /// Checking DuplicateRecords
        /// </summary>
        /// <param name="accountsList"></param>
        /// <returns></returns>
        private async Task<List<AccountUnitDto>> ValidateDuplicateRecords(List<CreateAccountUnitInput> accountsList)
        {
            var accountunitDtoList = new List<AccountUnitDto>();
            //making commaseperated string of AccountNumbers
            var accountNumberList = string.Join(",", accountsList.Select(p => p.AccountNumber).ToArray());
            //Get the Accounts from Cache
            var duplicateAccounts = await _accountcache.GetAccountCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId)));

            //filtering acconts with Corporate COA
            var duplicateAccountItems =
                duplicateAccounts.Where(p => p.ChartOfAccountId == accountsList[0].ChartOfAccountId).ToList();

            //Getting Duplicate AccountList
            var duplicateAccountList =
                duplicateAccountItems.Where(
                    p => accountNumberList.Contains(p.AccountNumber)).ToList();

            //Constructing Duplicate AccountEntity List with Error
            var duplicateAccountsAccountNumberList = (from p in accountsList
                                                      join p2 in duplicateAccountList on p.AccountNumber equals p2.AccountNumber
                                                      select new { account = p, ErrorMesage = L("DuplicateAccountNumber") + p.AccountNumber }).ToList();

            //Constructing OutPutDto with ErrorMessage
            foreach (var account in duplicateAccountsAccountNumberList)
            {
                var accountdto = account.account.MapTo<AccountUnit>().MapTo<AccountUnitDto>();
                accountdto.TypeOfAccount = account.account.TypeOfAccount;
                accountdto.TypeOfCurrency = account.account.TypeOfCurrency;
                accountdto.TypeofConsolidation = account.account.TypeofConsolidation;
                accountdto.TypeOfCurrencyRate = account.account.TypeOfCurrencyRate;
                accountdto.RollUpDivision = account.account.RollUpDivision;
                accountdto.RollUpAccountCaption = account.account.RollUpAccountCaption;
                accountdto.ErrorMessage = account.ErrorMesage.TrimEnd(',').TrimStart(',');
                accountunitDtoList.Add(accountdto);
            }
            return accountunitDtoList;
        }

        /// <summary>
        /// Get MappedAccounts By coaId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Accounts)]
        public async Task<PagedResultOutput<AccountUnitDto>> GetLinkedAccountUnitsByCoaId(GetAccountInput input)
        {
            //get Coa record by coaId
            var coa = await _coaRepository.FirstOrDefaultAsync(p => p.Id == input.CoaId);
            int? linkedcoaId = coa.LinkChartOfAccountID;
            
            //Get all linkedchartofaccount accounts.
            var query =
                from au in _accountUnitRepository.GetAll()
                join linkaccount in
                (from account in _accountUnitRepository.GetAll().Where(p => p.ChartOfAccountId == linkedcoaId)
                 join homeAc in _accountLinkRepository.GetAll() on account.Id equals homeAc.MapAccountId.Value
                 select new { account.AccountNumber, homeAc })
                on au.Id equals linkaccount.homeAc.HomeAccountId.Value
                into linkaccountunit
                from linkaccounts in linkaccountunit.DefaultIfEmpty()
                join typeOfAccount in _typeOfAccountRepository.GetAll() on au.TypeOfAccountId equals typeOfAccount.Id
                into accounts
                from typeofaccounts in accounts.DefaultIfEmpty()
                join currencytype in _typeOfCurrencyRepository.GetAll() on au.TypeOfCurrencyId equals currencytype.Id
                into currencyt
                from currency in currencyt.DefaultIfEmpty()
                join currencyrate in _typeOfCurrencyRateRepository.GetAll() on au.TypeOfCurrencyRateId equals currencyrate.Id
                into ratecurrency
                from accountresults in ratecurrency.DefaultIfEmpty()
                select new
                {
                    Account = au,
                    TypeOfAccount = typeofaccounts.Description,
                    TypeOfAccountRate = accountresults.Description,
                    TypeOfCurrency = currency.Description,
                    LinkAccount = linkaccounts.AccountNumber,
                    LinkedAccountUnit = linkaccounts.homeAc

                };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.Account.ChartOfAccountId == linkedcoaId);


            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Account.AccountNumber ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<AccountUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Account.MapTo<AccountUnitDto>();
                dto.AccountId = item.Account.Id;
                dto.TypeofConsolidation = item.Account.TypeofConsolidationId != null ? item.Account.TypeofConsolidationId.ToDisplayName() : "";
                dto.TypeOfAccount = item.TypeOfAccount;
                dto.TypeOfCurrency = item.TypeOfCurrency;
                dto.TypeOfCurrencyRate = item.TypeOfAccountRate;
                dto.LinkAccount = item.LinkAccount;
                dto.AccountLinkId = ReferenceEquals(item.LinkedAccountUnit, null)
                    ? (long?)null
                    : item.LinkedAccountUnit.Id;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// Get Accounts List By CoaId for autofills
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AccountCacheItem>> GetAccountListByCoaId(AutoSearchInput input)
        {
            var accountList = await _accountcache.GetAccountCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId)));
            return accountList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
            p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
            || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.ChartOfAccountId == input.Id).ToList();
        }

        /// <summary>
        /// Create or Update AccountLinks
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateOrUpdateAccountLinkUnit(CreateOrUpdateAccountLinkUnit input)
        {
            var accountUnit = input.MapTo<AccountLinks>();
            if (input.AccountLinkId == 0)
                await _accountLinksManager.CreateAsync(accountUnit);
            else
                await _accountLinksManager.UpdateAsync(accountUnit);

            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
