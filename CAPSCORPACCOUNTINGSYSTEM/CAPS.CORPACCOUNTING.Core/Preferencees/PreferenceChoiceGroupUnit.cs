using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace CAPS.CORPACCOUNTING.Preferencees
{
   

    /// <summary>
    /// PreferenceChoiceGroup is the table name in Lajit
    /// </summary>
    [Table("CAPS_PreferenceChoiceGroup")]
    public class PreferenceChoiceGroupUnit:CreationAuditedEntity
    {
        /// <summary> Maximum Length of Description </summary>
        public const int MaxDescLength = 100;

        /// <summary> Maximum Length of Caption </summary>
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with PreferenceChoiceGroupId</summary>
        [Column("PreferenceChoiceGroupId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }
        
        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; } 

        #endregion
    }
}
