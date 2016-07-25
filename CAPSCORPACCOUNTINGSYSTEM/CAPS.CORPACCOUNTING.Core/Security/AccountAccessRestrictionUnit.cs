using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security
{
    [Table("AccountAccessRestriction")]
    public class AccountAccessRestrictionUnit : FullAuditedEntity<long>, IMustHaveTenant
    {
        [Column("AccountRestrictionId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit AccountUnit { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>Gets or sets the IsActive </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
    }
}
