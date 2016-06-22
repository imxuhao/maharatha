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
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Common;

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

        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager, IRepository<TypeOfAccountUnit, int> typeOfAccountRepository,
            IRepository<TypeOfCurrencyRateUnit, short> typeOfCurrencyRateRepository, IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyRepository,
            IRepository<CoaUnit, int> coaRepository)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _typeOfAccountRepository = typeOfAccountRepository;
            _typeOfCurrencyRateRepository = typeOfCurrencyRateRepository;
            _typeOfCurrencyRepository = typeOfCurrencyRepository;
            _coaRepository = coaRepository;
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
                    LinkAccount = linkAccounts.Caption

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

            var typeOfCurrency = await _typeOfCurrencyRepository.GetAll().Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return typeOfCurrency;
        }

        /// <summary>
        /// Get TypeOfAccount List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfAccountList()
        {
            var typeOfAccounts = await _typeOfAccountRepository.GetAll().Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return typeOfAccounts;
        }

        /// <summary>
        /// Get TypeOfCurrencyRate List
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTypeOfCurrencyRateList()
        {
            var typeOfCurrencyRates = await _typeOfCurrencyRateRepository.GetAll().Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
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
        /// Get GetRollupAccountsList 
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetRollupAccountsList(AutoSearchInput input)
        {
            return await (from au in _accountUnitRepository.GetAll()
                         .Where(p => p.IsRollupAccount == true && p.ChartOfAccountId == input.Id)
                         .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.Contains(input.Query))
                         .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId.Value)
                          select new NameValueDto { Name = au.Caption, Value = au.Id.ToString() }).ToListAsync();
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

    }
}
