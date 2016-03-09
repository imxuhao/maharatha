using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    /// <summary>
    ///  BusinessRuleGroup is the table name in Lajit
    /// </summary>
    [Table("CAPS_BusinessRuleGroup")]
   public class BusinessRuleGroupUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxCaptionLength = 20;
        private const int MaxDescriptionLength = 100;

        /// <summary> Overriding the ID column with BusinessRuleGroupId field. </summary>
        [Column("BusinessRuleGroupId")]
        public override int Id { get; set; }
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public string Notes { get; set; }

        /// <summary>Gets or sets the BusinessRuleCategoryId field. </summary>
        public short BusinessRuleCategoryId { get; set; }
        [ForeignKey("BusinessRuleCategoryId")]
        public virtual BusinessRuleCategoryUnit BusinessRuleCategoryUnit { get; set; }

        /// <summary>Gets or sets the TestedByUser field. </summary>
        public string TestedByUser { get; set; }

        /// <summary>Gets or sets the DateTested field. </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateTested { get; set; }

        /// <summary>Gets or sets the ApprovedByUser field. </summary>
        public string ApprovedByUser { get; set; }

        /// <summary>Gets or sets the DateApproved field. </summary>
        [Column(TypeName = "smalldatetime")]
        public DateTime? DateApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
