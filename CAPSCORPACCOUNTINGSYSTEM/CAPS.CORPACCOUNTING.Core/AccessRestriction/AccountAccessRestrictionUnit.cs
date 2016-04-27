using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Security;

namespace CAPS.CORPACCOUNTING.AccessRestriction
{
    [Table("CAPS_AccountAccessControl")]
    public class AccountAccessRestrictionUnit :AccountUnit
    {

        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit JobUnit { get; set; }
        public virtual int? SecureId { get; set; }

        [ForeignKey("SecureId")]
        public virtual SecureGroup SecureGroup { get; set; }

        public virtual long? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// This column will be used for both providing access or being denied
        /// </summary>
        public  bool IsAccessProvided { get; set; }

      
    }
}
