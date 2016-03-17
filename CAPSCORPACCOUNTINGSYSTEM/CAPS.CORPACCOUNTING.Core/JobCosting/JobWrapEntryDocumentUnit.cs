using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.PettyCash;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobWrapEntryDocument is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapEntryDocument")]
    public class JobWrapEntryDocumentUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobWrapDocumentLogId</summary>
        [Column("JobWrapDocumentLogId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the JobBudgetID field. </summary>
        public virtual int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the PaymentTermID field. </summary>
        public virtual int? PaymentTermId { get; set; } 

        /// <summary>Gets or sets the BankAccountID field. </summary>
        public virtual long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the BatchID field. </summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        /// <summary>Gets or sets the TypeOfInvoiceID field. </summary>
        public virtual TypeOfInvoice? TypeOfInvoiceId { get; set; }

        /// <summary>Gets or sets the PaymentDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PaymentDate { get; set; } 

        /// <summary>Gets or sets the PaymentNumber field. </summary>
        public virtual string PaymentNumber { get; set; }

        /// <summary>Gets or sets the PurchaseOrderReference field. </summary>
        public virtual string PurchaseOrderReference { get; set; } 

        /// <summary>Gets or sets the PettyCashAccountID field. </summary>
        public virtual long? PettyCashAccountId { get; set; }

        [ForeignKey("PettyCashAccountId")]
        public virtual PettyCashAccountUnit PettyCashAccount { get; set; }

        /// <summary>Gets or sets the IsCreditCard field. </summary>
        public virtual bool IsCreditCard { get; set; } 

        /// <summary>Gets or sets the IsPrintRequired field. </summary>
        public virtual bool IsPrintRequired { get; set; } 

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the IsHistory field. </summary>
        public virtual bool IsHistory { get; set; } 

        /// <summary>Gets or sets the IsRetired field. </summary>
        public virtual bool IsRetired { get; set; } 

        /// <summary>Gets or sets the SourcePOAccountingDocumentID field. </summary>
        public virtual long? SourcePoAccountingDocumentId { get; set; }  

        /// <summary>Gets or sets the InvoiceAccountingDocumentID field. </summary>
        public virtual long? InvoiceAccountingDocumentId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion

        public JobWrapEntryDocumentUnit()
        {
            IsCreditCard = false;
            IsPrintRequired = false;
            IsEnterable = false;
            IsHistory = false;
            IsRetired = false;
        }
    }
}
