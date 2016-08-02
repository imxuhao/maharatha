using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class RegionListDto
    {

        /// <summary>
        /// Gets or Sets CountryId
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or Sets RegionAbbreviation
        /// </summary>
        public string StateCode { get; set; }
    }
}
