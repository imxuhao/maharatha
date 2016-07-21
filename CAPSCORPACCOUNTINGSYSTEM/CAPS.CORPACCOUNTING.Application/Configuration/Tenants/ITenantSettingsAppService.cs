using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Configuration.Tenants.Dto;

namespace CAPS.CORPACCOUNTING.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        /// <summary>
        /// Sumit Method to GetAllSettings
        /// </summary>
        /// <returns></returns>
        Task<TenantSettingsEditDto> GetAllTenantSettings();
        

        /// <summary>
        /// Sumit Method to  Update AllSettings
        /// </summary>
        /// <returns></returns>
       Task UpdateAllTenantSettings(TenantSettingsEditDto input);
    }
}
