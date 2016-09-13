using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security.Dto
{
  
    public class BankAccountAccessListUnitDto
    {
        /// <summary>Gets or sets the BankAccountId</summary>
        public long BankAccountId { get; set; }

        /// <summary>Gets or sets the AccountName</summary>
        public string AccountName { get; set; }

        /// <summary>Gets or sets the BankAccountNumber</summary>
        public virtual string BankAccountNumber { get; set; }

        /// <summary>Gets or sets the BankName</summary>
        public string BankName { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

    }
}
