using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobWrapSalesLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapSalesLog")]
    public class JobWrapSalesLogUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescLength = 300;      
        public const int MaxLineInfoLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobWrapSalesLogId</summary>
        [Column("JobWrapSalesLogId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the JobWrapDocumentLogID field. </summary>
        public virtual int? JobWrapDocumentLogId { get; set; } 

        /// <summary>Gets or sets the JobBudgetID field. </summary>
        public virtual int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the LineInfo field. </summary>
        [StringLength(MaxLineInfoLength)]
        public virtual string LineInfo { get; set; }

        /// <summary>Gets or sets the Payor field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Payor { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the ReceiptInfo field. </summary>
        [StringLength(MaxLineInfoLength)]
        public virtual string ReceiptInfo { get; set; } 

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion

    }
}
