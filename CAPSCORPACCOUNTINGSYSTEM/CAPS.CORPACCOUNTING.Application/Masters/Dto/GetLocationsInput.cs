using Abp.Application.Services.Dto;


namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetLocationsInput : AutoSearchInput
    {
        public LocationSets? LocationSetTypeId { get; set; }
    }
}
