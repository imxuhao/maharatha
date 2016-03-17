using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Banking
{

    /// <summary>
    /// BankRecAdjustment is the table name in lajit
    /// </summary>
    [Table("CAPS_BankRecAdjustment")]
    public class BankRecAdjustmentUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Declaration of Properties
        /// <summary>Overriding the ID column with BankRecAdjustmentId</summary>
        [Column("BankRecAdjustmentId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the BankRecControlID field.</summary>
        public virtual int BankRecControlId { get; set; }

        [ForeignKey("BankRecControlId")]
        public virtual BankRecControlUnit BankRecControl { get; set; }

        /// <summary>Gets or sets the Description field.</summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the AccountID field.</summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobID field.</summary>
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the SubAccountID field.</summary>
        public virtual long? SubAccountId { get; set; }

        [ForeignKey("SubAccountId")]
        public virtual SubAccountUnit SubAccount { get; set; }

        /// <summary>Gets or sets the AccountingDocumentID field.</summary>
        public virtual long? AccountingDocumentId { get; set; }

        [ForeignKey("AccountingDocumentId")]
        public virtual AccountingHeaderTransactionsUnit AccountingHeaderTransaction { get; set; }

        /// <summary>Gets or sets the AdjustmentAmount field.</summary>
        public virtual decimal? AdjustmentAmount { get; set; }

        /// <summary>Gets or sets the IsActive field.</summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field.</summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }
       
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion

        public BankRecAdjustmentUnit()
        {
            IsActive = true;
        }

    }
}
