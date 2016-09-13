using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.EFAuditLog;
using Abp.Events.Bus;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// Enum for TypeOfAmount
    /// </summary>
    public enum TypeOfAmount
    {

        [Display(Name = "Standard Entry")]
        StandardEntry = 1,
        [Display(Name = "A/P Clearing")]
        APClearing = 2,
        [Display(Name = "A/P Discount")]
        APDiscount = 3,
        [Display(Name = "A/R Clearing")]
        ARClearing = 4,
        [Display(Name = "A/R Discount")]
        ARDiscount = 5,
        [Display(Name = "P/R Clearing")]
        PRClearing = 6,
        [Display(Name = "Autobalance")]
        Autobalance = 7,
        [Display(Name = "Credit Card Clearing")]
        CreditCardClearing = 8,
        [Display(Name = "P/R Straight Time")]
        PRStraightTime = 9,
        [Display(Name = "P/R Overtime")]
        PROvertime = 10,
        [Display(Name = "P/R Tax")]
        PRTax = 11,
        [Display(Name = "P/R Work Comp")]
        PRWorkComp = 12,
        [Display(Name = "Petty Cash Offset")]
        PettyCashOffset = 13,
        [Display(Name = "Cash Offset")]
        CashOffset = 14,
        [Display(Name = "Year End Closing")]
        YearEndClosing = 15,
        [Display(Name = "Use AICP Line Total")]
        UseAICPLineTotal = 16,
        [Display(Name = "Use AICP Group Total")]
        UseAICPGroupTotal = 17,
        [Display(Name = "Budget")]
        Budget = 18,
        [Display(Name = "Producer Actual")]
        ProducerActual = 19,
        [Display(Name = "Flat Amount")]
        FlatAmount = 20,
        [Display(Name = "YTD Total")]
        YTDTotal = 21,
        [Display(Name = "A to K Expense")]
        AtoKExpense = 22,
        [Display(Name = "A to K Overage")]
        AtoKOverage = 23,
        [Display(Name = "Production Expense")]
        ProductionExpense = 24,
        [Display(Name = "Production Fee Overage")]
        ProductionFeeOverage = 25,
        [Display(Name = "Production Fee")]
        ProductionFee = 26,
        [Display(Name = "Director Fee")]
        DirectorFee = 27,
        [Display(Name = "Other Expense")]
        OtherExpense = 28,
        [Display(Name = "Overhead Expense")]
        OverheadExpense = 29,
        [Display(Name = "Revenue")]
        Revenue = 30,
        [Display(Name = "Other Revenue")]
        OtherRevenue = 31,
        [Display(Name = "Include Values")]
        IncludeValues = 32,
        [Display(Name = "Exclude Values")]
        ExcludeValues = 33,
        [Display(Name = "Gross Profit")]
        GrossProfit = 34,
        [Display(Name = "Net Profit")]
        NetProfit = 35
    }

    public enum CheckType
    {
        [Display(Name = "Deposit Check")]
        DepositCheck = 1,
        [Display(Name = "Payment Check")]
        PaymentCheck = 2

    }

    public enum SourceType
    {
        [Display(Name = "AP")]
        AP = 1,
        [Display(Name = "CC")]
        CC = 2,
        [Display(Name = "PC")]
        PC = 3,
        [Display(Name = "MC")]
        MC = 4,
        [Display(Name = "JE")]
        JE = 5,
        [Display(Name = "PO")]
        PO = 6
    }

    /// <summary>
    /// AccountingItem is the table name in lajit
    /// </summary>
    [Table("CAPS_AccountingItem")]
    public class AccountingItemUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit, INeedModLog
    {

        #region Class Property Declarations

        /// <summary>Overriding the Id column with AccountingItemId </summary>
        [Column("AccountingItemId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the AccountingDocumentID field. </summary>
        public virtual long? AccountingDocumentId { get; set; }

        [ForeignKey("AccountingDocumentId")]
        public virtual AccountingHeaderTransactionsUnit AccountingHeaderTransaction { get; set; }

        /// <summary>Gets or sets the TypeOfTransactionID field. </summary>
        public virtual TypeofTransaction TypeOfTransactionId { get; set; }

        /// <summary>Gets or sets the TypeOfAmountID field. </summary>
        public virtual TypeOfAmount TypeOfAmountId { get; set; }

        /// <summary>Gets or sets the ItemLinkID field. </summary>
        public virtual long? ItemLinkId { get; set; }

        /// <summary>Gets or sets the LedgerReference field. </summary>       
        public virtual string LedgerReference { get; set; }

        /// <summary>Gets or sets the LedgerDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? LedgerDate { get; set; }

        /// <summary>Gets or sets the LedgerYYYYMM field. </summary>
        public virtual int? LedgerYyyymm { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the ItemMemo field. </summary>   
        public virtual string ItemMemo { get; set; }

        /// <summary>Gets or sets the IsAsset field. </summary>
        public virtual bool IsAsset { get; set; }

        /// <summary>Gets or sets the AccountRef1 field. </summary>        
        public virtual string AccountRef1 { get; set; }

        /// <summary>Gets or sets the AccountRef2 field. </summary>      
        public virtual string AccountRef2 { get; set; }

        /// <summary>Gets or sets the AccountRef3 field.
        /// AccountRef3 referred as Invoice Ref
        ///</summary>
        public virtual string AccountRef3 { get; set; }

        /// <summary>Gets or sets the AccountRef4 field. </summary>       
        public virtual string AccountRef4 { get; set; }

        /// <summary>Gets or sets the AccountRef5 field. </summary>       
        public virtual string AccountRef5 { get; set; }

        /// <summary>Gets or sets the AccountRef6 field. </summary>      
        public virtual string AccountRef6 { get; set; }

        /// <summary>Gets or sets the AccountRef7 field. </summary>       
        public virtual string AccountRef7 { get; set; }

        /// <summary>Gets or sets the AccountRef8 field. </summary>      
        public virtual string AccountRef8 { get; set; }

        /// <summary>Gets or sets the AccountRef9 field. </summary>        
        public virtual string AccountRef9 { get; set; }

        /// <summary>Gets or sets the AccountRef10 field. </summary>        
        public virtual string AccountRef10 { get; set; }

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public virtual long? SubAccountId1 { get; set; }

        [ForeignKey("SubAccountId1")]
        public virtual SubAccountUnit SubAccount1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public virtual long? SubAccountId2 { get; set; }

        [ForeignKey("SubAccountId2")]
        public virtual SubAccountUnit SubAccount2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public virtual long? SubAccountId3 { get; set; }

        [ForeignKey("SubAccountId3")]
        public virtual SubAccountUnit SubAccount3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field.
        /// SubAccountID4 referred as Locations
        /// </summary>
        public virtual long? SubAccountId4 { get; set; }

        [ForeignKey("SubAccountId4")]
        public virtual SubAccountUnit SubAccount4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. 
        /// SubAccountID4 referred as Sets
        /// </summary>
        public virtual long? SubAccountId5 { get; set; }

        [ForeignKey("SubAccountId5")]
        public virtual SubAccountUnit SubAccount5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public virtual long? SubAccountId6 { get; set; }

        [ForeignKey("SubAccountId6")]
        public virtual SubAccountUnit SubAccount6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public virtual long? SubAccountId7 { get; set; }

        [ForeignKey("SubAccountId7")]
        public virtual SubAccountUnit SubAccount7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public virtual long? SubAccountId8 { get; set; }

        [ForeignKey("SubAccountId8")]
        public virtual SubAccountUnit SubAccount8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public virtual long? SubAccountId9 { get; set; }

        [ForeignKey("SubAccountId9")]
        public virtual SubAccountUnit SubAccount9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public virtual long? SubAccountId10 { get; set; }

        [ForeignKey("SubAccountId10")]
        public virtual SubAccountUnit SubAccount10 { get; set; }

        /// <summary>Gets or sets the TypeOf1099T4ID field. </summary>
        public virtual Typeof1099T4? TypeOf1099T4Id { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>Gets or sets the CompanyCurrencyAmount field. </summary>
        public virtual decimal? CompanyCurrencyAmount { get; set; }

        /// <summary>Gets or sets the CurrencyAdjustmentAmount field. </summary>
        public virtual decimal? CurrencyAdjustmentAmount { get; set; }

        /// <summary>Gets or sets the OriginalItemID field.[OriginalItemID renamed to PoAccountingItemId] </summary>
        public virtual long? PoAccountingItemId { get; set; }

        /// <summary>Gets or sets the AccountingItemIDLink field. </summary>
        public virtual long? AccountingItemIdLink { get; set; }

        /// <summary>Gets or sets the IsChanged field. </summary>
        public virtual bool IsChanged { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the ReconciliationID field. </summary>
        public virtual int? ReconciliationId { get; set; }

        [ForeignKey("ReconciliationId")]
        public virtual BankRecControlUnit BankRecControl { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the AccountingItemOrigAmount field. </summary>
        public virtual decimal? AccountingItemOrigAmount { get; set; }

        /// <summary>Gets or sets the AccountingItemTypeOfModificationID field. </summary>
        public virtual int? AccountingItemTypeOfModificationId { get; set; }
        
        /// <summary>Gets or sets the SplitAccountingItemId field.[AccountingItemOrigID renamed to SplitAccountingItemId] </summary>
        public virtual long? SplitAccountingItemId { get; set; }

        [ForeignKey("SplitAccountingItemId")]
        public virtual AccountingItemUnit AccountingItem { get; set; }

        /// <summary>Gets or sets the ICTJobID field. </summary>
        public virtual int? IctJobId { get; set; }

        /// <summary>Gets or sets the ICTAccountingItemID field. </summary>
        public virtual long? IctAccountingItemId { get; set; }


        /// <summary>Gets or sets the TaxRebateID field. </summary>
        public virtual int? TaxRebateId { get; set; }

        /// <summary>Gets or sets the CurrencyOverrideRate field. </summary>
        public virtual double? CurrencyOverrideRate { get; set; }

        /// <summary>Gets or sets the FunctionalCurrencyAmount field. </summary>
        public virtual decimal? FunctionalCurrencyAmount { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyRateID field. </summary>
        public virtual short? TypeOfCurrencyRateId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the HomeCurAmount field. </summary>
        public virtual decimal? HomeCurAmount { get; set; }

        /// <summary>Gets or sets the CustomForexRate field. </summary>
        public virtual decimal? CustomForexRate { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsAccountingItemSplit field. </summary>
        public virtual bool IsAccountingItemSplit { get; set; }

        /// <summary>
        /// Gets or sets the CheckType field
        /// </summary>
        public virtual CheckType? CheckTypeId { get; set; }

        /// <summary>Gets or sets the RowNumber field. </summary>
        public virtual long? RowNumber { get; set; }

        #endregion


        public AccountingItemUnit()
        {
            IsAsset = false;
            IsChanged = false;
            IsActive = true;
            IsEnterable = false;
        }
    }
}
