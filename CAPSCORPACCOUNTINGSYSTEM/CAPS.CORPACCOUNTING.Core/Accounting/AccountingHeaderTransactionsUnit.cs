using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Banking;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Accounting
{
    public enum TypeOfAccountingDocument
    {
        [Display(Name = "GeneralLedger")]
        GeneralLedger = 1,
        [Display(Name = " Cash Receipts")]
        CashReceipts = 2,
        [Display(Name = "Accounts Payable")]
        AccountsPayable = 3,
        [Display(Name = "Accounts Receivable")]
        AccountsReceivable = 4,
        [Display(Name = "Petty Cash")]
        PettyCash = 5,
        [Display(Name = "Purchase Orders")]
        PurchaseOrders = 6,
        [Display(Name = "Payroll")]
        Payroll = 7,
        [Display(Name = "A/R Receipts")]
        ARReceipts = 8,
        [Display(Name = "Credit Card")]
        CreditCard = 9,
        [Display(Name = "Debit Card")]
        DebitCard = 10,
        [Display(Name = "Shipping")]
        Shipping = 11,
        [Display(Name = "Petty Cash Reimbursement")]
        PettyCashReimbursement = 12,
        [Display(Name = "Petty Cash Deposit")]
        PettyCashDeposit = 13,
        [Display(Name = "Bank Adjustments")]
        BankAdjustments = 14,
        [Display(Name = " Bank Transfer")]
        BankTransfer = 15,
        [Display(Name = "ICT")]
        ICT = 16,
        [Display(Name = "Void Check")]
        VoidCheck = 17,
        [Display(Name = "Credit Card - Personal Charges")]
        CreditCardPersonalCharges = 18,
    }


    [Table("CAPS_AccountingHeaderTransactions")]
    public class AccountingHeaderTransactionsUnit : FullAuditedEntity, IMustHaveTenant, IMustHaveOrganizationUnit
    {

        /// <summary> Maximum length of the  Description property.</summary>
        public const int MaxLength = 100;
        #region Class Property Declarations


        /// <summary>Overriding the Id column with BatchId </summary>
        [Column("AHTID")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxLength)]
        public virtual string Description { get; set; }

        /// <summary>Get Sets the TypeOfAccountingDocumentID Date </summary>
        [EnumDataType(typeof(TypeOfAccountingDocument))]
        public virtual TypeOfAccountingDocument TypeOfAccountingDocumentId { get; set; }

        /// <summary>Get Sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject? TypeOfObjectId { get; set; }

        /////// <summary>Get Sets the DocumentLinkId field. </summary>
        ////public virtual long? DocumentLinkId { get; set; }

        /////// <summary>Get Sets the PostingCycleId field. </summary>
        ////public virtual int? PostingCycleId { get; set; }

        /// <summary>Get Sets the RecurDocId field.</summary>
        public virtual long? RecurDocId { get; set; }

        /// <summary>Get Sets the ReverseDocId field.</summary>
        public virtual long? ReverseDocId { get; set; }

        /// <summary>Get Sets the DocumentDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? DocumentDate { get; set; }

        /// <summary>Get Sets the TransactionDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime TransactionDate { get; set; }

        // [Column(TypeName = "smalldatetime")]
        //  public virtual DateTime? DateCreated { get; set; }

        /// <summary>Get Sets the DatePosted field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? DatePosted { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //public virtual DateTime? DateChanged { get; set; }

        /// <summary>Get Sets the OriginalDocumentId field.</summary>
        public virtual long? OriginalDocumentId { get; set; }

        /// <summary>Get Sets the ControlTotal field.</summary>
        public virtual decimal? ControlTotal { get; set; }

        /// <summary>Get Sets the DocumentReference field.</summary>
        [StringLength(MaxLength)]
        public virtual string DocumentReference { get; set; }

        /// <summary>Get Sets the VoucherReference field.</summary>
        [StringLength(MaxLength)]
        public virtual string VoucherReference { get; set; }

        /// <summary>Get Sets the TypeOfCurrencyId field.</summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Get Sets the CurrencyAdjustmentId field.</summary>
        public virtual int? CurrencyAdjustmentId { get; set; }

        ////public virtual int? CorporateControlPeriodId { get; set; }

        ////public virtual int? SubledgerControlPeriodId { get; set; }

        ////public virtual int? ProjectControlPeriodId { get; set; }

        /// <summary>Get Sets the PostBatchDescription field.</summary>
        [StringLength(MaxLength)]
        public virtual string PostBatchDescription { get; set; }

        ////public virtual bool IsNewActivityPrinted { get; set; }

        // <summary>Get Sets the IsPosted field.</summary>
        public virtual bool IsPosted { get; set; }

        // <summary>Get Sets the IsAutoPosted field.</summary>
        public virtual bool IsAutoPosted { get; set; }

        // <summary>Get Sets the IsChanged field.</summary>
        public virtual bool IsChanged { get; set; }

        ////   public virtual bool IsChangePrinted { get; set; }

        ////  public virtual int? CreatedByUserId { get; set; }

        // <summary>Get Sets the PostedByUserId field.</summary>
        public virtual int? PostedByUserId { get; set; }

        ////   public virtual int? ChangedByUserId { get; set; }

        ////   public virtual int? NewActivityPrintedByUserId { get; set; }

        ////   public virtual int? ChangeActivityPrintedByUserId { get; set; }


        // <summary>Get Sets the BankRecControlId field.</summary>
        public virtual int? BankRecControlId { get; set; }

        // <summary>Get Sets the IsSelected field.</summary>
        public virtual bool IsSelected { get; set; }

        // <summary>Get Sets the IsActive field.</summary>
        public virtual bool IsActive { get; set; }

        // <summary>Get Sets the IsApproved field.</summary>
        public virtual bool IsApproved { get; set; }

        // <summary>Get Sets the TypeOfInactiveStatusId field.</summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        // <summary>Get Sets the IsBankRecOmitted field.</summary>
        public virtual bool? IsBankRecOmitted { get; set; }

        // <summary>Get Sets the IsICTJournal field.</summary>
        public virtual bool? IsICTJournal { get; set; }

        // <summary>Get Sets the ICTCompanyId field.</summary>
        public virtual int? ICTCompanyId { get; set; }

        // <summary>Get Sets the ICTAccountingDocumentId field.</summary>
        public virtual long? ICTAccountingDocumentId { get; set; }

        // <summary>Get Sets the CurrencyOverrideRate field.</summary>
        public virtual double? CurrencyOverrideRate { get; set; }

        // <summary>Get Sets the FunctionalCurrencyControlTotal field.</summary>
        public virtual decimal? FunctionalCurrencyControlTotal { get; set; }

        // <summary>Get Sets the TypeOfCurrencyRateId field.</summary>
        public virtual short? TypeOfCurrencyRateId { get; set; }

        // <summary>Get Sets the MemoLine field.</summary>
        [StringLength(MaxLength)]
        public virtual string MemoLine { get; set; }

        // <summary>Get Sets the Is13Period field.</summary>
        public virtual bool? Is13Period { get; set; }

        // <summary>Get Sets the HomeCurrencyAmount field.</summary>
        public virtual decimal? HomeCurrencyAmount { get; set; }

        // <summary>Get Sets the CustomForexRate field.</summary>       
        public virtual decimal? CustomForexRate { get; set; }

        // <summary>Get Sets the IsPOSubmitForApproval field.</summary>
        public virtual bool IsPOSubmitForApproval { get; set; }

        // <summary>Get Sets the IsCPASTran field.</summary>
        public virtual bool? IsCPASTran { get; set; }

        // <summary>Get Sets the CPASProjCloseId field.</summary>
        public virtual int? CPASProjCloseId { get; set; }

        // <summary>Get Sets the CPASProjId field.</summary>
        public virtual int? CPASProjId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long OrganizationUnitId { get; set; }

        #endregion
        public AccountingHeaderTransactionsUnit() { }

        public AccountingHeaderTransactionsUnit(string description, TypeOfAccountingDocument typeofaccountingdocumentid) {
            Description = description;
            TypeOfAccountingDocumentId = typeofaccountingdocumentid;

        }
        public AccountingHeaderTransactionsUnit(string description, TypeOfAccountingDocument typeofaccountingdocumentid, TypeofObject? typeofobjectid, long? recurdocid, long? reversedocid,
                 DateTime? documentdate, DateTime transactiondate, DateTime? dateposted, long? originaldocumentid, decimal? controltotal, string documentreference,
                 string voucherreference, short? typeofcurrencyid, int? currencyadjustmentid, string postbatchdescription, bool isposted, bool isautoposted, bool ischanged,
                 int? postedbyuserid, int? bankreccontrolid, bool isselected, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid,
                 bool? isbankrecomitted, bool? isictjournal, int? ictcompanyid, long? ictaccountingdocumentid, double? currencyoverriderate,
                 decimal? functionalcurrencycontroltotal, short? typeofcurrencyrateid, string memoline, bool? is13period, decimal? homecurrencyamount, decimal? customforexrate,
                 bool isposubmitforapproval, bool? iscpastran, int? cpasprojcloseid, int? cpasprojid,  long organizationunitid)
        {
            Description = description;
            TypeOfAccountingDocumentId = typeofaccountingdocumentid;
            TypeOfObjectId = typeofobjectid;
            RecurDocId = recurdocid;
            ReverseDocId = reversedocid;
            DocumentDate = documentdate;
            TransactionDate = transactiondate;
            DatePosted = dateposted;
            OriginalDocumentId = originaldocumentid;
            ControlTotal = controltotal;
            DocumentReference = documentreference;
            VoucherReference = voucherreference;
            TypeOfCurrencyId = typeofcurrencyid;
            CurrencyAdjustmentId = currencyadjustmentid;
            PostBatchDescription = postbatchdescription;
            IsPosted = isposted;
            IsAutoPosted = IsAutoPosted;
            IsChanged = ischanged;
            PostedByUserId = postedbyuserid;
            BankRecControlId = bankreccontrolid;
            IsSelected = isselected;
            IsActive = isactive;TypeOfInactiveStatusId = typeofinactivestatusid;
            IsBankRecOmitted = isbankrecomitted;
            IsICTJournal = isictjournal;
            ICTCompanyId = ictcompanyid;
            ICTAccountingDocumentId = ictaccountingdocumentid;
            CurrencyOverrideRate = currencyoverriderate;
            FunctionalCurrencyControlTotal = functionalcurrencycontroltotal;
            TypeOfCurrencyId = typeofcurrencyid;
            MemoLine = memoline;
            Is13Period = is13period;
            HomeCurrencyAmount = homecurrencyamount;
            CustomForexRate = customforexrate;
            IsPOSubmitForApproval = isposubmitforapproval;
            IsCPASTran = iscpastran;
            CPASProjCloseId = cpasprojcloseid;
            CPASProjId = cpasprojcloseid;
            OrganizationUnitId = organizationunitid;
            
        }
    }
}









