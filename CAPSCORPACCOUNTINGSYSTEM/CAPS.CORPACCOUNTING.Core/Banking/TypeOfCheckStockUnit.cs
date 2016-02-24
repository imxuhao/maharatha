using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace CAPS.CORPACCOUNTING.Banking
{
    [Table("CAPS_TypeOfCheckStock")]
    public class TypeOfCheckStockUnit : CreationAuditedEntity
    {
        /// <summary> Maximum length of the Description  property.</summary>
        public const int MaxDescriptionLength = 100;

        /// <summary> Maximum length of the Notes  property.</summary>
        public const int MaxNotesLength = 500;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfBankAccountId</summary>
        [Column("TypeOfCheckStockId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field.</summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field.</summary>
        public virtual int? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field.</summary>
        [StringLength(MaxNotesLength)]
        public virtual string Notes { get; set; }

      
        #endregion

    }
}
