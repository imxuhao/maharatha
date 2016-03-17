using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    ///  ReceiptHistory is the table name in Lajit
    /// </summary>
    [Table("CAPS_ReceiptHistory")]
    public class ReceiptHistoryUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescriptionLength = 400;
        public const int MaxDepositReferenceLength = 100;

        /// <summary> Overriding the ID column with ReceiptHistoryId field. </summary>
        [Column("ReceiptHistoryId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the ReceiptAccountingItemId field. </summary>
        public virtual long? ReceiptAccountingItemId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the DepositReference field. </summary>
        [StringLength(MaxDepositReferenceLength)]
        public virtual string DepositReference { get; set; }

        /// <summary>Gets or sets the DepositDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DepositDate { get; set; }

        /// <summary>Gets or sets the CustomerId field. </summary>
        public virtual int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual CustomerUnit CustomerUnit { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public virtual long? BankAccountId { get; set; }
        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccountUnit { get; set; }

        /// <summary>Gets or sets the ArytdInvoiceId field. </summary>
        public virtual int? ArytdInvoiceId { get; set; }

        /// <summary>Gets or sets the ArBillingTypeId field. </summary>
        public virtual int? ArBillingTypeId { get; set; }
        [ForeignKey("ArBillingTypeId")]
        public ARBillingTypeUnit ARBillingType { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit AccountUnit { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }
        [ForeignKey("JobId")]
        public JobUnit JobUnit { get; set; }

        /// <summary>Gets or sets the ReceiptAmount field. </summary>
        public virtual decimal? ReceiptAmount { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
