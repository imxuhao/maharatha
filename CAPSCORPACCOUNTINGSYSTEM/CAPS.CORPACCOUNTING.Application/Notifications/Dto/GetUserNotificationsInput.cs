using Abp.Notifications;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}