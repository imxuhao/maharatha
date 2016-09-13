using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System;

namespace CAPS.CORPACCOUNTING.Payables
{
    /// <summary>
    /// InvoicePayment is the table name in lajit
    /// </summary>
    [Table("CAPS_InvoicePayment")]
    public class InvoicePaymentUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Max Length of PaymentNumber
        /// </summary>
        public const int MaxLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with InvoicePaymentId</summary>
        [Column("InvoicePaymentId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public int? LajitId { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>
        public virtual int? VendorId { get; set; } 

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the InvoiceAccountingDocumentID field. </summary>
        public virtual long InvoiceAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the PaymentAccountingDocumentID field. </summary>
        public virtual long? PaymentAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the PaymentRequestID field. </summary>
        public virtual int? PaymentRequestId { get; set; } 

        /// <summary>Gets or sets the TypeOfCheckGroupID field. </summary>
        public virtual TypeOfCheckGroup? TypeOfCheckGroupId { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual short? TypeOfCurrencyId { get; set; } 

        /// <summary>Gets or sets the TypeOfPaymentMethodID field. </summary>
        public virtual TypeofPaymentMethod? TypeOfPaymentMethodId { get; set; } 

        /// <summary>Gets or sets the IsPaymentSelected field. </summary>
        public virtual bool IsPaymentSelected { get; set; } 

        /// <summary>Gets or sets the InvoicePaymentAmount field. </summary>
        public virtual decimal? InvoicePaymentAmount { get; set; }

        /// <summary>Gets or sets the SelectedPaymentAmount field. </summary>
        public virtual decimal? SelectedPaymentAmount { get; set; }

        /// <summary>Gets or sets the PaymentNumber field. </summary>
        [StringLength(MaxLength)]
        public virtual string PaymentNumber { get; set; }

        /// <summary>Gets or sets the DiscountDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DiscountDate { get; set; } 

        /// <summary>Gets or sets the DiscountAmount field. </summary>
        public virtual decimal? DiscountAmount { get; set; }

        /// <summary>Gets or sets the ProcessDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? ProcessDate { get; set; } 

        /// <summary>Gets or sets the IsGeneratedDiscount field. </summary>     
        public virtual bool? IsGeneratedDiscount { get; set; } 

        /// <summary>Gets or sets the DiscountLinkInvoicePaymentID field. </summary>
        public virtual long? DiscountLinkInvoicePaymentId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public InvoicePaymentUnit()
        {
            IsPaymentSelected = false;
        }
    }
}
