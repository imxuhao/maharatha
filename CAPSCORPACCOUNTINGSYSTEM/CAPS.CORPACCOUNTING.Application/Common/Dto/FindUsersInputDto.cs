using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Common.Dto
{
    public class FindUsersInputDto : SearchInputDto
    {
        public int? TenantId { get; set; }
    }
}