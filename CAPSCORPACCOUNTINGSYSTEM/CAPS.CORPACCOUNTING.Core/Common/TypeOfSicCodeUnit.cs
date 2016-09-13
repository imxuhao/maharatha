using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  TypeOfSicCode is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfSicCode")]
    public class TypeOfSicCodeUnit:CreationAuditedEntity
    {

        public const int MaxDescriptionLength = 100;
        public const int MaxCaptionLength = 20;
        public const int MaxSicCodeLength = 20;

        /// <summary> Overriding the ID column with TypeOfSicCodeId field. </summary>
        [Column("TypeOfSicCodeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the SicCode field. </summary>
        [StringLength(MaxSicCodeLength)]
        public virtual string SicCode { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

    }
}
