using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Banking
{
    /// <summary>
    /// Enum for TypeOfBatch
    /// </summary>
    public enum TypeOfBatch
    {
        [Display(Name = "General Journal")]
        GeneralJournal = 1,
        [Display(Name = "Cash Journal")]
        CashJournal = 2,
        [Display(Name = "Accounts Payable")]
        AccountsPayable = 3,
        [Display(Name = "Accounts Receivable")]
        AccountsReceivable = 4,
        [Display(Name = "Payroll")]
        Payroll = 5,
        [Display(Name = "Petty Cash")]
        PettyCash = 6,
        [Display(Name = "Petty Cash Reimbursement")]
        PettyCashReimbursement = 7,
        [Display(Name = "A/R Cash")]
        ARCash = 8,
        [Display(Name = "Petty Cash Return")]
        PettyCashReturn = 9,
        [Display(Name = "Recurring Journal")]
        RecurringJournal = 10,
        [Display(Name = "Reversing Journal")]
        ReversingJournal = 11,
        [Display(Name = "ICT Batch")]
        ICTBatch = 12,
        [Display(Name = "Revaluation")]
        Revaluation = 13,
        [Display(Name = "A/P Check Cycle")]
        APCheckCycle = 14,

    }

    /// <summary>
    /// Batch is the table name in lajit
    /// </summary>
    [Table("CAPS_Batch")]
    public class BatchUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Maximum length of the  Description property.</summary>
        public const int MaxLength = 100;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with BatchId</summary>
        [Column("BatchId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TypeOfBatchId field. </summary>
        [EnumDataType(typeof(TypeOfBatch))]
        public virtual TypeOfBatch TypeOfBatchId { get; set; }

        /// <summary>Gets or sets the DefaultTransactionDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DefaultTransactionDate { get; set; }

        /// <summary>Gets or sets the DefaultCheckDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DefaultCheckDate { get; set; }

        /// <summary>Gets or sets the PostingDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PostingDate { get; set; }

        /// <summary>Gets or sets the ControlTotal field. </summary>
        public virtual decimal? ControlTotal { get; set; }

        /// <summary>Gets or sets the RecurMonthIncrement field. </summary>
        public virtual int? RecurMonthIncrement { get; set; }

        /// <summary>Gets or sets the IsRetained field. </summary>
        public virtual bool IsRetained { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsBatchFinalized field. </summary>
        public virtual bool? IsBatchFinalized { get; set; }

        /// <summary>Gets or sets the IsUniversal field. </summary>
        public virtual bool? IsUniversal { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion
        public BatchUnit() {
            IsRetained = false;
            IsDefault = false;
            IsActive = true;
        }
        public BatchUnit(string description, TypeOfBatch typeofbatchid, DateTime? defaulttransactiondate, DateTime? defaultcheckdate, DateTime? postingdate,
            decimal? controltotal, int? recurmonthincrement, bool isretained, bool isdefault, bool isactive, TypeOfInactiveStatus? typeofinactivestatusid,
            bool? isbatchfinalized, bool? isuniversal, long? organizationunitid)
        {
            Description = description;
            typeofbatchid = TypeOfBatchId;
            DefaultTransactionDate = defaulttransactiondate;
            PostingDate = postingdate;
            ControlTotal = controltotal;
            RecurMonthIncrement = recurmonthincrement;
            IsRetained = isretained;
            IsDefault = isdefault;
            IsActive = isactive;
            TypeOfInactiveStatusId = typeofinactivestatusid;
            IsBatchFinalized = isbatchfinalized;
            IsUniversal = isuniversal;
            OrganizationUnitId = organizationunitid;
        }
    }
}
