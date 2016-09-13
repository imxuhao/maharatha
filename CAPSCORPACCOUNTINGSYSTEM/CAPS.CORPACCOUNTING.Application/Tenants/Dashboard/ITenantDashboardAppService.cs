using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Tenants.Dashboard.Dto;

namespace CAPS.CORPACCOUNTING.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
