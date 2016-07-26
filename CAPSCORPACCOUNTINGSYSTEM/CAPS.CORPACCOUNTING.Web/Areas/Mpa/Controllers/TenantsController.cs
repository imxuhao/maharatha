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
        private readonly IAppFolders _appFolders;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly TenantExtendedUnitManager _tenantExtendedUnitManager;
        protected IRepository<TenantExtendedUnit> _tenantExtendedUnitRepository { get; private set; }



        public TenantsController(
            ITenantAppService tenantAppService, 
            TenantManager tenantManager, 
            IEditionAppService editionAppService, IAppFolders appFolders, IBinaryObjectManager binaryObjectManager, TenantExtendedUnitManager tenantExtendedUnitManager)
        {
            _tenantAppService = tenantAppService;
            _tenantManager = tenantManager;
            _editionAppService = editionAppService;
            _appFolders = appFolders;
            _binaryObjectManager = binaryObjectManager;
            _tenantExtendedUnitManager = tenantExtendedUnitManager;
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


        /// <summary>
        /// Sumit Code to upload the CompanyLogo
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize(AppPermissions.Pages_Tenants)]
        public JsonResult UploadCompanyLogo()
        {
            try
            {
                //Check input
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 5242880) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.InputStream);
                if (!fileImage.RawFormat.Equals(ImageFormat.Jpeg) && !fileImage.RawFormat.Equals(ImageFormat.Png))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "sumitOrgImage");

                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                var tempFileName = "sumitOrgImage" + fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                file.SaveAs(tempFilePath);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    return Json(new MvcAjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new MvcAjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        /// <summary>
        /// To get the CompanyLogo
        /// </summary>
        /// <returns></returns>

        [DisableAuditing]
        public async Task<ActionResult> ShowCompanyLogo()
        {
            FileResult picture;
            var tenantExtenedUnit = await _tenantExtendedUnitRepository.FirstOrDefaultAsync(p=>p.TenantId==AbpSession.GetTenantId());
            if (ReferenceEquals(tenantExtenedUnit.Logo,null))
            {
                    picture = GetDefaultCompanyLogo();
            }
            else
            {
                picture = tenantExtenedUnit.Logo!= null ? File(tenantExtenedUnit.Logo, MimeTypeNames.ImageJpeg) : GetDefaultCompanyLogo();
            }
            var contentType = picture.ContentType;
            var fileContent = ((System.Web.Mvc.FileContentResult)picture).FileContents;

            var base64String = Convert.ToBase64String(fileContent);
            return Json((new { image = base64String, contentType = contentType, success = true }), JsonRequestBehavior.AllowGet);
        }

        private FileResult GetDefaultCompanyLogo()
        {
            return File(Server.MapPath("~/Common/Images/default-profile-picture.png"), MimeTypeNames.ImagePng);
        }
    }
}