using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    /// <summary>
    ///  BusinessRule is the table name in Lajit
    /// </summary>
    [Table("CAPS_BusinessRule")]
    public class BusinessRuleUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxCaptionLength = 20;
        private const int MaxDescriptionLength = 100;

        /// <summary> Overriding the ID column with BusinessRuleId field. </summary>
        [Column("BusinessRuleId")]
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

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual short BusinessRuleCategoryId { get; set; }
        [ForeignKey("BusinessRuleCategoryId")]
        public virtual BusinessRuleCategoryUnit BusinessRuleCategoryUnit { get; set; }

        /// <summary>Gets or sets the IsSchema field. </summary>
        public virtual bool IsSchema { get; set; }

        /// <summary>Gets or sets the SchemaId field. </summary>
        public virtual int? SchemaId { get; set; }

        /// <summary>Gets or sets the IsPreference field. </summary>
        public virtual bool IsPreference { get; set; }

        /// <summary>Gets or sets the DefaultPreferenceId field. </summary>
        public virtual int? DefaultPreferenceId { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public virtual bool IsPrivate { get; set; }

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
