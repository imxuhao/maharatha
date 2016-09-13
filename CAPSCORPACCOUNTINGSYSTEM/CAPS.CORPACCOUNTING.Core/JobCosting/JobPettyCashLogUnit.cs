using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobWrapPettyCashLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapPettyCashLog")]   
    public class JobPettyCashLogUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 500;
        public const int MaxLineLength = 50;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobPettyCashLogId</summary>
        [Column("JobPettyCashLogId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobBudgetId field. </summary>
        [Range(0,Int32.MaxValue)]
        public virtual int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the LineInfo field. </summary>
        [StringLength(MaxLineLength)]
        public virtual string LineInfo { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string  Description { get; set; }

        /// <summary>Gets or sets the Amount field. </summary>
        [Column(TypeName = "Money")]
        public virtual decimal Amount { get; set; }

        /// <summary>Gets or sets the AccountingTransactionId field. </summary>
        public virtual string AccountingTransactionId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public JobPettyCashLogUnit()
        {
        }

        public JobPettyCashLogUnit(int jobbudgetid, long? accountid, string lineinfo, string description, decimal amount, string accountingtransactionid, long? organizationunitid)
        {
            JobBudgetId = jobbudgetid;
            AccountId = accountid;
            LineInfo = lineinfo;
            Description = description;
            Amount = amount;
            AccountingTransactionId = accountingtransactionid;
            OrganizationUnitId = organizationunitid;
        }
    }
}