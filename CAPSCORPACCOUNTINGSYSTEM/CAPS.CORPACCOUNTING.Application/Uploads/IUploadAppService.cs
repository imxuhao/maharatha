using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;

namespace CAPS.CORPACCOUNTING.Uploads
{
    public interface IUploadAppService:IApplicationService 
    {
        /// <summary>
        /// Uploading Excel data to SumitSystem
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="entityName"></param>
        /// <param name="coaId"></param>
        /// <returns></returns>
        Task UploadExcelData(DataTable dataTable, string entityName, int? coaId);
    }
}
