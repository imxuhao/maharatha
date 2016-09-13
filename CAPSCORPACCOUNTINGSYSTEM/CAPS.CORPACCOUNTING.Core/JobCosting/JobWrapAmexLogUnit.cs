using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobWrapAmexLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapAmexLog")]
    public class JobWrapAmexLogUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescLength = 200;
        public const int MaxPayeeLength = 300;
        public const int MaxLineInfoLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobWrapAmexLogId</summary>
        [Column("JobWrapAmexLogId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobWrapDocumentLogID field. </summary>
        public int? JobWrapDocumentLogId { get; set; } 

        /// <summary>Gets or sets the JobBudgetID field. </summary>
        public int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long? AccountId { get; set; }
        
        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the LineInfo field. </summary>
        [StringLength(MaxLineInfoLength)]
        public string LineInfo { get; set; }

        /// <summary>Gets or sets the Payee field. </summary>
        [StringLength(MaxPayeeLength)]
        public string Payee { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the PaymentInfo field. </summary>
        [StringLength(MaxLineInfoLength)]
        public string PaymentInfo { get; set; }

        /// <summary>Gets or sets the PaymentDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime? PaymentDate { get; set; }

        /// <summary>Gets or sets the PONumber field. </summary>
        [StringLength(MaxLineInfoLength)]
        public string PoNumber { get; set; } 

        /// <summary>Gets or sets the Amount field. </summary>
        public decimal? Amount { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion

    }
}
