using System.Web.Mvc;

namespace CAPS.CORPACCOUNTING.Web.Controllers
{
    public class HomeController : CORPACCOUNTINGControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Home()
        {
            return View();
        }
    }
}