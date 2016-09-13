using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using CAPS.CORPACCOUNTING.Web.Controllers;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : CORPACCOUNTINGControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}