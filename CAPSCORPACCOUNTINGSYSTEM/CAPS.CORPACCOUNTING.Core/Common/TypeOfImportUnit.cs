using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{
    // <summary>
    /// TypeOfImport is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfImport")]
    public class TypeOfImportUnit : CreationAuditedEntity<short>
    {
        public const int MaxDescLength = 100;
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations      

        /// <summary>Overriding the ID column with TypeOfImportID</summary>
        [Column("TypeOfImportId")]
        public override short Id { get; set; }

        [Required]
        [StringLength(MaxDescLength)]
        /// <summary>Gets or sets the Description field.</summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field.</summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field.</summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field.</summary>
        public virtual string Notes { get; set; }

        #endregion
    }
}
