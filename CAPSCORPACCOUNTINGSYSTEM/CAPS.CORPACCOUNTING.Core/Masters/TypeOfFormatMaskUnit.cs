using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters
{
    public enum TypeOfMask
    {
        [Display(Name = "Amount")]
        Amount=1,
        [Display(Name = "Account")]
        Account = 2,
        [Display(Name = "Sub-Account")]
        SubAccount = 3,
        [Display(Name = "Job")]
        Job = 4,
        [Display(Name = "Date")]
        Date = 5
    }

    /// <summary>
    /// TypeOfFormatMask is the table name in lajit
    /// </summary>
    [Table("CAPS_TypeOfFormatMask")]
    public class TypeOfFormatMaskUnit : CreationAuditedEntity<short>
    {
        public const int MaxDescLength = 100;  
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfFormatMaskId</summary>
        [Column("TypeOfFormatMaskId")]
        public override short Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; } 

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the FormatMask field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string FormatMask { get; set; } 

        /// <summary>Gets or sets the IsIntegerRequired field. </summary>
        public virtual bool IsIntegerRequired { get; set; }

        /// <summary>Gets or sets the IsNumberRequired field. </summary>
        public virtual bool IsNumberRequired { get; set; }

        /// <summary>Gets or sets the TypeOfMaskID field. </summary>
        public virtual TypeOfMask? TypeOfMaskId { get; set; } 

        #endregion
    }
}
