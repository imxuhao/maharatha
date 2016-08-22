using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using OfficeOpenXml;
using CAPS.CORPACCOUNTING.ExcelTemplates;
using CAPS.CORPACCOUNTING.Uploads;
using CAPS.CORPACCOUNTING.Uploads.Dto;

namespace CAPS.CORPACCOUNTING.Web.Controllers
{
    [AbpMvcAuthorize]
    public class FileUploadController : CORPACCOUNTINGControllerBase
    {
        private readonly IAppFolders _appFolders;
        private readonly UploadAppService _uploadAppService;

        public FileUploadController(IAppFolders appFolders, UploadAppService uploadAppService)
        {
            _appFolders = appFolders;
            _uploadAppService = uploadAppService;
        }

        public async Task<JsonResult> UploadExcelFile(string entityName, int? coaId)
        {
            try
            {
                //Check input
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ExcelUploadError"));
                }

                var file = Request.Files[0];


                //Check file type & format
                var fileInfo = new FileInfo(file.FileName);
                if (fileInfo.Extension != ".xls" && fileInfo.Extension != ".xlsx")
                {
                    throw new ApplicationException("Uploaded file is not an accepted Excel file !");
                }

                ExcelPackage package = new ExcelPackage(Request.InputStream);

                // Uploap the worksheet data to a DataTable.
                DataTable worksheetData = ExcelHelper.ConvertExcelToDataTable(package, entityName);
                ErrorMessageswithAccountDto errorMessageList = await _uploadAppService.UploadExcelData(worksheetData, entityName, coaId);
               

                return Json(new AjaxResponse(new { success = errorMessageList==null, errorMessageList }));

            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }



    }
}