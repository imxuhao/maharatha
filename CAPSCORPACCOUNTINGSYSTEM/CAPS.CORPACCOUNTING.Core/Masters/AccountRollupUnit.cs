using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// AccountRollup is the table name in lajit
    /// </summary>
    [Table("CAPS_AccountRollup")]
    public class AccountRollupUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {     

        #region Class Property Declarations

        /// <summary>Overriding the ID column with AccountRollupId</summary>
        [Column("AccountRollupId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the ChartOfAccountRollupID field. </summary>
        public virtual int ChartOfAccountRollupId { get; set; }

        [ForeignKey("ChartOfAccountRollupId")]
        public ChartOfAccountRollupUnit ChartOfAccountRollupUnit { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the LinkAccountID field. </summary>
        public virtual int? LinkAccountId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public AccountRollupUnit()
        {
            IsActive = true;
            IsApproved = false;
        }
    }
}
