﻿using System.Threading.Tasks;
using Abp.Notifications;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.MultiTenancy;

namespace CAPS.CORPACCOUNTING.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(long userId, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
