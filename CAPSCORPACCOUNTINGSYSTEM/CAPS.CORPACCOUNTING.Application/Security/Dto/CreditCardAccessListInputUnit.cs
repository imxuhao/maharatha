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
  
    public  class CreditCardAccessListInputUnit
    {

        /// <summary>Gets or sets the AccountingDocumentId field. </summary>
        public long AccountingDocumentId { get; set; }

        /// <summary>Gets or sets the CardHolderName field. </summary>
        public string CardHolderName { get; set; }

        /// <summary>Gets or sets the CardNumber field. </summary>
        public string CardNumber { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public long UserId { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
