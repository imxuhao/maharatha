using System.Threading.Tasks;
using Abp.Configuration;

namespace CAPS.CORPACCOUNTING.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
