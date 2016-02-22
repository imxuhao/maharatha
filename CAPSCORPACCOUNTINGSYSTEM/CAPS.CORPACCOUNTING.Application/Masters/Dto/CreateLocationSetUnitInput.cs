using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CreateLocationSetUnitInput : IInputDto
    {
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

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
