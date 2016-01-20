using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Sessions.Dto;

namespace CAPS.CORPACCOUNTING.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
