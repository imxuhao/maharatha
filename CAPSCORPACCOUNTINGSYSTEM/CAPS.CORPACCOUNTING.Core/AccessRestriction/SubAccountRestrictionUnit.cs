using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Security;

namespace CAPS.CORPACCOUNTING.AccessRestriction
{
    [Table("CAPS_SubAccountAccessControl")]
    public class SubAccountRestrictionUnit :SubAccountUnit
    {

        public long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit AccountUnit { get; set; }
        public virtual int? SecureId { get; set; }

        [ForeignKey("SecureId")]
        public virtual SecureGroup SecureGroup { get; set; }

        public virtual long? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// This column will be used for both providing access or being denied
        /// </summary>
        public bool IsAccessProvided { get; set; }
    }
}
