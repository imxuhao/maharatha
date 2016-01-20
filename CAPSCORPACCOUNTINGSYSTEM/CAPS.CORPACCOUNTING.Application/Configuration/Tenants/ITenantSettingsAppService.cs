using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Configuration.Tenants.Dto;

namespace CAPS.CORPACCOUNTING.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);
    }
}
