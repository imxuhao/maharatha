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
        private readonly LinesUnitAppService _linesUnitAppService;


        public UploadAppService(AccountUnitAppService accountUnitAppService, JobUnitAppService jobUnitAppService,
            LinesUnitAppService linesUnitAppService)
        {
            _accountUnitAppService = accountUnitAppService;
            _jobUnitAppService = jobUnitAppService;
            _linesUnitAppService = linesUnitAppService;
        }

        /// <summary>
        /// Uploading Excel data to SumitSystem
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="entityName"></param>
        /// <param name="coaId"></param>
        /// <returns></returns>
        public async Task<ErrorMessageswithAccountDto> UploadExcelData(DataTable dataTable, string entityName, int? coaId)
        {
            ErrorMessageswithAccountDto errorMEssageswithAccount=null;
            switch (entityName)
            {
                case "FinancialAccounts":
                     //await _accountUnitAppService.ImportAccounts(dataTable, coaId.Value);
                    break;
                //case "Projects":
                //    errorMessageList = await _jobUnitAppService.ImportJobs(dataTable);
                //    break;
                case "Lines":
                    // await _linesUnitAppService.ImportLines(dataTable, coaId.Value);
                    break;

            }
            return errorMEssageswithAccount;

        }
    }
}
