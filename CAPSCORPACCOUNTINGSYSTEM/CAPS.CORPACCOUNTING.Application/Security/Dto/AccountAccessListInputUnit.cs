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
      public class AccountAccessListInputUnit
    {
        /// <summary>Gets or sets the AccountId field. </summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the AccountNumber field. </summary>
        public string AccountNumber { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public long UserId { get; set; }
    }
}
