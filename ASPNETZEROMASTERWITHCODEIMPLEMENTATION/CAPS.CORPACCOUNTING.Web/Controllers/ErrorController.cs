using System.Web.Mvc;
using Abp.Auditing;

namespace CAPS.CORPACCOUNTING.Web.Controllers
{
    public class ErrorController : CORPACCOUNTINGControllerBase
    {
        [DisableAuditing]
        public ActionResult E404()
        {
            return View();
        }
    }
}