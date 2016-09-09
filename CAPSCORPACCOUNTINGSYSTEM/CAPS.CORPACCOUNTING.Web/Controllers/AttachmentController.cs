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
        /// Method for saving the multiple uploaded attachment
        /// </summary>
        /// <param name="attachedObjectInput"></param>
        /// <returns></returns>
        public async Task<JsonResult> SaveUploadedAttachments(AttachedObjectUnitInput attachedObjectInput)
        {
            try
            {
                AttachedObjectUnitInput attachedObjectUnitInputLocal = new AttachedObjectUnitInput();

                int iFileCount = Request.Files.Count;
                int iCount = 0;

                //Check input
                if (iFileCount <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("NoFilesAttached_Message"));
                }

                foreach (var item in attachedObjectInput.CreateAttachedObjectUnit)
                {
                    var file = Request.Files[iCount];
                    var fileInfo = new FileInfo(file.FileName);

                    item.FileName = file.FileName;
                    item.FileExtension = fileInfo.Extension;

                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        item.Bytes = binaryReader.ReadBytes(file.ContentLength);
                    }

                    attachedObjectUnitInputLocal.CreateAttachedObjectUnit.Add(item);

                    iCount++;
                }

               await _attachedObjectUnitAppService.CreateAttachedObjectUnit(attachedObjectUnitInputLocal);

                //Return success
                return Json(new AjaxResponse());
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
        public async Task<JsonResult> UploadAttachment(UploadFileData fileData)
        {
            int iFileCount = Request.Files.Count;
            //TODO: Implement this method to accept single file only. Cannot post multiple files due to size restrictions.
            //Use uploadFileData as input dto. Move it to attachment dto folder i.e. create new file for class.
            return Json(new {success = true});
        }
    }
}

public class UploadFileData
{
    public UploadFileData()
    { }
    public string FileName { get; set; }
    public string Description { get; set; }
    public TypeOfAttachedObject TypeOfAttachedObjectId { get; set; }
    public string FileExtension { get; set; }
    public decimal FileSize { get; set; }
    public TypeofObject TypeOfObjectId { get; set; }
    public long ObjectId { get; set; }
}