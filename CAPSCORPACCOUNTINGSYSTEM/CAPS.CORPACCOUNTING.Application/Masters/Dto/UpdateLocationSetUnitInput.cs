using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class UpdateLocationSetUnitInput : IInputDto
    {
        /// <summary>>Gets or sets the LocationSetId field.</summary>    
        [Range(1,Int32.MaxValue)]
        public int LocationSetId { get; set; }
       
        /// <summary>Gets or sets the TypeOfLocationSetId field. </summary>
        [EnumDataType(typeof(LocationSets))]
        public LocationSets TypeOfLocationSetId { get; set; }

        /// <summary>Gets or sets the Number field. </summary>
        [StringLength(LocationSetUnit.MaxNumberLength)]
        public string Number { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(LocationSetUnit.MaxDescriptionLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
