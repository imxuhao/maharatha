using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// TypeOfCurrencyRate is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfCurrencyRate")]
    public class TypeOfCurrencyRateUnit : CreationAuditedEntity<short>
    {
        public const int MaxDescLength = 100;

        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfCurrencyRateId</summary>
        [Column("TypeOfCurrencyRateId")]
        public override short Id { get; set; }

        [Required]
        [StringLength(MaxDescLength)]
        /// <summary>Gets or sets the Description field.</summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field.</summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field.</summary>
        public virtual short? DisplaySequence { get; set; }  

        /// <summary>Gets or sets the Notes field.</summary>
        public virtual string Notes { get; set; } 

        /// <summary>Gets or sets the TypeOfUploadFileID field.</summary>
        public virtual int? TypeOfUploadFileId { get; set; }
        #endregion



        public TypeOfCurrencyRateUnit() { }
        public TypeOfCurrencyRateUnit(string description, string caption, string notes, int? typeOfUploadFileId)
        {
            Description = description;
            Caption = caption;
            Notes = notes;
            TypeOfUploadFileId = typeOfUploadFileId;
        }
    }
}
