using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Banking
{
    /// <summary>
    /// Enum for TypeOfInactiveStatus
    /// </summary>
    public enum TypeOfInactiveStatus
    {
        [Display(Name = "Uncommitted")]
        Uncommitted = 1,
        [Display(Name = "Deleted")]
        Deleted = 2,
        [Display(Name = "Waiting Approval")]
        WaitingApproval = 3,
        [Display(Name = "Closed")]
        Closed = 4,
        [Display(Name = "Auto Save")]
        AutoSave = 5,
        [Display(Name = "Not Used")]
        NotUsed = 6,
        [Display(Name = "Waiting Process Approval")]
        WaitingProcessApproval = 7,

    }
    /// <summary>
    /// Enum for TypeOfBankAccount
    /// </summary>
    public enum TypeOfBankAccount
    {
        [Display(Name = "Checking")]
        Checking = 1,
        [Display(Name = "Savings")]
        Savings = 2,
        [Display(Name = "Money Market")]
        MoneyMarket = 3,
        [Display(Name = "Line of Credit")]
        LineofCredit = 4,
        [Display(Name = "AMEX")]
        AMEX = 5,
        [Display(Name = "MasterCard")]
        MasterCard = 6,
        [Display(Name = "Visa")]
        Visa = 7,
        [Display(Name = "Other Credit Card")]
        OtherCreditCard = 8,
        [Display(Name = "Debit Card")]
        DebitCard = 9,
        [Display(Name = "AMEX Basic Control Account")]
        AMEXBasicControlAccount = 10,
        [Display(Name = "Other Control")]
        OtherControl = 11,
        [Display(Name = "VISAControl")]
        VISAControl = 12,
        [Display(Name = "Debit Control")]
        DebitControl = 13,
        [Display(Name = "AMEX Card Holder")]
        AMEXCardHolder = 14,
        [Display(Name = "Master Card Holder")]
        MasterCardHolder = 15,
        [Display(Name = "VISA Card Holder")]
        VISACardHolder = 16,
        [Display(Name = "Other Credit Card Holder")]
        OtherCreditCardHolder = 17,
        [Display(Name = "Debit Card Holder")]
        DebitCardHolder = 18,
        [Display(Name = "Pay Pal")]
        PayPal = 19,
        [Display(Name = "Petty Cash Custodian")]
        PettyCashCustodian = 20,
        [Display(Name = "FEDEX")]
        FEDEX = 21,
        [Display(Name = "UPS")]
        UPS = 22,
        [Display(Name = "DHL")]
        DHL = 23,
        [Display(Name = "Payroll Company")]
        PayrollCompany = 24

    }

    [Table("CAPS_BankAccount")]
    public class BankAccountUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Maximum length for  Description,BankAccountName,ControlAccount properties.</summary>
        public const int MaxLength = 200;

        /// <summary> Maximum length of the ExpirationMMYYYY  properties.</summary>
        public const int MaxExpirationLength = 50;

        /// <summary> Maximum length for BankAccountNumber,RoutingNumber,CCFullAccountNO  properties.</summary>
        public const int MaxAccountLength = 100;

        /// <summary> Maximum length of the Notes  property.</summary>
        public const int MaxNotesLength = 500;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with BankAccountId</summary>
        [Column("BankAccountId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the TypeOfBankAccountId field. </summary>
        [EnumDataType(typeof(TypeOfBankAccount))]
        public virtual TypeOfBankAccount TypeOfBankAccountId { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the BankAccountName field. </summary>
        [StringLength(MaxLength)]
        public virtual string BankAccountName { get; set; }

        /// <summary>Gets or sets the BankAccountNumber field. </summary>
        [StringLength(MaxAccountLength)]
        public virtual string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the RoutingNumber field. </summary>
        [StringLength(MaxAccountLength)]
        public virtual string RoutingNumber { get; set; }

        /// <summary>Gets or sets the TypeOfCheckStockId field. </summary>
        public virtual int? TypeOfCheckStockId { get; set; }

        [ForeignKey("TypeOfCheckStockId")]
        public TypeOfCheckStockUnit TypeofCheckStock { get; set; }

        /// <summary>Gets or sets the LastCheckNumberGenerated field. </summary>
        public virtual long? LastCheckNumberGenerated { get; set; }

        /// <summary>Gets or sets the ControlAccount field. </summary>
        [StringLength(MaxLength)]
        public virtual string ControlAccount { get; set; }

        /// <summary>Gets or sets the ClearingAccountId field. </summary>
        public virtual long? ClearingAccountId { get; set; }

        [ForeignKey("ClearingAccountId")]
        public AccountUnit ClearingAccount { get; set; }

        /// <summary>Gets or sets the ClearingJobId field. </summary>
        public virtual int? ClearingJobId { get; set; }

        [ForeignKey("ClearingJobId")]
        public JobUnit ClearingJob { get; set; }

        /// <summary>Gets or sets the MaxExpirationLength field. </summary>
        [StringLength(MaxExpirationLength)]
        public virtual string ExpirationMMYYYY { get; set; }

        /// <summary>Gets or sets the TypeOfUploadFileId field. </summary>
        public virtual int? TypeOfUploadFileId { get; set; }

        [ForeignKey("TypeOfUploadFileId")]
        public TypeOfUploadFileUnit TypeOfUploadFile { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the ControllingBankAccountId field. </summary>
        public virtual long? ControllingBankAccountId { get; set; }

        [ForeignKey("ControllingBankAccountId")]
        public AccountUnit ControllingBankAccount { get; set; }

        /// <summary>Gets or sets the IsClosed field. </summary>
        public virtual bool IsClosed { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the PositivePayTypeOfUploadFileId field. </summary>
        public virtual int? PositivePayTypeOfUploadFileId { get; set; }

        [ForeignKey("PositivePayTypeOfUploadFileId")]
        public TypeOfUploadFileUnit PositivePayTypeOfUploadFile { get; set; }

        /// <summary>Gets or sets the PositivePayTransmitterInfo field. </summary>
        [StringLength(MaxLength)]
        public virtual string PositivePayTransmitterInfo { get; set; }

        /// <summary>Gets or sets the PettyCashAccountId field. </summary>
        public virtual long? PettyCashAccountId { get; set; }

        [ForeignKey("PettyCashAccountId")]
        public AccountUnit PettyCashAccount { get; set; }

        /// <summary>Gets or sets the IsACHEnabled field. </summary>
        public virtual bool? IsACHEnabled { get; set; }

        /// <summary>Gets or sets the ACHDestinationCode field. </summary>
        [StringLength(MaxAccountLength)]
        public virtual string ACHDestinationCode { get; set; }

        /// <summary>Gets or sets the ACHDestinationName field. </summary>
        [StringLength(MaxLength)]
        public virtual string ACHDestinationName { get; set; }

        /// <summary>Gets or sets the ACHOriginCode field. </summary>
        [StringLength(MaxAccountLength)]
        public virtual string ACHOriginCode { get; set; }

        /// <summary>Gets or sets the ACHOriginName field. </summary>
        [StringLength(MaxLength)]
        public virtual string ACHOriginName { get; set; }

        /// <summary>Gets or sets the BatchId field. </summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public BatchUnit Batch { get; set; }

        /// <summary>Gets or sets the CCFullAccountNO field. </summary>
        [StringLength(MaxAccountLength)]
        public virtual string CCFullAccountNO { get; set; }

        /// <summary>Gets or sets the CCFootNote field. </summary>
        [StringLength(MaxLength)]
        public virtual string CCFootNote { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion
        public BankAccountUnit() { }
        public BankAccountUnit(string description, short? displaysequence, TypeOfBankAccount typeofbankaccountid, long? accountid, int? jobid,
                     string bankaccountname, string bankaccountnumber, string routingnumber, int? typeofcheckstockid, long? lastchecknumbergenerated,
                     string controlaccount, long? clearingaccountid, int? clearingjobid, string expirationmmyyyy, int? typeofuploadfileid, int? vendorid,
                     long? controllingbankaccountid, bool isclosed, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid,
                     int? positivepaytypeofuploadfileid, string positivepaytransmitterinfo, long? pettycashaccountid, bool? isachenabled,
                     string achdestinationcode, string achdestinationname, string achorigincode, string achoriginname, int? batchid,
                     string ccfullaccountno, string ccfootnote, long? organizationunitid)
        {
            Description = description;
            DisplaySequence = displaysequence;
            TypeOfBankAccountId = typeofbankaccountid;
            AccountId = accountid;
            JobId = jobid;
            BankAccountName = bankaccountname;
            BankAccountNumber = bankaccountnumber;
            RoutingNumber = routingnumber;
            TypeOfCheckStockId = typeofcheckstockid;
            LastCheckNumberGenerated = lastchecknumbergenerated;
            ControlAccount = controlaccount;
            ClearingAccountId = ClearingAccountId;
            ClearingJobId = clearingjobid;
            ExpirationMMYYYY = expirationmmyyyy;
            TypeOfUploadFileId = typeofuploadfileid;
            VendorId = vendorid;
            ControllingBankAccountId = controllingbankaccountid;
            IsClosed = isclosed;
            IsActive = isactive;
            IsApproved = isapproved;
            TypeOfInactiveStatusId = typeofinactivestatusid;
            PositivePayTransmitterInfo = positivepaytransmitterinfo;
            PositivePayTypeOfUploadFileId = positivepaytypeofuploadfileid;
            PettyCashAccountId = pettycashaccountid;
            IsACHEnabled = isachenabled;
            ACHDestinationCode = achdestinationcode;
            ACHDestinationName = achdestinationname;
            ACHOriginCode = achorigincode;
            ACHOriginName = achoriginname;
            BatchId = batchid;
            CCFullAccountNO = ccfullaccountno;
            CCFootNote = ccfootnote;
            OrganizationUnitId = organizationunitid;
        }

    }
}




