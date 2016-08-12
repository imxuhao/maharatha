using System.Data;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Accounts;
using CAPS.CORPACCOUNTING.Uploads.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Uploads
{
    public class UploadAppService :  IUploadAppService
    {

        private readonly AccountUnitAppService _accountUnitAppService;

        public UploadAppService(AccountUnitAppService accountUnitAppService)
        {
            _accountUnitAppService = accountUnitAppService;
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

            }
            return errorMessageList;

        }
    }
}
