using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System;

namespace CAPS.CORPACCOUNTING.Common
{
    // <summary>
    /// TypeOfGenericProcess is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfGenericProcess")]
    public class TypeOfGenericProcessUnit : CreationAuditedEntity
    {
        public const int MaxDescLength = 100;
        public const int MaxCaptionLength = 20;
        public const int MaxRangeLength = 50;

        #region Class Property Declarations      

        /// <summary>Overriding the ID column with TypeOfGenericProcessId</summary>
        [Column("TypeOfGenericProcessId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field.</summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field.</summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field.</summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field.</summary>
        public virtual string Notes { get; set; } 

        /// <summary>Gets or sets the Choose1 field.</summary>
        public virtual int? Choose1 { get; set; }  

        /// <summary>Gets or sets the Choose2 field.</summary>
        public virtual int? Choose2 { get; set; } 

        /// <summary>Gets or sets the Choose3 field.</summary>
        public virtual int? Choose3 { get; set; } 

        /// <summary>Gets or sets the Choose4 field.</summary>
        public virtual int? Choose4 { get; set; } 

        /// <summary>Gets or sets the Choose5 field.</summary>
        public virtual int? Choose5 { get; set; }

        /// <summary>Gets or sets the StartDate1 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate1 { get; set; }

        /// <summary>Gets or sets the EndDate1 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate1 { get; set; }

        /// <summary>Gets or sets the StartDate2 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate2 { get; set; }

        /// <summary>Gets or sets the EndDate2 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate2 { get; set; }

        /// <summary>Gets or sets the StartDate3 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate3 { get; set; }

        /// <summary>Gets or sets the EndDate3 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate3 { get; set; }

        /// <summary>Gets or sets the StartRange1 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange1 { get; set; }

        /// <summary>Gets or sets the EndRange1 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange1 { get; set; }

        /// <summary>Gets or sets the StartRange2 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange2 { get; set; }

        /// <summary>Gets or sets the EndRange2 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange2 { get; set; }

        /// <summary>Gets or sets the StartRange3 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange3 { get; set; }

        /// <summary>Gets or sets the EndRange3 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange3 { get; set; } 

        /// <summary>Gets or sets the Select1 field.</summary>
        public virtual bool? Select1 { get; set; } 

        /// <summary>Gets or sets the Select2 field.</summary>
        public virtual bool? Select2 { get; set; } 

        /// <summary>Gets or sets the Select3 field.</summary>
        public virtual bool? Select3 { get; set; } 

        /// <summary>Gets or sets the Select4 field.</summary>
        public virtual bool? Select4 { get; set; } 

        /// <summary>Gets or sets the Select5 field.</summary>
        public virtual bool? Select5 { get; set; } 

        /// <summary>Gets or sets the Choose6 field.</summary>
        public virtual int? Choose6 { get; set; } 

        /// <summary>Gets or sets the Choose7 field.</summary>
        public virtual int? Choose7 { get; set; } 

        /// <summary>Gets or sets the Choose8 field.</summary>
        public virtual int? Choose8 { get; set; } 

        /// <summary>Gets or sets the Choose9 field.</summary>
        public virtual int? Choose9 { get; set; } 

        /// <summary>Gets or sets the Choose10 field.</summary>
        public virtual int? Choose10 { get; set; }

        /// <summary>Gets or sets the StartDate4 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate4 { get; set; }

        /// <summary>Gets or sets the EndDate4 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate4 { get; set; }

        /// <summary>Gets or sets the StartDate5 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate5 { get; set; }

        /// <summary>Gets or sets the EndDate5 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate5 { get; set; }

        /// <summary>Gets or sets the StartDate6 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate6 { get; set; }

        /// <summary>Gets or sets the EndDate6 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate6 { get; set; }

        /// <summary>Gets or sets the StartDate7 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate7 { get; set; }

        /// <summary>Gets or sets the EndDate7 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate7 { get; set; }

        /// <summary>Gets or sets the StartDate8 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate8 { get; set; }

        /// <summary>Gets or sets the EndDate8 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate8 { get; set; }

        /// <summary>Gets or sets the StartDate9 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate9 { get; set; }

        /// <summary>Gets or sets the EndDate9 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate9 { get; set; }

        /// <summary>Gets or sets the StartDate10 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate10 { get; set; }

        /// <summary>Gets or sets the EndDate10 field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate10 { get; set; }

        /// <summary>Gets or sets the StartRange4 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange4 { get; set; }

        /// <summary>Gets or sets the EndRange4 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange4 { get; set; }

        /// <summary>Gets or sets the StartRange5 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange5 { get; set; }

        /// <summary>Gets or sets the EndRange5 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange5 { get; set; }

        /// <summary>Gets or sets the StartRange6 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange6 { get; set; }

        /// <summary>Gets or sets the EndRange6 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange6 { get; set; }

        /// <summary>Gets or sets the StartRange7 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange7 { get; set; }

        /// <summary>Gets or sets the EndRange7 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange7 { get; set; }

        /// <summary>Gets or sets the StartRange8 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange8 { get; set; }

        /// <summary>Gets or sets the EndRange8 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange8 { get; set; }

        /// <summary>Gets or sets the StartRange9 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange9 { get; set; }

        /// <summary>Gets or sets the EndRange9 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange9 { get; set; }

        /// <summary>Gets or sets the StartRange10 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string StartRange10 { get; set; }

        /// <summary>Gets or sets the EndRange10 field.</summary>
        [StringLength(MaxRangeLength)]
        public virtual string EndRange10 { get; set; }

        /// <summary>Gets or sets the Select6 field.</summary>
        public virtual bool? Select6 { get; set; } 

        /// <summary>Gets or sets the Select7 field.</summary>
        public virtual bool? Select7 { get; set; } 

        /// <summary>Gets or sets the Select8 field.</summary>
        public virtual bool? Select8 { get; set; } 

        /// <summary>Gets or sets the Select9 field.</summary>
        public virtual bool? Select9 { get; set; } 

        /// <summary>Gets or sets the Select10 field.</summary>
        public virtual bool? Select10 { get; set; } 
        #endregion
    }
}
