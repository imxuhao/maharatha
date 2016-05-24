using Abp.Application.Services.Dto;


namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class IdInputExtensionDto : IdInput
    { 
        public long? OrganizationUnitId { get; set; }
      
    }
}
