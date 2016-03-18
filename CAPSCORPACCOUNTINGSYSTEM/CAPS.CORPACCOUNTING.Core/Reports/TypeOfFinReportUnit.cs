using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Reports
{

    /// <summary>
    /// TypeOfFinReport is the table name in Lajit
    /// </summary>

    [Table("CAPS_TypeOfFinReport")]
    public class TypeOfFinReportUnit
    {
        private const int MaxDescriptionLength = 100;
        private const int MaxCaptionLength = 20;

        /// <summary> virtual the ID column with TypeOfReportId field. </summary>
        [Column("TypeOfReportId")]
        public virtual int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        
        public  virtual short TypeOfCategoryId { get; set; }

        [ForeignKey("TypeOfCategoryId")]
        public virtual TypeOfCategoryUnit TypeOfCategory { get; set; }

         

    }
}
