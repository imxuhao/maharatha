using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(TerritoriesUnit))]
    public class UpdateTerritoriesUnitInput
    {
        /// <summary>Gets or sets the TerritorieId field. </summary>
        public virtual int TerritorieId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(TerritoriesUnit.MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
