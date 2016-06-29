using Abp.Application.Services.Dto;


namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class IdInputExtensionDto : IdInput
    { 
        public long? OrganizationUnitId { get; set; }
      
    }
    public class IdInputExtensionDto<T1> : IdInput<T1>
    {
        public long? OrganizationUnitId { get; set; }

    }
    public class IdInputExtensionDto<T1, T2> : IdInput<T2>
    {

        public T1 Value { get; set; }
    }
}
