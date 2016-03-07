using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
namespace CAPS.CORPACCOUNTING.JobCosting
{

    /// <summary>
    /// JobBudgetDetail is the table name in lajit
    /// </summary>
    [Table("CAPS_JobBudgetDetails")]
    public class JobBudgetDetailsUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Declaration of Properties
        /// <summary>Overriding the Id column with JobBudgetDetailId</summary>
        [Column("JobBudgetDetailId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the JobBudgetId field. </summary>
        [Required]
        [Range(0, Int32.MaxValue)]
        public virtual int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        [Required]
        [Range(0, Int64.MaxValue)]
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the SubAccountId field. </summary>
        [Range(0, Int64.MaxValue)]
        public virtual long? SubAccountId { get; set; }

        [ForeignKey("SubAccountId")]
        public AccountUnit SubAccount { get; set; }

        /// <summary>Gets or sets the LocationId field. </summary>
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationSetUnit Location { get; set; }

        /// <summary>Gets or sets the LocationId field. </summary>
        public int? SetId { get; set; }
        [ForeignKey("SetId")]
        public LocationSetUnit LocationSet { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        [Column(TypeName = "Money")]
        public virtual decimal? Amount { get; set; }

        /// <summary>Gets or sets the IsFringeLine field. </summary>
        public virtual bool? IsFringeLine { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public JobBudgetDetailsUnit()
        { }

        public JobBudgetDetailsUnit(int jobbudgetid, long accountid, long? subaccountid, int? locationid, int? setid, decimal? amount, bool? isfringeline, long? organizationunitid)
        {
            JobBudgetId = jobbudgetid;
            AccountId = accountid;
            SubAccountId = subaccountid;
            LocationId = locationid;
            SetId = setid;
            Amount = amount;
            IsFringeLine = isfringeline;
            OrganizationUnitId = organizationunitid;


        }
    }
}
