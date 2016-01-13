using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}