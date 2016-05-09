using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Extensions;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///Enum for TypeOfConsolidation 
    /// </summary>
    public enum TypeofConsolidation
    {
        [Display(Name = "Date")]
        Date = 1,
        [Display(Name = "Month")]
        Month = 2,
        [Display(Name = "Year")]
        Year = 3,
        [Display(Name = "Voucher Reference")]
        VoucherReference = 4
    }
    ///// <summary>
    /////Enum for TypeOfConsolidation 
    ///// </summary>
    //public enum TypeOfCurrency
    //{
    //    [Display(Name = "US Dollar")]
    //    USDollar = 1,
    //    [Display(Name = "Canadian Dollar")]
    //    CanadianDollar = 2
    //}
    
    /// <summary>
    /// Account is the table name in lajit
    /// </summary>
    [Table("CAPS_Account")]
    public class AccountUnit : FullAuditedEntity<long>, IMustHaveTenant, IMustHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum length of the <see cref="Caption" /> property.
        /// </summary>
        public const int MaxDisplayNameLength = 128;

        /// <summary>
        ///     Maximum size of an Account Number.
        /// </summary>
        public const int MaxAccountSize = 10;

        /// <summary>
        ///     Maximum size of Description.
        /// </summary>
        public const int MaxDesc = 400;

        /// <summary>
        ///     Maximum depth of an Account hierarchy.
        /// </summary>
        public const int MaxDepth = 5;

        /// <summary>
        ///     Length of a code unit between dots.
        /// </summary>
        public const int CodeUnitLength = 5;

        /// <summary>
        ///     Maximum length of the <see cref="Code" /> property.
        /// </summary>
        public const int MaxCodeLength = MaxDepth*(CodeUnitLength + 1) - 1;


        /// <summary>
        ///     Children of this OU.
        /// </summary>
        public ICollection<AccountUnit> Children { get; set; }

        /// <summary>
        ///     Creates code for given numbers.
        ///     Example: if numbers are 4,2 then returns "00004.00002";
        /// </summary>
        /// <param name="numbers">Numbers</param>
        public static string CreateCode(params int[] numbers)
        {
            if (numbers.IsNullOrEmpty())
            {
                return null;
            }

            return numbers.Select(number => number.ToString(new string('0', CodeUnitLength))).JoinAsString(".");
        }

        /// <summary>
        ///     Appends a child code to a parent code.
        ///     Example: if parentCode = "00001", childCode = "00042" then returns "00001.00042".
        /// </summary>
        /// <param name="parentCode">Parent code. Can be null or empty if parent is a root.</param>
        /// <param name="childCode">Child code.</param>
        public static string AppendCode(string parentCode, string childCode)
        {
            if (childCode.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(childCode), "childCode can not be null or empty.");
            }

            if (parentCode.IsNullOrEmpty())
            {
                return childCode;
            }

            return parentCode + "." + childCode;
        }

        /// <summary>
        ///     Gets relative code to the parent.
        ///     Example: if code = "00019.00055.00001" and parentCode = "00019" then returns "00055.00001".
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="parentCode">The parent code.</param>
        public static string GetRelativeCode(string code, string parentCode)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            if (parentCode.IsNullOrEmpty())
            {
                return code;
            }

            if (code.Length == parentCode.Length)
            {
                return null;
            }

            return code.Substring(parentCode.Length + 1);
        }

        /// <summary>
        ///     Calculates next code for given code.
        ///     Example: if code = "00019.00055.00001" returns "00019.00055.00002".
        /// </summary>
        /// <param name="code">The code.</param>
        public static string CalculateNextCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            var parentCode = GetParentCode(code);
            var lastUnitCode = GetLastUnitCode(code);

            return AppendCode(parentCode, CreateCode(Convert.ToInt32(lastUnitCode) + 1));
        }

        /// <summary>
        ///     Gets the last unit code.
        ///     Example: if code = "00019.00055.00001" returns "00001".
        /// </summary>
        /// <param name="code">The code.</param>
        public static string GetLastUnitCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            var splittedCode = code.Split('.');
            return splittedCode[splittedCode.Length - 1];
        }

        /// <summary>
        ///     Gets parent code.
        ///     Example: if code = "00019.00055.00001" returns "00019.00055".
        /// </summary>
        /// <param name="code">The code.</param>
        public static string GetParentCode(string code)
        {
            if (code.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(code), "code can not be null or empty.");
            }

            var splittedCode = code.Split('.');
            if (splittedCode.Length == 1)
            {
                return null;
            }

            return splittedCode.Take(splittedCode.Length - 1).JoinAsString(".");
        }

        #region Class Property Declarations

        /// <summary>Gets or sets the AccountId field. </summary>
        [Column("AccountId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public long? LajitId { get; set; }

        [Required]
        [StringLength(MaxAccountSize)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(MaxDisplayNameLength)]
        public string Caption { get; set; }

        [MaxLength(MaxDesc)]
        public string Description { get; set; }


        [Required]
        public int ChartOfAccountId { get; set; }

        [ForeignKey("ChartOfAccountId")]
        public CoaUnit Chartofaccounts { get; set; }

        public int? TypeOfAccountId { get; set; }


        [ForeignKey("ParentId")]
        public AccountUnit Parent { get; set; }

        /// <summary>
        ///     Parent <see cref="AccountUnit" /> Id.
        ///     Null, if this AcountUnit is root.
        /// </summary>
        public long? ParentId { get; set; }


        // <summary>Gets or sets the TypeOfCurrencyId field. </summary>

        public short? TypeOfCurrencyId { get; set; }

        /// <summary>
        ///     Hierarchical Code of this organization unit.
        ///     Example: "00001.00042.00005".
        ///     This is a unique code for a Tenant.
        ///     It's changeable if Account hierarch is changed.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public string Code { get; set; }


        public int? DisplaySequence { get; set; }


        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsDescriptionLocked field. </summary>
        public bool IsDescriptionLocked { get; set; }

        /// <summary>Gets or sets the IsElimination field. </summary>
        public bool IsElimination { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool IsEnterable { get; set; }

        /// <summary>Gets or sets the IsRollupAccount field. </summary>
        public bool IsRollupAccount { get; set; }

        /// <summary>Gets or sets the IsRollupOverridable field. </summary>
        public bool IsRollupOverridable { get; set; }

        /// <summary>Gets or sets the LinkAccountId field. </summary>
        public long? LinkAccountId { get; set; }

        /// <summary>Gets or sets the LinkJobId field. </summary>
        public int? LinkJobId { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public long? RollupAccountId { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public int? RollupJobId { get; set; }

        /// <summary>Gets or sets the RowStamp field. </summary>
        /// <summary>Gets or sets the IsElimination field. </summary>
        /// <summary>if yes, then it is not possible to post manually to this account</summary>
        public bool IsDocControlled { get; set; }


        /// <summary>
        ///     No entries can be posted to this account, this is a summary of the subaccounts. All entries have to be
        ///     submitted to the subaccounts
        /// </summary>
        public bool IsSummaryAccount { get; set; }


        /// <summary>If this item appears in the balance sheet report - it's identifier</summary>
        public bool IsBalanceSheet { get; set; }


        /// <summary>
        ///     If this item appears in the balance sheet report - it's name as displayed in the report
        /// </summary>
        [MaxLength(MaxDisplayNameLength)]
        public string BalanceSheetName { get; set; }

        /// <summary>If this item appears in the form 1120 (US tax balance sheet) report - it's identifier</summary>
        public bool IsUs1120BalanceSheet { get; set; }

        /// <summary>
        ///     If this item appears in the form 1120 (US tax balance sheet) report - it's name as displayed in the report
        /// </summary>
        [MaxLength(MaxDisplayNameLength)]
        public string Us1120BalanceSheetName { get; set; }


        /// <summary>
        ///     If this item appears in the profit and loss report - it's identifie
        /// </summary>
        public bool IsProfitLoss { get; set; }


        /// <summary>
        ///     If this item appears in the profit and loss report - it's name as displayed in the report
        /// </summary>
        [MaxLength(MaxDisplayNameLength)]
        public string ProfitLossName { get; set; }


        /// <summary>
        ///     US 1120 Income Stmt If this item appears in the form 1120 (US tax profit and loss) report - it's identifier
        /// </summary>
        public bool IsUs1120IncomeStmt { get; set; }


        /// <summary>
        ///     If this item appears in the form 1120 (US tax profit and loss) report - it's name as displayed in the report
        /// </summary>
        [MaxLength(MaxDisplayNameLength)]
        public string Us1120IncomeStmtName { get; set; }


        /// <summary>
        ///     If this item appears in the cash flow report - it's identifier
        /// </summary>
        public bool IsCashFlow { get; set; }

        /// <summary>
        ///     If this item appears in the cash flow report - it's name as displayed in the report
        /// </summary>
        [MaxLength(MaxDisplayNameLength)]
        public string CashFlowName { get; set; }

        /// <summary>Gets or sets the TypeofConsolidationId field. </summary>
        public TypeofConsolidation? TypeofConsolidationId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateId field. </summary>
        public short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Gets or sets the IsAccountRevalued field. </summary>
        public bool IsAccountRevalued { get; set; }
        
        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long OrganizationUnitId { get; set; }

        public bool IsActive { get; set; }

        #endregion

        #region Constructor Initialization

        public AccountUnit()
        {
        }

        public AccountUnit(string accountNumber, string balanceSheetName, string caption, string cashFlowName,
            int chartOfAccountId,  string description, int? displaySequence, bool isActive,
            bool isApproved, bool isBalanceSheet, bool isCashFlow, bool isDescriptionLocked, bool isDocControlled,
            bool isElimination, bool isEnterable, bool isProfitLoss, bool isRollupAccount, bool isRollupOverridable,
            bool isSummaryAccount, bool isUs1120BalanceSheet, bool isUs1120IncomeStmt, int? linkAccountId,
            int? linkJobId, long? parentId, string profitLossName, long? rollupAccountId, int? rollupJobId,
            int? typeOfAccountId, string us1120BalanceSheetName, string us1120IncomeStmtName, long organizationunitid)
        {
            if (chartOfAccountId <= 0) throw new ArgumentOutOfRangeException(nameof(chartOfAccountId));
            AccountNumber = accountNumber;
            BalanceSheetName = balanceSheetName;
            Caption = caption;
            CashFlowName = cashFlowName;
            ChartOfAccountId = chartOfAccountId;
            Description = description;
            DisplaySequence = displaySequence;
            IsActive = isActive;
            IsApproved = isApproved;
            IsBalanceSheet = isBalanceSheet;
            IsCashFlow = isCashFlow;
            IsDescriptionLocked = isDescriptionLocked;
            IsDocControlled = isDocControlled;
            IsElimination = isElimination;
            IsEnterable = isEnterable;
            IsProfitLoss = isProfitLoss;
            IsRollupAccount = isRollupAccount;
            IsRollupOverridable = isRollupOverridable;
            IsSummaryAccount = isSummaryAccount;
            IsUs1120BalanceSheet = isUs1120BalanceSheet;
            IsUs1120IncomeStmt = isUs1120IncomeStmt;
            LinkAccountId = linkAccountId;
            LinkJobId = linkJobId;
            ParentId = parentId;
            ProfitLossName = profitLossName;
            RollupAccountId = rollupAccountId;
            RollupJobId = rollupJobId;
            TypeOfAccountId = typeOfAccountId;
            Us1120BalanceSheetName = us1120BalanceSheetName;
            Us1120IncomeStmtName = us1120IncomeStmtName;
            OrganizationUnitId = organizationunitid;
        }
        #endregion
    }
}