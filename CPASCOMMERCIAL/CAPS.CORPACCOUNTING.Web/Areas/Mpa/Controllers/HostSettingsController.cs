using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Configuration.Host;
using CAPS.CORPACCOUNTING.Web.Controllers;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Host_Settings)]
    public class HostSettingsController : CORPACCOUNTINGControllerBase
    {
        private readonly IHostSettingsAppService _hostSettingsAppService;

        public HostSettingsController(IHostSettingsAppService hostSettingsAppService)
        {
            _hostSettingsAppService = hostSettingsAppService;
        }

        public async Task<ActionResult> Index()
        {
            var output = await _hostSettingsAppService.GetAllSettings();

            return View(output);
        }
    }
}