using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CreateCoaUnitInput : IInputDto
    {
        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(CoaUnit.MaxDisplayNameLength)]
        [Required]
        public string Caption { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(CoaUnit.MaxDesc)]
        public string Description { get; set; }

        /// <summary>Gets or sets the ChartofAccountsType field. </summary>
        [EnumDataType(typeof(ChartofAccountsType))]
        public ChartofAccountsType ChartofAccountsType { get; set; }
        
        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public int? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; } = false;

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public bool IsPrivate { get; set; } = false;

        /// <summary>Gets or sets the OrganizationId field. </summary>
        public long? OrganizationId { get; set; }
    }
}