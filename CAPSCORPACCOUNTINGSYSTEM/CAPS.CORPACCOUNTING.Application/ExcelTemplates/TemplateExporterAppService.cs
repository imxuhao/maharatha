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

        private readonly AccountsTemplate _accountsTemplate;
        private readonly ProjectsTemplate _projectsTemplate;
        private readonly LinesTemplate _linesTemplate;
        private readonly DivisionTemplate _divisionsTemplate;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountsTemplate"></param>
        /// <param name="projectsTemplate"></param>
        /// <param name="divisionsTemplate"></param>
        /// <param name="linesTemplate"></param>
        public TemplateExporterAppService(AccountsTemplate accountsTemplate,
            ProjectsTemplate projectsTemplate,
              DivisionTemplate divisionsTemplate,
                LinesTemplate linesTemplate


            )
        {
            _accountsTemplate = accountsTemplate;
            _projectsTemplate = projectsTemplate;
            _divisionsTemplate = divisionsTemplate;
            _linesTemplate = linesTemplate;

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
                    fileDto = await _projectsTemplate.DownLoadTemplate(0);
                    break;
                case "Divisions":
                    fileDto = await _divisionsTemplate.DownLoadTemplate(0);
                    break;
                case "Lines":
                    fileDto = await _linesTemplate.DownLoadTemplate(0);
                    break;
            }

            return fileDto;
        }
      
    }
}
