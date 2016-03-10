using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// SalesRegion is the table name in Lajit
    /// </summary>

    [Table("CAPS_SalesRegion")]
    public class SalesRegionUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescriptionLength = 200;
        public const int MaxAbbreviationLength = 10;

        /// <summary> Overriding the ID column with SalesRegionId field. </summary>
        [Column("SalesRegionId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }


        /// <summary>Gets or sets the SalesRegionAbbreviation field. </summary>
        [StringLength(MaxAbbreviationLength)]
        public virtual string SalesRegionAbbreviation { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }
        [ForeignKey("EntityId")]
        public  virtual EntityUnit EntityUnit { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
