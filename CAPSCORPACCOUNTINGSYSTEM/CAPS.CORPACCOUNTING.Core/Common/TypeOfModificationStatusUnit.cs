using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;


namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// TypeOfModificationStatus is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfModificationStatus")]
    public class TypeOfModificationStatusUnit : CreationAuditedEntity<short>
    {
        public const int MaxDescLength = 50;

        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfModificationStatusId</summary>
        [Column("TypeOfModificationStatusId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the Description field.</summary>
        [StringLength(MaxDescLength)]
        public string Description { get; set; } 

        /// <summary>Gets or sets the Caption field.</summary>
        [StringLength(MaxCaptionLength)]
        public string Caption { get; set; }  

        /// <summary>Gets or sets the DisplaySequence field.</summary>
        public short? DisplaySequence { get; set; } 

        /// <summary>Gets or sets the Notes field.</summary>
        public string Notes { get; set; } 

        #endregion
    }
}
