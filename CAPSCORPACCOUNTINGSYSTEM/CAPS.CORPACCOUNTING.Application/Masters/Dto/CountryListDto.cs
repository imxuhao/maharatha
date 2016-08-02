using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CountryListDto
    {
        /// <summary>
        /// Gets or Sets CountryId
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or Sets TwoLetterAbbreviation
        /// </summary>
        public string IsoCode { get; set; }
    }
}
