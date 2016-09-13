using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Banking
{
    public enum TyeofUpload
    {
        [Display(Name = "UploadMethod")]
        UploadMethod = 1,
        [Display(Name = "PositivePayFile")]
        PositivePayFile = 2
    }

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

        /// <summary>Gets or sets the TyeofUpload field.</summary>
        public virtual TyeofUpload? TypeofUploadId { get; set; }

        /// <summary>Gets or sets the SecureAccessCategoryIdAssignedByUser field.</summary>
        public virtual short? SecureAccessCategoryIdAssignedByUser { get; set; }

        #endregion
        public TypeOfUploadFileUnit()
        {
            UploadOptionA = false;
            UploadOptionB = false;
            UploadOptionC = false;
            UploadOptionD = false;
        }

        public TypeOfUploadFileUnit( string description, int? displaysequence, string notes, string uploadfilename, bool? uploadoptiona,
            bool? uploadoptionb, bool? uploadoptionc, bool? uploadoptiond, int? overridejobid, TyeofUpload? typeofuploadid)
        {
           
            Description = description;
            DisplaySequence = displaysequence;
            Notes = notes;
            UploadFileName = uploadfilename;
            UploadOptionA = uploadoptiona;
            UploadOptionB = uploadoptionb;
            UploadOptionC = uploadoptionc;
            UploadOptionD = uploadoptiond;
            OverrideJobId = overridejobid;
            TypeofUploadId = typeofuploadid;
        }
    }
}


