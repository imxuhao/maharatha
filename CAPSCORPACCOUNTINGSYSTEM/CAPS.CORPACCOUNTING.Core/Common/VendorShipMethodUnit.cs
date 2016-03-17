using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  VendorShipMethod is the table name in Lajit
    /// </summary>
    [Table("CAPS_VendorShipMethod")]
    public class VendorShipMethodUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescriptionLength= 50;

        /// <summary> Overriding the ID column with ShipMethodId field. </summary>
        [Column("ShipMethodId")]
        public override int Id { get; set; }
        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; } 

        /// <summary>Gets or sets the EntityID field. </summary>
        public int EntityId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
