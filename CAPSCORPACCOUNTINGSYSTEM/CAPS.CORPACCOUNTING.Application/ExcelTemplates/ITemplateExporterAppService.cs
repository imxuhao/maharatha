using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Dto;
using CAPS.CORPACCOUNTING.ExcelTemplates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
    /// <summary>
    /// 
    /// </summary>
   public interface ITemplateExporterAppService : IApplicationService
    {
        Task<FileDto> GetTemplateByEntity(TemplateInputUnit input);
    }
}
