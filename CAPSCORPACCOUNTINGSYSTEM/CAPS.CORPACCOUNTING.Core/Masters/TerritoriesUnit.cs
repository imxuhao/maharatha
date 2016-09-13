using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_Territories")]
    public class TerritoriesUnit:FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
       
        public const int MaxDescriptionLength = 100;
        /// <summary>Gets or sets the AccountId field. </summary>
        [Column("TerritorieId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
