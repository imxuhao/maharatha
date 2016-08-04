using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{

    /// <summary>
    ///  TypeOfCountry is the table name in Lajit
    /// </summary>
    [Table("CAPS_Country")]
    public class CountryUnit:Entity, IMayHaveTenant, IMayHaveOrganizationUnit
    {

        private const int MaxDescriptionLength = 100;
        private const int MaxCaptionLength = 20;
        private const int MaxTwoLetterLength = 2;
        private const int MaxThreeLetterLength = 3;
        private const int MaxIsoNumberLength = 3;


        /// <summary> the ID column with TypeOfCountryId field. </summary>
        [Column("CountryID")]
        public override int Id { get; set; }

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

        ///// <summary>Gets or sets the TypeOfCountryId field. </summary>
        //public virtual short? TypeOfCountryId { get; set; }
        //[ForeignKey("TypeOfCountryId")]
        //public virtual TypeOfCountryUnit TypeOfCountryUnit { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int? TenantId { get; set; }


        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        public CountryUnit()
        { }

        public CountryUnit(string description, string twoLetterAbbreviation, string threeLetterAbbreviation, string isoNumber)
        {
            Description = description;
            TwoLetterAbbreviation = twoLetterAbbreviation;
            ThreeLetterAbbreviation = threeLetterAbbreviation;
            IsoNumber = isoNumber;
        }
    }
}
