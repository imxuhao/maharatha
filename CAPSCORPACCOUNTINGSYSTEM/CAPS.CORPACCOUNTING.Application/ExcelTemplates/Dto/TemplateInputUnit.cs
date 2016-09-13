using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.ExcelTemplates.Dto
{
  public class TemplateInputUnit
    {

        /// <summary>Gets or sets the entityName field. </summary>  
        public string EntityName { get; set; }

        /// <summary>Gets or sets the CoaId field. 
        /// this parameter is required for downloading Accounts and Line Templates
        /// </summary>  
        public int? CoaId { get; set; }
    }
}
