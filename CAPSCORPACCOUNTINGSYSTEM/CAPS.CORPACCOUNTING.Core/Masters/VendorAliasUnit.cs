using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{

    [Table("CAPS_VendorAlias")]
    public class VendorAliasUnit : FullAuditedEntity
    {
        /// <summary>Overriding the ID column with VendorAliasId</summary>
        [Column("VendorAliasId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit VendorUnit { get; set; }

        /// <summary>Gets or sets the AliasName field. </summary>
        public virtual string AliasName { get; set; }

    }
}
