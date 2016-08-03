using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security.Dto
{
    /// <summary>
    /// 
    /// </summary>
   
    public class CreditAccessListUnitDto
    {
        /// <summary>Gets or sets the AccountingDocumentId field. </summary>
        public long AccountingDocumentId { get; set; }

        /// <summary>Gets or sets the CardHolderName</summary>
        public string CardHolderName { get; set; }

        /// <summary>Gets or sets the CardNumber</summary>
        public virtual string CardNumber { get; set; }
      
        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
