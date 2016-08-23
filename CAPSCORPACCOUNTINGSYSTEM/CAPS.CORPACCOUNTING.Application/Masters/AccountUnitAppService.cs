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
using System.Data;
using Abp.Authorization;
using Abp.Collections.Extensions;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Masters.CustomRepository;
using CAPS.CORPACCOUNTING.Sessions;
using Hangfire;

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
        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager, IRepository<TypeOfAccountUnit, int> typeOfAccountRepository,
            IRepository<TypeOfCurrencyRateUnit, short> typeOfCurrencyRateRepository, IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyRepository,
            IRepository<CoaUnit, int> coaRepository, AccountCache accountcache, CustomAppSession customAppSession, ICustomAccountRepository customAccountRepository)
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
                            .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
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
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var typeOfAccounts = await _typeOfAccountRepository.GetAll()
                            .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                            .ToListAsync();
                return typeOfAccounts;
            }
        }

        /// <summary>
        /// Get TypeOfCurrencyRate List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfCurrencyRateList()
        {

            var typeOfCurrencyRates = await _typeOfCurrencyRateRepository.GetAll()
                        .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() })
                        .ToListAsync();
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
             .ToListAsync();
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
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.IsCorporate == true && p.IsRollupAccount == true).ToList();

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
        /// Importing the Accounts From Excel
        /// </summary>
        /// <param name="accountTable"></param>
        /// <param name="coaId"></param>
        /// <returns></returns>
        public async Task<List<AccountUnitDto>> ImportAccounts(DataTable accountTable, int coaId)
        {
            var classificationlist = await GetTypeOfAccountList();
            var currencylist = await GetTypeOfCurrencyList();
            var consolidationList = EnumList.GetTypeofConsolidationList();
            var typeOfCurrencyRateList = await GetTypeOfCurrencyRateList();
            var linkedAccountList = await GetLinkAccountListByCoaId(new AutoSearchInput() { Id = coaId });

            var accountsList = (from tbl in accountTable.AsEnumerable()
                                join classification in classificationlist on tbl.Field<string>(L("Classification")) equals classification.Name
                                  into classifications
                                from classificationunit in classifications.DefaultIfEmpty()
                                join currency in currencylist on tbl.Field<string>(L("Currency")) equals currency.Name
                                         into currencies
                                from currencyunit in currencies.DefaultIfEmpty()
                                join consolidation in consolidationList on tbl.Field<string>(L("Consolidation")) equals consolidation.Name
                                         into consolidations
                                from consolidationunit in consolidations.DefaultIfEmpty()

                                join typeOfCurrencyRate in typeOfCurrencyRateList on tbl.Field<string>(L("RateTypeOverride")) equals typeOfCurrencyRate.Name
                                         into typeOfCurrencyRates
                                from typeOfCurrencyRateunit in typeOfCurrencyRates.DefaultIfEmpty()

                                join linkedAccount in linkedAccountList on tbl.Field<string>(L("NewAccount")) equals linkedAccount.Name
                                         into linkedAccounts
                                from linkedAccountunit in linkedAccounts.DefaultIfEmpty()
                                select new CreateAccountUnitInput
                                {
                                    ExcelRowNumber = Convert.ToInt32(tbl.Field<string>(L("No"))),
                                    Caption = string.IsNullOrEmpty(tbl.Field<string>(L("Description"))) ? string.Empty : tbl.Field<string>(L("Description")),
                                    TypeOfCurrencyId = ReferenceEquals(currencyunit, null) ? (short?)null : Convert.ToInt16(currencyunit.Value),
                                    TypeofConsolidationId = ReferenceEquals(consolidationunit, null) ? (TypeofConsolidation?)null : (TypeofConsolidation)Convert.ToInt32(consolidationunit.Value),
                                    LinkAccountId = ReferenceEquals(linkedAccountunit, null) ? (int?)null : Convert.ToInt32(linkedAccountunit.Value),
                                    TypeOfCurrencyRateId = ReferenceEquals(typeOfCurrencyRateunit, null) ? (short?)null : Convert.ToInt16(typeOfCurrencyRateunit.Value),
                                    IsEnterable = !string.IsNullOrEmpty(tbl.Field<string>(L("JournalsAllowed"))) && Helper.ConvertToBoolean(tbl.Field<string>(L("JournalsAllowed"))),
                                    ChartOfAccountId = coaId,
                                    AccountNumber = string.IsNullOrEmpty(tbl.Field<string>(L("AccountNumber"))) ? string.Empty : tbl.Field<string>(L("AccountNumber")),
                                    TypeOfAccountId = ReferenceEquals(classificationunit, null) ? (int?)null : Convert.ToInt32(classificationunit.Value),
                                    IsAccountRevalued = !string.IsNullOrEmpty(tbl.Field<string>(L("Multi-CurrencyReval"))) && Helper.ConvertToBoolean(tbl.Field<string>(L("Multi-CurrencyReval"))),
                                    IsElimination = !string.IsNullOrEmpty(tbl.Field<string>(L("EliminationAccount"))) && Helper.ConvertToBoolean(tbl.Field<string>(L("EliminationAccount"))),
                                    IsRollupAccount = !string.IsNullOrEmpty(tbl.Field<string>(L("RollUpAccount"))) && Helper.ConvertToBoolean(tbl.Field<string>(L("RollUpAccount")))
                                }).ToList();

            return await BulkAccountUploads(new CreateAccountListInput { AccountList = accountsList });
            
        }


        /// <summary>
        /// BulkAccount Import
        /// </summary>
        /// <param name="accountList"></param>
        /// <returns></returns>
        public async Task<List<AccountUnitDto>> BulkAccountUploads(CreateAccountListInput accountList)
        {
            List<AccountUnitDto> accountUnitList = new List<AccountUnitDto>();
            if (accountList.AccountList.Count > 0)
            {
                foreach (var account in accountList.AccountList)
                {
                    string erroeMsg = string.Empty;
                    var accountUnit = account.MapTo<AccountUnit>();
                    accountUnit.ParentId = account.ParentId != 0 ? account.ParentId : null;

                    ////Check RequiredField and MAxLenght Validations
                    string validationerroeMsg=ValidateUploadedData(account);
                    if (validationerroeMsg.Length <= 0)
                    {
                        _accountUnitManager.IsRecordfromExcel = true;
                            erroeMsg = await _accountUnitManager.CreateAccountListAsync(accountUnit);
                    }
                    if (erroeMsg.Length > 0 || validationerroeMsg.Length>0)
                    {
                        var accountUnitDto = accountUnit.MapTo<AccountUnitDto>();
                        accountUnitDto.ErrorMessage = (erroeMsg + ","+ validationerroeMsg).TrimStart(',').TrimEnd(',');
                        accountUnitList.Add(accountUnitDto);
                    }
                }
           }
            return accountUnitList;

        }

        public async Task<List<AccountUnitDto>> BulkAccountInsert(List<CreateAccountUnitInput> listAccountUnitDtos )
        {
            List<AccountUnit> accountList = new List<AccountUnit>();

            List<AccountUnitDto> erroredAccountList = new List<AccountUnitDto>();
            // Do your validation here and if validation is not successful remove the item from the List and put it into and another list
            foreach (var accountUnitDto in listAccountUnitDtos)
            {
                await _accountUnitManager.ValidateAccountUnitAsync(accountUnitDto.MapTo<AccountUnit>());
                
            }
            
            if (listAccountUnitDtos.Any())
            {
                
                accountList.AddRange(listAccountUnitDtos.Select(accountunitDto => accountunitDto.MapTo<AccountUnit>()));
            }
            
            await _customAccountRepository.BulkInsertAccountUnits(accountList: accountList);

            return erroredAccountList;

        }

        private string ValidateUploadedData(CreateAccountUnitInput acccountUnit)
        {
            string error = string.Empty;
            error= DataValidator.RequiredValidataion(acccountUnit.AccountNumber, L("AccountNumber"));
            error= error+","+ DataValidator.RequiredValidataion(acccountUnit.Caption, L("Description"));
            return error.TrimStart(',');
        }
    }
}
