using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public TemplateExporterAppService(AccountsTemplate accountsTemplate,JobsTemplate jobsTemplate)
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
            switch (input.entityName)
            {
                case "FinancialAccounts":
                    fileDto = await _accountsTemplate.DownLoadTemplate();
                    break;
                case "Projects":
                    fileDto = await _jobsTemplate.DownLoadTemplate();
                    break;
            }

            return fileDto;
        }
    }
}
