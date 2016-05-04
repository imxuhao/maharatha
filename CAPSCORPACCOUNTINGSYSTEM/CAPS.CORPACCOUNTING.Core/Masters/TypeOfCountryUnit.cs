using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///  TypeOfCountry is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfCountry")]
    public class TypeOfCountryUnit:Entity<short>
    {

        private const int MaxDescriptionLength = 100;
        private const int MaxCaptionLength = 20;
        private const int MaxTwoLetterLength = 2;
        private const int MaxThreeLetterLength = 3;
        private const int MaxIsoNumberLength = 3;


        /// <summary> the ID column with TypeOfCountryId field. </summary>
        [Column("TypeOfCountryId")]
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

        /// <summary>Gets or sets the TwoLetterAbbreviation field. </summary>
        [MaxLength(MaxTwoLetterLength)]
        public virtual string TwoLetterAbbreviation { get; set; }

        /// <summary>Gets or sets the ThreeLetterAbbreviation field. </summary>
        [MaxLength(MaxThreeLetterLength)]
        public virtual string ThreeLetterAbbreviation { get; set; }

        /// <summary>Gets or sets the IsoNumber field. </summary>
        [MaxLength(MaxIsoNumberLength)]
        public virtual string IsoNumber { get; set; }

        public TypeOfCountryUnit()
        { }

        public TypeOfCountryUnit(string description,string twoLetterAbbreviation,string threeLetterAbbreviation,string isoNumber)
        {
            Description = description;
            TwoLetterAbbreviation = twoLetterAbbreviation;
            ThreeLetterAbbreviation = threeLetterAbbreviation;
            IsoNumber = isoNumber;
        }
    }
}
