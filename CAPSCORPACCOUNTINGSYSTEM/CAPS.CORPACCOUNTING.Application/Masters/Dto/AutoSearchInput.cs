using Abp.Application.Services.Dto;


namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class AutoSearchInput: IInputDto
    {
        public long? Id { get; set; }

        public string Query { get; set; }

        public bool Value { get; set; }

        public long? OrganizationId { get; set; }
    }
}
