using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Banking
{
    /// <summary>
    /// BankStatementDetails is the table name in lajit
    /// </summary>
    [Table("CAPS_BankStatementDetails")]
    public class BankStatementDetailUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the Id column with BankStatementDetailId</summary>
        [Column("BankStatementDetailId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public virtual long BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the BankRecControlId field. </summary>
        [Range(1,Int32.MaxValue)]
        public virtual int BankRecControlId { get; set; }

        [ForeignKey("BankRecControlId")]
        public BankRecControlUnit BankRecControl { get; set; }

        /// <summary>Gets or sets the ClosingPeriod field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? ClosingPeriod { get; set; }

        /// <summary>Gets or sets the StartingBalance field. </summary>
        public virtual decimal? StartingBalance { get; set; }

        /// <summary>Gets or sets the Endingbalance field. </summary>
        public virtual decimal? Endingbalance { get; set; }

        /// <summary>Gets or sets the OpenBalance field. </summary>
        public virtual decimal? OpenBalance { get; set; }

        /// <summary>Gets or sets the ClearedChecks field. </summary>
        public virtual decimal? ClearedChecks { get; set; }

        /// <summary>Gets or sets the ClearedDeposits field. </summary>
        public virtual decimal? ClearedDeposits { get; set; }

        /// <summary>Gets or sets the ClearedAdjs field. </summary>
        public virtual decimal? ClearedAdjs { get; set; }

        /// <summary>Gets or sets the LedgerBalance field. </summary>
        public virtual decimal? LedgerBalance { get; set; }

        /// <summary>Gets or sets the GLEndBal field. </summary>
        public virtual decimal? GLEndBal { get; set; }

        /// <summary>Gets or sets the BankCharges field. </summary>
        public virtual decimal? BankCharges { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion

        /// <summary>Default constructor  </summary>
        public BankStatementDetailUnit() { }

        /// <summary>Parameterized constructor to initialize the properties  </summary>
        public BankStatementDetailUnit(long bankaccountid, int bankreccontrolid, DateTime? closingperiod, decimal? startingbalance, decimal? endingbalance, decimal? openbalance,
            decimal? clearedchecks, decimal? cleareddeposits, decimal? clearedadjs, decimal? ledgerbalance, decimal? glendbal, decimal? bankcharges, long? organizationunitid)
        {
            BankAccountId = bankaccountid;
            BankRecControlId = bankreccontrolid;
            StartingBalance = startingbalance;
            Endingbalance = endingbalance;
            OpenBalance = openbalance;
            ClearedChecks = clearedchecks;
            ClearedDeposits = cleareddeposits;
            ClearedAdjs = clearedadjs;
            LedgerBalance = ledgerbalance;
            GLEndBal = glendbal;
            BankCharges = bankcharges;
            OrganizationUnitId = organizationunitid;
        }
    }
}
