using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Security;
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
   
    public class BankAccountAccessListInputUnit
    {

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public long BankAccountId { get; set; }

        /// <summary>Gets or sets the BankAccountNumber field. </summary>
        public string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the AccountName field. </summary>
        public string AccountName { get; set; }


        /// <summary>Gets or sets the BankName field. </summary>
        public string BankName { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public long UserId { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId field. </summary>
        public long? OrganizationUnitId { get; set; }

    }
}
