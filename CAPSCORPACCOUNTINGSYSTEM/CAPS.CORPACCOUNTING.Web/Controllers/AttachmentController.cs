using Abp.Application.Services.Dto;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using CAPS.CORPACCOUNTING.Attachments;
using CAPS.CORPACCOUNTING.Attachments.Dto;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AttachmentController : CORPACCOUNTINGControllerBase
    {
        private readonly IAppFolders _appFolders;
        private readonly IAttachedObjectUnitAppService _attachedObjectUnitAppService;

        public AttachmentController(IAppFolders appFolders, IAttachedObjectUnitAppService attachedObjectUnitAppService)
        {
            _appFolders = appFolders;
            _attachedObjectUnitAppService = attachedObjectUnitAppService;
        }

        /// <summary>
        /// Action for saving the uploaded attachment
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public async Task<JsonResult> UploadAttachment(UploadFileDataInput fileData)
        {

            try
            {
                int iFileCount = Request.Files.Count;
                var file = Request.Files[0];

                //Check input
                if (iFileCount <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("NoFilesAttached_Message"));
                }

                var createAttachedObjectInputUnit = new CreateAttachedObjectInputUnit()
                {
                    FileName = fileData.FileName,
                    Description = fileData.Description,
                    TypeOfAttachedObjectId = fileData.TypeOfAttachedObjectId,
                    FileExtension = fileData.FileExtension,
                    FileSize = Convert.ToInt32(fileData.FileSize),
                    TypeOfObjectId = fileData.TypeOfObjectId,
                    ObjectId = fileData.ObjectId
                };

                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    createAttachedObjectInputUnit.Bytes = binaryReader.ReadBytes(file.ContentLength);
                }

                await _attachedObjectUnitAppService.CreateAttachedObjectUnit(createAttachedObjectInputUnit);

                return Json(new { success = true });
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }


        /// <summary>
        /// GET the file and downloads it.
        /// </summary>
        /// <param name="getFileAttachedInputUnitId"></param>
        /// <returns></returns>
        public async Task<FileResult> GetFilesById(GetFileAttachedObjectInputUnit getFileAttachedInputUnitId)
        {
            var attachedObjectUnit = await _attachedObjectUnitAppService.GetFileAttachedObjecUnit(getFileAttachedInputUnitId);

            return File(attachedObjectUnit.Bytes, attachedObjectUnit.FileExtension);
        }
    }
}

