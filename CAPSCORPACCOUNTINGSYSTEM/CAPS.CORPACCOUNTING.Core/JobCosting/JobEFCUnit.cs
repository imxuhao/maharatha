using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobEFC is the table name in lajit
    /// </summary>
    [Table("CAPS_JobEFC")]
    public class JobEFCUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobEfcId</summary>
        [Column("JobEfcId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobBudgetID field. </summary>
        public virtual int JobBudgetId { get; set; } 

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the SubAccountID field. </summary>
        public virtual long? SubAccountId { get; set; }

        [ForeignKey("SubAccountId")]
        public virtual SubAccountUnit SubAccount { get; set; }

        /// <summary>Gets or sets the EFCAdjustment field. </summary>
        public virtual decimal? EfcAdjustment { get; set; }

        /// <summary>Gets or sets the ProjectControlPeriodID field. </summary>
        public virtual int? ProjectControlPeriodId { get; set; }

        [ForeignKey("ProjectControlPeriodId")]
        public virtual ProjectControlPeriodUnit ProjectControlPeriod { get; set; }

        /// <summary>Gets or sets the Comment field. </summary>
        public virtual string Comment { get; set; }

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public virtual long? SubAccountId1 { get; set; }

        [ForeignKey("SubAccountId1")]
        public virtual SubAccountUnit SubAccount1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public virtual long? SubAccountId2 { get; set; }

        [ForeignKey("SubAccountId2")]
        public virtual SubAccountUnit SubAccount2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public virtual long? SubAccountId3 { get; set; }

        [ForeignKey("SubAccountId3")]
        public virtual SubAccountUnit SubAccount3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public virtual long? SubAccountId4 { get; set; }

        [ForeignKey("SubAccountId4")]
        public virtual SubAccountUnit SubAccount4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public virtual long? SubAccountId5 { get; set; }

        [ForeignKey("SubAccountId5")]
        public virtual SubAccountUnit SubAccount5 { get; set; }

        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public virtual long? SubAccountId6 { get; set; }

        [ForeignKey("SubAccountId6")]
        public virtual SubAccountUnit SubAccount6 { get; set; }

        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public virtual long? SubAccountId7 { get; set; }

        [ForeignKey("SubAccountId7")]
        public virtual SubAccountUnit SubAccount7 { get; set; }

        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public virtual long? SubAccountId8 { get; set; }

        [ForeignKey("SubAccountId8")]
        public virtual SubAccountUnit SubAccount8 { get; set; }

        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public virtual long? SubAccountId9 { get; set; }

        [ForeignKey("SubAccountId9")]
        public virtual SubAccountUnit SubAccount9 { get; set; }

        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public virtual long? SubAccountId10 { get; set; }

        [ForeignKey("SubAccountId10")]
        public virtual SubAccountUnit SubAccount10 { get; set; }

        /// <summary>Gets or sets the BudgetAdjustment field. </summary>
        public virtual decimal? BudgetAdjustment { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion
    }
}
