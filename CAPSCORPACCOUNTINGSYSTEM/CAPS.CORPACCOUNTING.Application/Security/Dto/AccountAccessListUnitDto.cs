using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
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

    public class AccountAccessListUnitDto
    {
     
        /// <summary>Gets or sets the AccountId</summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the Caption</summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the AccountNumber</summary>
        public string AccountNumber { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }


    }
}
