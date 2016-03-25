using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.Logging.Dto;

namespace CAPS.CORPACCOUNTING.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
