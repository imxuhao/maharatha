using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    /// Entity is the table name in Lajit
    /// </summary>

    [Table("CAPS_Entity")]
    public class EntityUnit  : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescriptionLength = 100;
        public const int MaxCaptionLength = 20;

        /// <summary> Overriding the ID column with EntityId field. </summary>
        [Column("EntityId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the StandardIconId field. </summary>
        public virtual short? StandardIconId { get; set; }

        /// <summary>Gets or sets the IsBusinessRuleExist field. </summary>
        public virtual bool IsBusinessRuleExist { get; set; }

        /// <summary>Gets or sets the IsSecureGroupx field. </summary>
        public virtual bool? IsSecureGroupx { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyId field. </summary>
        public virtual int? TypeOfCurrencyId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
