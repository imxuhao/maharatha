using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Accounts;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.ExcelTemplates.Dto;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateExporterAppService : CORPACCOUNTINGServiceBase, ITemplateExporterAppService
    {

        private readonly ITemplate _accountsTemplate;
        private readonly JobsTemplate _jobsTemplate;
        


        /// <param name="accountsTemplate"></param>
        public TemplateExporterAppService(AccountsTemplate accountsTemplate, JobsTemplate jobsTemplate)
        {
            _accountsTemplate = accountsTemplate;
            _jobsTemplate = jobsTemplate;
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FileDto> GetTemplateByEntity(TemplateInputUnit input)
        {
            FileDto fileDto = new FileDto();
            switch (input.EntityName)
            {
                case "FinancialAccounts":
                    fileDto = await _accountsTemplate.DownLoadTemplate(input.CoaId.Value);
                    break;
                case "Projects":
                    fileDto = await _jobsTemplate.DownLoadTemplate(0);
                    break;
            }

            return fileDto;
        }
      
    }
}
