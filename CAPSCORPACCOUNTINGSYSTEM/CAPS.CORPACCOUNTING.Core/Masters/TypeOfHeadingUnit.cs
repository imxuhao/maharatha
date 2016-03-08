using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// Enum for TypeOfHeadingGroup
    /// </summary>
    public enum TypeOfHeadingGroup
    {
        [Display(Name = "Accounts")]
        Accounts = 1,
        [Display(Name = "Projects")]
        Projects = 2,
        [Display(Name = "Sub-Accounts")]
        SubAccounts = 3,
        [Display(Name = "References")]
        References = 4,
        [Display(Name = "Amounts")]
        Amounts = 5,
        [Display(Name = "Accounting Entry Layout")]
        AccountingEntryLayout = 6,
        [Display(Name = "Miscellaneous")]
        Miscellaneous = 7
    }

    /// <summary>
    /// TypeOfHeading is the Table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfHeading")]
    public class TypeOfHeadingUnit : CreationAuditedEntity
    {
        /// <summary>MaxDescription Length </summary>
        public const int MaxDescLenght = 100;

        /// <summary>Max Caption Length </summary>
        public const int MaxCaptionLenght = 40;

        /// <summary>Max Notes Length </summary>
        public const int MaxNoteLenght = 500;


        #region Declaration of Properties
        /// <summary>Overriding the ID column with TypeOfHeadingID</summary>
        [Column("TypeOfHeadingID")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLenght)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLenght)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        [StringLength(MaxNoteLenght)]
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfHeadingGroupID field. </summary>
        public virtual TypeOfHeadingGroup TypeOfHeadingGroupId { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool IsDefault { get; set; } 

        #endregion

        public TypeOfHeadingUnit()
        {
            IsDefault = false;
        }
    }
}
