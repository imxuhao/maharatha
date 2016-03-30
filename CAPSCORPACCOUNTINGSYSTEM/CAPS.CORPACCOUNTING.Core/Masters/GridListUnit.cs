using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_GridList")]
   public class GridListUnit : CreationAuditedEntity, IMustHaveTenant
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with GridId</summary>
        [Column("GridId"),DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Name field. </summary>
        [Required]
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        public GridListUnit(int gridid, string name, string description)
        {
            Id = gridid;
            Name = name;
            Description = description;
        }

        #endregion
    }
}
