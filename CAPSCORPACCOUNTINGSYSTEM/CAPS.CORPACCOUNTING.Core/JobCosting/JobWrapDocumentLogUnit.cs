using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobWrapDocumentLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapDocumentLog")]
    public class JobWrapDocumentLogUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 200;
        public const int MaxPayeeLength = 300;
        public const int MaxLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobWrapCheckLogId</summary>
        [Column("JobWrapDocumentLogId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobBudgetID field. </summary>
        public virtual int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the TypeOfInvoiceID field. </summary>
        public virtual TypeOfInvoice? TypeOfInvoiceId { get; set; }

        /// <summary>Gets or sets the InvoiceInfo field. </summary>
        [StringLength(MaxLength)]
        public virtual string InvoiceInfo { get; set; }

        /// <summary>Gets or sets the InvoiceDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? InvoiceDate { get; set; } 

        /// <summary>Gets or sets the TypeOfCurrencyID field. </summary>
        public virtual short? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the Payee field. </summary>
        [StringLength(MaxPayeeLength)]
        public virtual string Payee { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the CheckInfo field. </summary>
        [StringLength(MaxLength)]
        public virtual string CheckInfo { get; set; }

        /// <summary>Gets or sets the CheckDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>Gets or sets the PONumber field. </summary>
        [StringLength(MaxLength)]
        public virtual string PoNumber { get; set; } 

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; }

        /// <summary>Gets or sets the IsUploaded field. </summary>
        public virtual bool IsUploaded { get; set; }

        /// <summary>Gets or sets the DateImported field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateImported { get; set; } 

        /// <summary>Gets or sets the ImportedByUserID field. </summary>
        public virtual int? ImportedByUserId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion

        public JobWrapDocumentLogUnit()
        {
            IsUploaded = false;
        }
    }
}
