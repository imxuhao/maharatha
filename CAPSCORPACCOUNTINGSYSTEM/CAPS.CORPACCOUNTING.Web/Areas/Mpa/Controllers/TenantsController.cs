using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Abp.Web.Mvc.Models;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Editions;
using CAPS.CORPACCOUNTING.IO;
using CAPS.CORPACCOUNTING.MultiTenancy;
using CAPS.CORPACCOUNTING.Web.Areas.Mpa.Models.Tenants;
using CAPS.CORPACCOUNTING.Web.Controllers;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using CAPS.CORPACCOUNTING.Net.MimeTypes;
using CAPS.CORPACCOUNTING.Storage;

namespace CAPS.CORPACCOUNTING.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenants)]
    public class TenantsController : CORPACCOUNTINGControllerBase
    {
        private readonly ITenantAppService _tenantAppService;
        private readonly TenantManager _tenantManager;
        private readonly IEditionAppService _editionAppService;

        public TenantsController(
            ITenantAppService tenantAppService, 
            TenantManager tenantManager, 
            IEditionAppService editionAppService)
        {
            _tenantAppService = tenantAppService;
            _tenantManager = tenantManager;
            _editionAppService = editionAppService;
        }

        public ActionResult Index()
        {
            ViewBag.FilterText = Request.QueryString["filterText"];
            return View();
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_Create)]
        public async Task<PartialViewResult> CreateModal()
        {
            var editionItems = await _editionAppService.GetEditionComboboxItems();
            var viewModel = new CreateTenantViewModel(editionItems);

            return PartialView("_CreateModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<PartialViewResult> EditModal(int id)
        {
            var tenantEditDto = await _tenantAppService.GetTenantForEdit(new EntityRequestInput(id));
            var editionItems = await _editionAppService.GetEditionComboboxItems(tenantEditDto.EditionId);
            var viewModel = new EditTenantViewModel(tenantEditDto, editionItems);

            return PartialView("_EditModal", viewModel);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task<PartialViewResult> FeaturesModal(int id)
        {
            var tenant = await _tenantManager.GetByIdAsync(id);
            var output = await _tenantAppService.GetTenantFeaturesForEdit(new EntityRequestInput(id));
            var viewModel = new TenantFeaturesEditViewModel(tenant, output);

            return PartialView("_FeaturesModal", viewModel);
        }

    }
}