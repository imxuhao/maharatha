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

namespace CAPS.CORPACCOUNTING.Accounts
{
    public class AccountUnitAppService : CORPACCOUNTINGAppServiceBase, IAccountUnitAppService
    {
        private readonly AccountUnitManager _accountUnitManager;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AccountUnitAppService(AccountUnitManager accountUnitManager, IRepository<AccountUnit, long> accountUnitRepository, UserManager userManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _accountUnitManager = accountUnitManager;
            _accountUnitRepository = accountUnitRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;

        }
        public async Task<ListResultOutput<AccountUnitDto>> GetAccountUnits(long? organizationUnitId)
        {
           
    
            var items =
                  from au in await _accountUnitRepository.GetAllListAsync(p => p.OrganizationUnitId == organizationUnitId)
                  select new { au, memberCount = au };

           
            return new ListResultOutput<AccountUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<AccountUnitDto>();
                    return dto;
                }).ToList());
        }
        public async Task<ListResultOutput<AccountUnitDto>> GetAccountUnitsByCoaId(IdInput coaId)
        {
            var query =
                _accountUnitRepository.GetAll()
                    .Where(au => au.ChartOfAccountId == coaId.Id)
                    .Select(au => new {au});

            var items = await query.ToListAsync();

            return new ListResultOutput<AccountUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<AccountUnitDto>();
                    return dto;
                }).ToList());
        }

        [UnitOfWork]
        public async Task<AccountUnitDto> CreateAccountUnit(CreateAccountUnitInput input)
        {
            var accountUnit = new AccountUnit(accountNumber: input.AccountNumber, caption: input.Caption,
                chartOfAccountId: input.ChartOfAccountId, parentId: input.ParentId,
                organizationunitid: input.OrganizationId,
                balanceSheetName: input.BalanceSheetName, cashFlowName: input.CashFlowName,
                description: input.Description, displaySequence: input.DisplaySequence, isActive: input.IsActive,
                isApproved: input.IsApproved, isBalanceSheet: input.IsBalanceSheet, isCashFlow: input.IsCashFlow,
                isDescriptionLocked: input.IsDescriptionLocked, isDocControlled: input.IsDocControlled,
                isElimination: input.IsElimination, isEnterable: input.IsEnterable, isProfitLoss: input.IsProfitLoss,
                isRollupAccount: input.IsRollupAccount, isRollupOverridable: input.IsRollupOverridable,
                isSummaryAccount: input.IsSummaryAccount, isUs1120BalanceSheet: input.IsUs1120BalanceSheet,
                isUs1120IncomeStmt: input.IsUs1120IncomeStmt, linkAccountId: input.LinkAccountId,
                linkJobId: input.LinkJobId, profitLossName: input.ProfitLossName, rollupAccountId: input.RollupAccountId,
                rollupJobId: input.RollupJobId, typeOfAccountId: input.TypeOfAccountId,
                us1120BalanceSheetName: input.Us1120BalanceSheetName, us1120IncomeStmtName: input.Us1120IncomeStmtName);

            await _accountUnitManager.CreateAsync(accountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) => { };

            return accountUnit.MapTo<AccountUnitDto>();
        }
        public async Task<AccountUnitDto> UpdateAccountUnit(UpdateAccountUnitInput input)
        {
            var accountUnit = await _accountUnitRepository.GetAsync(input.Id);

            #region Setting the values to be updated

            accountUnit.AccountNumber = input.AccountNumber;
            accountUnit.Caption = input.Caption;
            accountUnit.ChartOfAccountId = input.ChartOfAccountId;
            accountUnit.ParentId = input.ParentId;
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
        [UnitOfWork]
        public async Task DeleteAccountUnit(IdInput<long> input)
        {
            await _accountUnitManager.DeleteAsync(input.Id);
        }
    }
}
