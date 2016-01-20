using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Common.Modals;
using CAPS.CORPACCOUNTING.Web.Controllers;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : CORPACCOUNTINGControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}