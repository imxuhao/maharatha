using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// TypeOfOtherName is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfOtherName")]
    public class TypeOfOtherNameUnit : CreationAuditedEntity<short>
    {
        public const int MaxDescLength = 100;     

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfOtherNameId</summary>
        [Column("TypeOfOtherNameId")]
        public override short Id { get; set; }
       
        [StringLength(MaxDescLength)]
        /// <summary>Gets or sets the Description field.</summary>
        public virtual string Description { get; set; }
       
        /// <summary>Gets or sets the DisplaySequence field.</summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field.</summary>
        public virtual string Notes { get; set; }

        #endregion

    }
}
