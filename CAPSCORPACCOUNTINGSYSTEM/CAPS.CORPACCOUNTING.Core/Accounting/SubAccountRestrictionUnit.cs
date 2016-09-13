using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// This is the new table to restricting accounts for subaccounts
    /// </summary>

    [Table("CAPS_SubAccountRestriction")]
    public class SubAccountRestrictionUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>Overriding the Id column with SubAccountRestrictionId </summary>
        [Column("SubAccountRestrictionId")]
        public override long Id { get; set; }


        /// <summary>Gets or sets the AccountId</summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the SubAccountId </summary>

        public virtual long SubAccountId { get; set; }

        [ForeignKey("SubAccountId")]
        public SubAccountUnit SubAccount { get; set; }

        /// <summary>Gets or sets the IsActive </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId </summary>
        public virtual int TenantId { get; set; }
       
    }
}
