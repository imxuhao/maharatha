using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// Enum for TypeofTransaction
    /// </summary>
    public enum TypeofTransaction
    {
        [Display(Name = "AP Invoice")]
        APInvoice = 1,
        [Display(Name = "Manual Check")]
        ManualCheck = 2,
        [Display(Name = "Purchase Order")]
        PurchaseOrder = 3,
        [Display(Name = "Existed PO")]
        ExistedPO = 4,
        [Display(Name = "Existed AP")]
        ExistedAP = 5,
        [Display(Name = "CreditCard PO")]
        CreditCardPO = 6,
    }

    /// <summary>
    /// JobWrapPurchaseOrderLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapPurchaseOrderLog")]
    public class JobPurchaseOrderLogUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 500;
        public const int MaxLineLength = 50;
        public const int MaxPayeeLength = 200;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobPurchaseOrderLogId</summary>
        [Column("JobPurchaseOrderLogId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobBudgetId field. </summary>
        [Range(0,Int32.MaxValue)]
        public virtual int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account{ get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the LineInfo field. </summary>
        [StringLength(MaxLineLength)]
        public virtual string LineInfo { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Payee field. </summary>
        [StringLength(MaxPayeeLength)]
        public virtual string Payee { get; set; }

        /// <summary>Gets or sets the PONumber field. </summary>
        [StringLength(MaxLineLength)]
        public virtual string PONumber { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        [Column(TypeName = "Money")]
        public virtual decimal Amount { get; set; }

        /// <summary>Gets or sets the AccountingTransactionId field. </summary>
        [StringLength(MaxLineLength)]
        public virtual string AccountingTransactionId { get; set; }

        /// <summary>Gets or sets the InvoiceNumber field. </summary>
        [StringLength(MaxLineLength)]
        public virtual string InvoiceNumber { get; set; }

        /// <summary>Gets or sets the TransactionAmount field. </summary>
        [Column(TypeName = "Money")]
        public virtual decimal TransactionAmount { get; set; }


        /// <summary>Gets or sets the CheckNumber field. </summary>
        [StringLength(MaxLineLength)]
        public virtual string CheckNumber { get; set; }

        /// <summary>Gets or sets the InvoiceDate field. </summary>
        public virtual DateTime? InvoiceDate { get; set; }

        /// <summary>Gets or sets the CheckDate field. </summary>
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>Gets or sets the PostingDate field. </summary>
        public virtual DateTime? PostingDate { get; set; }

        /// <summary>Gets or sets the IsReconciled field. </summary>
        public virtual bool IsReconciled { get; set; }

        /// <summary>Gets or sets the TransactionType field. </summary>
        public virtual TypeofTransaction? TransactionTypeId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        
        #endregion

        public JobPurchaseOrderLogUnit()
        {
        }
        public JobPurchaseOrderLogUnit(int jobbudgetid, long? accountid, int? vendorid, string lineinfo, string description, string payee, string ponumber,
           decimal amount, string accountingtransactionid, string invoicenumber, decimal transactionamount, string checknumber, DateTime? invoicedate,
           DateTime? checkdate, DateTime? postingdate, bool isreconciled, TypeofTransaction? transactiontypeid, long? organizationunitid)
        {
            JobBudgetId = jobbudgetid;
            AccountId = accountid;
            VendorId = vendorid;
            LineInfo = lineinfo;
            Description = description;
            Payee = payee;
            PONumber = ponumber;
            Amount = amount;
            AccountingTransactionId = accountingtransactionid;
            InvoiceNumber = invoicenumber;
            TransactionAmount = transactionamount;
            CheckNumber = checknumber;
            InvoiceDate = invoicedate;
            CheckDate = checkdate;
            PostingDate = postingdate;
            IsReconciled = isreconciled;
            TransactionTypeId = transactiontypeid;
            OrganizationUnitId = organizationunitid;
        }
    }
}