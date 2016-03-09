using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    public enum TypeOfBusinessProcess
    {
        [Display(Name = "Form List Controls", Description = "Form List Controls")]
        FormListControls = 1
    }


    /// <summary>
    ///  BusinessProcess is the table name in Lajit
    /// </summary>
    [Table("CAPS_BusinessProcess")]
    public class BusinessProcessUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxCaptionLength = 20;
        private const int MaxDescriptionLength = 100;

        /// <summary> Overriding the ID column with BusinessProcessId field. </summary>
        [Column("BusinessProcessId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfBusinessProcessId field. </summary>
        public virtual TypeOfBusinessProcess TypeOfBusinessProcessId { get; set; }

        /// <summary>Gets or sets the BusinessProcessCategoryId field. </summary>
        public virtual BusinessProcessCategory BusinessProcessCategoryId { get; set; }

        /// <summary>Gets or sets the BusinessRuleGroupId field. </summary>
        public virtual int? BusinessRuleGroupId { get; set; }
        [ForeignKey("BusinessRuleGroupId")]
        public virtual BusinessRuleGroupUnit BusinessRuleGroupUnit { get; set; }

        /// <summary>Gets or sets the RunBusinessProcessGroupId field. </summary>
        public virtual int? RunBusinessProcessGroupId { get; set; }
        [ForeignKey("RunBusinessProcessGroupId")]
        public virtual BusinessProcessGroupUnit BusinessProcessGroupUnit { get; set; }

        /// <summary>Gets or sets the ServiceLevelAgreementId field. </summary>
        public virtual int? ServiceLevelAgreementId { get; set; }
        [ForeignKey("ServiceLevelAgreementId")]
        public virtual ServiceLevelAgreementUnit ServiceLevelAgreementUnit { get; set; }

        /// <summary>Gets or sets the IsLogRequired field. </summary>
        public virtual bool IsLogRequired { get; set; }

        /// <summary>Gets or sets the TestedByUser field. </summary>
        public virtual string TestedByUser { get; set; }

        /// <summary>Gets or sets the DateTested field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateTested { get; set; }

        /// <summary>Gets or sets the ApprovedByUser field. </summary>
        public virtual string ApprovedByUser { get; set; }

        /// <summary>Gets or sets the DateApproved field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
