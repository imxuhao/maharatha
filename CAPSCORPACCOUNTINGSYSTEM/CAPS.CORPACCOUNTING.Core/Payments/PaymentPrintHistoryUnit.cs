using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAPS.CORPACCOUNTING.Payments
{

    /// <summary>
    ///  PaymentPrintHistory is the table name in Lajit
    /// </summary>
    [Table("CAPS_PaymentPrintHistory")]
   public class PaymentPrintHistoryUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxPayToNameLength = 500;
        public const int MaxPaymentNumberLength = 50;

        /// <summary> Overriding the ID column with PaymentPrintHistoryId field. </summary>
        [Column("PaymentPrintHistoryId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public long? LajitId { get; set; }

        /// <summary>Gets or sets the PaymentAccountingDocumentId field. </summary>
        public virtual long? PaymentAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the PaymentRequestId field. </summary>
        public virtual int? PaymentRequestId { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public virtual long? BankAccountId { get; set; }
        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public VendorUnit VendorUnit { get; set; }

        /// <summary>Gets or sets the PayToName field. </summary>
        [StringLength(MaxPayToNameLength)]
        public virtual string PayToName { get; set; }

        /// <summary>Gets or sets the PaymentNumber field. </summary>
        [StringLength(MaxPaymentNumberLength)]
        public virtual string PaymentNumber { get; set; }

        /// <summary>Gets or sets the PaymentDate field. </summary>
        [Column(TypeName ="smalldatetime")]
        public virtual DateTime? PaymentDate { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>Gets or sets the PrintDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PrintDate { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual int? UserId { get; set; }
      
        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
