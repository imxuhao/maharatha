using CAPS.CORPACCOUNTING.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.ExcelTemplates
{
  public interface ITemplate
    {
        /// <summary>
        /// Download Template
        /// </summary>
        /// <returns></returns>
        Task<FileDto> DownLoadTemplate(int coaId);
    }
}
