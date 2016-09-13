using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(TerritoriesUnit))]
    public class TerritoriesUnitDto
    {
        /// <summary>Gets or sets the TerritorieId field. </summary>
        public int TerritorieId { get; set; }
    
        /// <summary>Gets or sets the Description field. </summary>
        public  string Description { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

    }
}
