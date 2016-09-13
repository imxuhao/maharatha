using Abp;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class UnlinkUserInput : IInputDto
    {
        public int? TenantId { get; set; }

        public long UserId { get; set; }

        public UserIdentifier ToUserIdentifier()
        {
            return new UserIdentifier(TenantId, UserId);
        }
    }
}