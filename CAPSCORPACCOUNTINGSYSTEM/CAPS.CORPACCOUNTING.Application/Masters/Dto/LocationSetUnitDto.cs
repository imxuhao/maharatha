using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(LocationSetUnit))]
    public class LocationSetUnitDto:IOutputDto
    {
        /// <summary>>Gets or sets the Number field.</summary>       
        public int LocationSetId { get; set; }
        /// <summary>Gets or sets the TypeOfLocationSetId field. </summary>
        public LocationSets TypeOfLocationSetId { get; set; }

        /// <summary>Gets or sets the Number field. </summary>       
        public string Number { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
