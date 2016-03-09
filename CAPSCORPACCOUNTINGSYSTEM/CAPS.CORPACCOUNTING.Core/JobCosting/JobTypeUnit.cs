using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.JobCosting
{

    /// <summary>
    /// JobType is the table name in lajit
    /// </summary>
    [Table("CAPS_JobType")]
    public class JobTypeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescLength = 100;

        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobTypeId</summary>
        [Column("JobTypeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the DefaultChartOfAccountID field. </summary>
        public virtual int? DefaultChartOfAccountId { get; set; }

        /// <summary>Gets or sets the TypeOfJobID field. </summary>
        public virtual short? TypeOfJobId { get; set; } 

        /// <summary>Gets or sets the TypeOfFormatMaskID field. </summary>
        public virtual int? TypeOfFormatMaskId { get; set; }  

        /// <summary>Gets or sets the TypeOfHeadingID field. </summary>
        public virtual int? TypeOfHeadingId { get; set; } 

        /// <summary>Gets or sets the BudgetTypeOfAccountingLayoutID field. </summary>
        public virtual int? BudgetTypeOfAccountingLayoutId { get; set; }

        [ForeignKey("BudgetTypeOfAccountingLayoutId")]
        public virtual AccountingLayoutUnit AccountingLayout { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int EntityId { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } // 
       
        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion


    }
}
