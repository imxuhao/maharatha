﻿using System.Data.Entity;
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
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Common;

namespace CAPS.CORPACCOUNTING.Accounts
{
    public class AccountUnitAppService : CORPACCOUNTINGAppServiceBase, IAccountUnitAppService
    {
        private readonly AccountUnitManager _accountUnitManager;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<TypeOfAccountUnit, int> _typeOfAccountRepository;
        private readonly IRepository<TypeOfCurrencyRateUnit, short> _typeOfCurrencyRateRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository,
            UserManager userManager, IUnitOfWorkManager unitOfWorkManager, IRepository<TypeOfAccountUnit, int> typeOfAccountRepository,
            IRepository<TypeOfCurrencyRateUnit, short> typeOfCurrencyRateRepository)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _typeOfAccountRepository = typeOfAccountRepository;
            _typeOfCurrencyRateRepository = typeOfCurrencyRateRepository;
        }
        public async Task<ListResultOutput<AccountUnitDto>> GetAccountUnits(long? organizationUnitId = null)
        {
            var items =
                  from au in await _accountUnitRepository.GetAllListAsync()
                  where organizationUnitId == null || au.OrganizationUnitId == organizationUnitId
                  select new { au, memberCount = au };

            return new ListResultOutput<AccountUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<AccountUnitDto>();
                    dto.AccountId = item.au.Id;
                    return dto;
                }).ToList());
        }
        /// <summary>
        /// Get the Records by CoaId with paging sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<AccountUnitDto>> GetAccountUnitsByCoaId(GetAccountInput input)
        {
            var query =
                from au in _accountUnitRepository.GetAll()
                join typeOfAccount in _typeOfAccountRepository.GetAll() on au.TypeOfAccountId equals typeOfAccount.Id
                into accounts
                from coaunit in accounts.DefaultIfEmpty()
                join currencyrate in _typeOfCurrencyRateRepository.GetAll() on au.TypeOfCurrencyRateId equals currencyrate.Id
                into ratecurrency 
                from accountresults in ratecurrency.DefaultIfEmpty()
                select new { Account = au, TypeOfAccount= coaunit.Description, TypeOfAccountRate=accountresults.Description };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item =>item.Account.ChartOfAccountId==input.CoaId && (item.Account.OrganizationUnitId == input.OrganizationUnitId || item.Account.OrganizationUnitId == null));


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
                     dto.TypeofConsolidation = item.Account.TypeofConsolidationId != null ? item.Account.TypeofConsolidationId.ToDisplayName():"";
                     dto.TypeOfAccount = item.TypeOfAccount;
                     dto.TypeOfCurrency= item.Account.TypeOfCurrencyId != null ? item.Account.TypeOfCurrencyId.ToDisplayName() : "";
                     dto.TypeOfCurrencyRate = item.TypeOfAccountRate;
                     return dto;
                 }).ToList());
        }
       
        /// <summary>
        /// Crating the AccountUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<AccountUnitDto> CreateAccountUnit(CreateAccountUnitInput input)
        {
            var accountUnit = input.MapTo<AccountUnit>();
            accountUnit.ParentId = input.ParentId != 0 ? input.ParentId : null;
            await _accountUnitManager.CreateAsync(accountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            _unitOfWorkManager.Current.Completed += (sender, args) => { };
            return accountUnit.MapTo<AccountUnitDto>();
        }
        /// <summary>
        /// Updating the AccountUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AccountUnitDto> UpdateAccountUnit(UpdateAccountUnitInput input)
        {
            var accountUnit = await _accountUnitRepository.GetAsync(input.AccountId);

            #region Setting the values to be updated

            accountUnit.AccountNumber = input.AccountNumber;
            accountUnit.Caption = input.Caption;
            accountUnit.ChartOfAccountId = input.ChartOfAccountId;
            accountUnit.ParentId = input.ParentId!=0? input.ParentId:null;
            accountUnit.OrganizationUnitId = input.OrganizationId;
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

            return accountUnit.MapTo<AccountUnitDto>();
        }
        /// <summary>
        /// Deleting the Account by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
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
        public List<NameValueDto> GetTypeOfCurrencyList()
        {
            return EnumList.GetTypeOfCurrencyList();
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

    }
}
