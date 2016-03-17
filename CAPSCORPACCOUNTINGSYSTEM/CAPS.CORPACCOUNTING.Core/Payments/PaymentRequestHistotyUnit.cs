using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using CAPS.CORPACCOUNTING.Accounting;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Payments
{
    /// <summary>
    /// PaymentRequestHistory is the table name in lajit
    /// </summary>
    [Table("CAPS_PaymentRequestHistory")]
    public class PaymentRequestHistotyUnit : FullAuditedEntity,IMayHaveOrganizationUnit,IMustHaveTenant
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxLength = 50;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with PaymentRequestId</summary>
        [Column("PaymentRequestId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfAccountingDocumentId field.</summary>
        public virtual TypeOfAccountingDocument TypeOfAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the BankAccountId field.</summary>
        public virtual long? BankAccountId { get; set; } 

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit Bank { get; set; }

        /// <summary>Gets or sets the VendorId field.</summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the TypeOfPaymentMethodId field.</summary>
        public virtual TypeofPaymentMethod? TypeOfPaymentMethodId { get; set; }

        /// <summary>Gets or sets the PaymentDate field.</summary>
        public virtual DateTime? PaymentDate { get; set; }

        /// <summary>Gets or sets the StartingPaymentNumber field.</summary>
        [StringLength(MaxLength)]
        public virtual string StartingPaymentNumber { get; set; }

        /// <summary>Gets or sets the IsCheckPrinted field.</summary>
        public virtual bool IsCheckPrinted { get; set; }

        /// <summary>Gets or sets the IsRegisterPrinted field.</summary>
        public virtual bool IsRegisterPrinted { get; set; }

        /// <summary>Gets or sets the IsPosted field.</summary>// IsRegisterPrinted
        public virtual bool IsPosted { get; set; }

        /// <summary>Gets or sets the TenantId field.</summary>// UserID
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field.</summary>// TenantID
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public PaymentRequestHistotyUnit()
        {
            IsCheckPrinted = false;
            IsRegisterPrinted = false;
            IsPosted = false;
        }
    }
}
