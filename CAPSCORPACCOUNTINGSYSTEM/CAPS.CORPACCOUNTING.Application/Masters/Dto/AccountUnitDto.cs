using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Castle.Core;


namespace CAPS.CORPACCOUNTING.Masters.Dto
{
   
    [AutoMapFrom(typeof(AccountUnit))]
    public class AccountUnitDto :IOutputDto
    {
        /// <summary>Gets or sets the ParentId field. </summary>
        public long? ParentId { get; set; }

        /// <summary>Gets or sets the AccountNumber field. </summary>
        public string AccountNumber { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public long AccountId { get; set; }
       
        /// <summary>Gets or sets the Caption field. </summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the ChartOfAccountId field. </summary>
        public int ChartOfAccountId { get; set; }
      
        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual int DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsAccountRevalued field. </summary>
        public bool IsAccountRevalued { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; } = true;
        
        /// <summary>Gets or sets the IsDescriptionLocked field. </summary>
        public bool IsDescriptionLocked { get; set; }

        /// <summary>Gets or sets the IsElimination field. </summary>
        public bool IsElimination { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool IsEnterable { get; set; } = true;

        /// <summary>Gets or sets the IsRollupAccount field. </summary>
        public bool IsRollupAccount { get; set; }

        /// <summary>Gets or sets the IsRollupOverridable field. </summary>
        public bool IsRollupOverridable { get; set; }

        /// <summary>Gets or sets the LinkAccountId field. </summary>
        public int? LinkAccountId { get; set; }

        /// <summary>Gets or sets the LinkJobId field. </summary>
        public int? LinkJobId { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public int? RollupAccountId { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public int? RollupJobId { get; set; }

        /// <summary>Gets or sets the TypeOfAccountId field. </summary>
        public int? TypeOfAccountId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyId field. </summary>
        public bool IsDocControlled { get; set; }

        /// <summary>Gets or sets the IsSummaryAccount field. </summary>
        public bool IsSummaryAccount { get; set; }

        /// <summary>Gets or sets the IsBalanceSheet field. </summary>
        public bool IsBalanceSheet { get; set; }

        /// <summary>Gets or sets the IsUs1120BalanceSheet field. </summary>
        public bool IsUs1120BalanceSheet { get; set; }

        /// <summary>Gets or sets the IsProfitLoss field. </summary>
        public bool IsProfitLoss { get; set; }

        /// <summary>Gets or sets the IsUs1120IncomeStmt field. </summary>
        public bool IsUs1120IncomeStmt { get; set; }

        /// <summary>Gets or sets the IsCashFlow field. </summary>
        public bool IsCashFlow { get; set; }

        /// <summary>Gets or sets the CashFlowName field. </summary>
        public string CashFlowName { get; set; }

        /// <summary>Gets or sets the Us1120BalanceSheetName field. </summary>
        public string Us1120BalanceSheetName { get; set; }

        /// <summary>Gets or sets the BalanceSheetName field. </summary>
        public string BalanceSheetName { get; set; }

        /// <summary>Gets or sets the ProfitLossName field. </summary>
        public string ProfitLossName { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the Us1120IncomeStmtName field. </summary>
        public string Us1120IncomeStmtName { get; set; }

        /// <summary>Gets or sets the OrganizationId field. </summary>
        public long? OrganizationId { get; set; }

       
    }
}
