using CAPS.CORPACCOUNTING.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
  public interface ITemplate
    {
        Task<FileDto> DownLoadTemplate();
    }
}
