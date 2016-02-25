using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Banking
{
    [Table("CAPS_BankRecControl")]
    public class BankRecControlUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the Id column with BankRecControlId</summary>
        [Column("BankRecControlId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the BankAccountId</summary>
        [Range(0, Int32.MaxValue)]
        public virtual int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the ClosingPeriod field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? ClosingPeriod { get; set; }

        /// <summary>Gets or sets the StartingBalance field. </summary>
        public virtual decimal? StartingBalance { get; set; }

        /// <summary>Gets or sets the Endingbalance field. </summary>
        public virtual decimal? Endingbalance { get; set; }

        /// <summary>Gets or sets the DateReconciled field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateReconciled { get; set; }

        /// <summary>Gets or sets the ReconciledByUserId field. </summary>
        public virtual int? ReconciledByUserId { get; set; }

        /// <summary>Gets or sets the IsReconciled field. </summary>
        public virtual bool IsReconciled { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion

        /// <summary>Default constructor  </summary>
        public BankRecControlUnit() { }

        /// <summary>Parameterized constructor to initialize the properties  </summary>
        public BankRecControlUnit(int bankaccountid, int? jobid, int? accountid, DateTime? closingperiod, decimal? startingbalance, decimal? endingbalance, DateTime? datereconciled,
            int? reconciledbyuserid, bool isreconciled, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid, long? organizationunitid)
        {
            BankAccountId = bankaccountid;
            JobId = jobid;
            AccountId = accountid;
            ClosingPeriod = closingperiod;
            StartingBalance = startingbalance;
            Endingbalance = endingbalance;
            DateReconciled = datereconciled;
            ReconciledByUserId = reconciledbyuserid;
            IsReconciled = isreconciled;
            IsActive = isactive;
            IsApproved = isapproved;
            TypeOfInactiveStatusId = typeofinactivestatusid;
            OrganizationUnitId = organizationunitid;

        }
    }
}
