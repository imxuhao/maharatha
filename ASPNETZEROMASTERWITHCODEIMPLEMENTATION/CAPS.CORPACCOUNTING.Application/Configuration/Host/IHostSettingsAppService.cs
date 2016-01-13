using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;

namespace CAPS.CORPACCOUNTING.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);
    }
}
