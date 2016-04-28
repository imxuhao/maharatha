using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Security;

namespace CAPS.CORPACCOUNTING.Security
{
    [Table("CAPS_SubEntityRestrictionUnits")]
    public class SubEntityAccessRestrictionUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        [Column("AccessCOntrolID")]
        public override long Id { get; set; }

        public virtual int? CoaId { get; set; }

        [ForeignKey("CoaId")]
        public virtual CoaUnit CoaUnit { get; set; }
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit JobUnit { get; set; }

        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit AccountUnit { get; set; }

        public long? SubAccountId { get; set; }

        [ForeignKey("SubAccountId")]
        public virtual SubAccountUnit SubAccountUnit { get; set; }

        //This covers the access for Bank, and Credit Card in a single column
        public virtual long? BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }


        public virtual long? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual int? SecureId { get; set; }

        [ForeignKey("SecureId")]
        public virtual SecureGroup SecureGroup { get; set; }
        /// <summary>
        /// This column will be used for both providing access or being denied
        /// </summary>
        public bool IsAccessProvided { get; set; }

        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
    }

}
