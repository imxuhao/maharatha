using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// TypeOfJob is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfJob")]
    public class TypeOfJobUnit : CreationAuditedEntity<short>
    {
        /// <summary>
        /// Maximum Length of Description
        /// </summary>
        public const int MaxDescriptionLength = 100;

        /// <summary>
        /// Maximum Length of Caption
        /// </summary>
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations
        /// <summary> Overriding the ID column with TypeOfChargeID field. </summary>
        [Column("TypeOfJobId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the IsCorporateLedger field. </summary>
        public virtual bool IsCorporateLedger { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 
        #endregion

        public TypeOfJobUnit()
        {
            IsCorporateLedger = false;
            IsActive = true;
        }
    }
}
