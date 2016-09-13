using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;

namespace CAPS.CORPACCOUNTING.Security
{
    [Table("CAPS_SecureGroupMappingUnit")]
    public  class SecureGroupMappingUnit :SecureGroup
    {

        public virtual int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual long? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        
    }
}
