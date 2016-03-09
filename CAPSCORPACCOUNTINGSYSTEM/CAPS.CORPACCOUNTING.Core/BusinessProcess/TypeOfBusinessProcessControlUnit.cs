
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    /// <summary>
    ///  TypeOfBusinessProcessControl is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfBusinessProcessControl")]
  public class TypeOfBusinessProcessControlUnit
    {
        private const int MaxCaptionLength = 20;
        private const int MaxDescriptionLength = 100;

        /// <summary> Overriding the ID column with TypeOfBusinessProcessControlId field. </summary>
        [Column("TypeOfBusinessProcessControlId")]
        public virtual int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the ControlValue field. </summary>
        public virtual string ControlValue { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }
    }
}
