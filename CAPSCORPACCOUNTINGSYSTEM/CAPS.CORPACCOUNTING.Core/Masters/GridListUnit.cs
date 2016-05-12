using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_SystemViewList")]
    public class GridListUnit : Entity
    {
        #region Class Property Declarations
        public const int MaxNameLength = 300;
        /// <summary>Overriding the ID column with GridId</summary>
        [Column("ViewId"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Name field. </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        public GridListUnit()
        {
            IsActive = true;
        }
        public GridListUnit(int gridid, string name, string description,bool isactive)
        {
            Id = gridid;
            Name = name;
            Description = description;
            IsActive = isactive;
        }

        #endregion
    }
}
