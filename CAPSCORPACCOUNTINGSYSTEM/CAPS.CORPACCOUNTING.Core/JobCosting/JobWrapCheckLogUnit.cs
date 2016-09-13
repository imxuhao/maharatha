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
    /// JobWrapCheckLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapCheckLog")]
    public class JobWrapCheckLogUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 200;
        public const int MaxPayeeLength = 300;
        public const int MaxNumLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobWrapCheckLogId</summary>
        [Column("JobWrapCheckLogId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

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
        [StringLength(MaxNumLength)]
        public virtual string LineInfo { get; set; }

        /// <summary>Gets or sets the Payee field. </summary>
        [StringLength(MaxPayeeLength)]
        public virtual string Payee { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the CheckInfo field. </summary>
        [StringLength(MaxNumLength)]
        public virtual string CheckInfo { get; set; }

        /// <summary>Gets or sets the CheckDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>Gets or sets the PONumber field. </summary>
        [StringLength(MaxNumLength)]
        public virtual string PoNumber { get; set; } 

        /// <summary>Gets or sets the Amount field. </summary>
        public virtual decimal? Amount { get; set; } 
        
        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion
    }
}
