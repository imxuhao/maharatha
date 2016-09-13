using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;
using System;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// ARYTDInvoice is the table name in lajit
    /// </summary>
    [Table("CAPS_ARYTDInvoice")]
    public class ArytdInvoiceUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Max Length of InvoiceNumber
        /// </summary>
        public const int MaxLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with ARYTDInvoiceId</summary>
        [Column("ARYTDInvoiceId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the CustomerID field. </summary>
        public virtual int CustomerId { get; set; } 

        [ForeignKey("CustomerId")]
        public virtual CustomerUnit Customer { get; set; }

        /// <summary>Gets or sets the ARInvoiceRequestID field. </summary>
        public virtual int? ArInvoiceRequestId { get; set; }

        /// <summary>Gets or sets the InvoiceDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? InvoiceDate { get; set; }

        /// <summary>Gets or sets the InvoiceNumber field. </summary>
        [StringLength(MaxLength)]
        public virtual string InvoiceNumber { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the IsInvoicePrintRequired field. </summary>
        public virtual bool? IsInvoicePrintRequired { get; set; }

        /// <summary>Gets or sets the IsInvoiceHistory field. </summary>
        public virtual bool IsInvoiceHistory { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the ManualAcctDocID field. </summary>
        public virtual long? ManualAcctDocId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public ArytdInvoiceUnit()
        {
            IsInvoicePrintRequired = true;
            IsInvoiceHistory = false;
            IsEnterable = true;
            IsActive = true;
        }

    }
}
