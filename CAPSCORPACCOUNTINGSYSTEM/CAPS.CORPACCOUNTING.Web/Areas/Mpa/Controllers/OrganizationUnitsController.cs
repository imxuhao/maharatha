using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.Web.Mvc.Authorization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.OrganizationUnits;
using CAPS.CORPACCOUNTING.Web.Controllers;
using Abp.Domain.Uow;
using Abp.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using CAPS.CORPACCOUNTING.IO;
using System.IO;
using Abp.Web.Mvc.Models;
using Abp.Web.Models;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitsController : CORPACCOUNTINGControllerBase
    {
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IAppFolders _appFolders;
        public OrganizationUnitsController(IRepository<OrganizationUnit, long> organizationUnitRepository, IAppFolders appFolders)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _appFolders = appFolders;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public PartialViewResult CreateModal(long? parentId)
        {
            return PartialView("_CreateModal", new CreateOrganizationUnitModalViewModel(parentId));
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<PartialViewResult> EditModal(long id)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(id);
            var model = organizationUnit.MapTo<EditOrganizationUnitModalViewModel>();

            return PartialView("_EditModal", model);
        }


        
    }
}