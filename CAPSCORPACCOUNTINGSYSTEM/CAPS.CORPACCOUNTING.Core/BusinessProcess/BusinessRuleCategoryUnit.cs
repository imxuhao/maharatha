using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    /// <summary>
    ///  BusinessRuleCategory is the table name in Lajit
    /// </summary>
    [Table("CAPS_BusinessRuleCategory")]
    public class BusinessRuleCategoryUnit
    {
        private const int MaxCaptionLength = 20;
        private const int MaxDescriptionLength = 100;

        /// <summary>ID column with BusinessRuleCategoryId field. </summary>
        [Column("BusinessRuleCategoryId")]
        public virtual short Id { get; set; }
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }
    }
}
