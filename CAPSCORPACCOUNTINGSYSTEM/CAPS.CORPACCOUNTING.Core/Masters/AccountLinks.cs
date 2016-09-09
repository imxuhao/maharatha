using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_AccountLinks")]
    public  class AccountLinks: FullAuditedEntity<long>, IMustHaveTenant
    {
        [Column("AccountLinkID")]
        public override long Id { get; set; }

        [ForeignKey("HomeAccountId")]
        public AccountUnit HomeAccount { get; set; }

        public long? HomeAccountId { get; set; }

        [ForeignKey("MapAccountId")]
        public AccountUnit MappingAccount { get; set; }

        public long? MapAccountId { get; set; }

        public int TenantId { get; set; }
    }
}
