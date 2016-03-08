using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// TypeOfAccountingLayout is the Table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfAccountingLayout")]
    public class TypeOfAccountingLayoutUnit : CreationAuditedEntity
    {
        /// <summary>MaxDescription Length </summary>
        public const int MaxDescLenght = 100;

        /// <summary>Max Notes Length </summary>
        public const int MaxNoteLenght = 500;


        #region Declaration of Properties
       
        /// <summary>Overriding the ID column with TypeOfAccountingLayoutID</summary>
        [Column("TypeOfAccountingLayoutID")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfHeadingGroupID field. </summary>
        public TypeOfHeadingGroup? TypeOfHeadingGroupId { get; set; } 

        /// <summary>Gets or sets the TypeOfEntryLayoutHeadingID field. </summary>
        public short? TypeOfEntryLayoutHeadingId { get; set; }

        /// <summary>Gets or sets the TypeOfAccountingLayoutHeadingID field. </summary>
        public short? TypeOfAccountingLayoutHeadingId { get; set; }

        /// <summary>Gets or sets the DescriptionInternalUseOnly field. </summary>
        [StringLength(MaxDescLenght)]
        public string DescriptionInternalUseOnly { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public short DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>

        [StringLength(MaxNoteLenght)]
        public string Notes { get; set; } 

        /// <summary>Gets or sets the IsDisplayedOnFirstPage field. </summary>
        public bool IsDisplayedOnFirstPage { get; set; }

        /// <summary>Gets or sets the IsHidden field. </summary>
        public bool IsHidden { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } 

        #endregion

        public TypeOfAccountingLayoutUnit()
        {
            IsDisplayedOnFirstPage = true;
            IsHidden = false;
            IsActive = true;
        }
    }
}
