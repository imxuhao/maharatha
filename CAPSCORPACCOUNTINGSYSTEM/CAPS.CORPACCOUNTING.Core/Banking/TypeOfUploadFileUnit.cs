using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Banking
{
    [Table("CAPS_TypeOfUploadFile")]
    public class TypeOfUploadFileUnit : CreationAuditedEntity
    {
        /// <summary> Maximum length of the Description  property.</summary>
        public const int MaxDescriptionLength = 100;

        /// <summary> Maximum length of the Notes  property.</summary>
        public const int MaxNotesLength = 500;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfUploadFileId</summary>
        [Column("TypeOfUploadFileId")]
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

        /// <summary>Gets or sets the UploadFileName field.</summary>
        [StringLength(MaxDescriptionLength)]
        public virtual string UploadFileName { get; set; }

        /// <summary>Gets or sets the UploadOptionA field.</summary>
        public virtual bool? UploadOptionA { get; set; }

        /// <summary>Gets or sets the UploadOptionB field.</summary>
        public virtual bool? UploadOptionB { get; set; }
       
        /// <summary>Gets or sets the UploadOptionC field.</summary>
        public virtual bool? UploadOptionC { get; set; }

        /// <summary>Gets or sets the UploadOptionD field.</summary>
        public virtual bool? UploadOptionD { get; set; }

        /// <summary>Gets or sets the OverrideJobId field.</summary>
        public virtual int? OverrideJobId { get; set; }

        [ForeignKey("OverrideJobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the SecureAccessCategoryIdAssignedByUser field.</summary>
        public virtual int? SecureAccessCategoryIdAssignedByUser { get; set; }        

        #endregion

    }
}
