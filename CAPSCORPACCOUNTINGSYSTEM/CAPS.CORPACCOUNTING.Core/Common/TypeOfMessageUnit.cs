using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    ///  TypeOfMessage is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfMessage")]
    public class TypeOfMessageUnit : CreationAuditedEntity<short>
    {

        public const int MaxCaptionLength = 20;

        /// <summary> Overriding the ID column with TypeOfMessageID field. </summary>
        [Column("TypeOfMessageId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }
     }
}