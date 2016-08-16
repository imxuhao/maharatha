using System.Data;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Accounts;
using CAPS.CORPACCOUNTING.Uploads.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Uploads
{
    public class UploadAppService :  IUploadAppService
    {

        private readonly AccountUnitAppService _accountUnitAppService;
        private readonly JobUnitAppService _jobUnitAppService;


        public UploadAppService(AccountUnitAppService accountUnitAppService, JobUnitAppService jobUnitAppService)
        {
            _accountUnitAppService = accountUnitAppService;
            _jobUnitAppService = jobUnitAppService;
        }

        /// <summary>
        /// Uploading Excel data to SumitSystem
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="entityName"></param>
        /// <param name="coaId"></param>
        /// <returns></returns>
        public async Task<List<UploadErrorMessagesOutputDto>> UploadExcelData(DataTable dataTable, string entityName, int? coaId)
        {
            List<UploadErrorMessagesOutputDto> errorMessageList=null;
            switch (entityName)
            {
                case "FinancialAccounts":
                    errorMessageList= await _accountUnitAppService.ImportAccounts(dataTable, coaId.Value);
                    break;
                case "Projects":
                    errorMessageList = await _jobUnitAppService.ImportJobs(dataTable);
                    break;

            }
            return errorMessageList;

        }
    }
}
