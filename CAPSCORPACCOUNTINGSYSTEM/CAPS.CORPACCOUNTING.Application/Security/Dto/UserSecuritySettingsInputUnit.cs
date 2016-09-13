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
   public class UserSecuritySettingsInputUnit
    {

        /// <summary>Gets or sets the UserIdList field. </summary>
        public List<long> UserIdList { get; set; }
        ///<summary>Get Sets the AccountAccessList field.</summary>
        public List<AccountAccessListInputUnit> AccountAccessList { get; set; }

        ///<summary>Get Sets the BankAccountAccessList field.</summary>
        public List<BankAccountAccessListInputUnit> BankAccountAccessList { get; set; }


        ///<summary>Get Sets the CreditCardAccessList field.</summary>
        public List<CreditCardAccessListInputUnit> CreditCardAccessList { get; set; }

        ///<summary>Get Sets the ProjectAccessList field.</summary>
        public List<ProjectAccessListInputUnit> ProjectAccessList { get; set; }
    }
}
