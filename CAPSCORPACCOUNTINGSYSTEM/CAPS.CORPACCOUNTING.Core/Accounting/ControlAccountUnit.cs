using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// Enum for TypeOfControlAccount
    /// </summary>
    public enum TypeOfControlAccount
    {
        [Display(Name = "A/P Clearing")]
        APClearing = 1,
        [Display(Name = "A/P Suspense")]
        APSuspense = 2,
        [Display(Name = "A/P Discount")]
        APDiscount = 3,
        [Display(Name = "A/R Clearing")]
        ARClearing = 4,
        [Display(Name = "A/R Suspense")]
        ARSuspense = 5,
        [Display(Name = "A/R Discount")]
        ARDiscount = 6,
        [Display(Name = "A/R Write Off")]
        ARWriteOff = 7,
        [Display(Name = "A/R Prepaid Job")]
        ARPrepaidJob = 8,
        [Display(Name = "A/R Prepaid Non-Job")]
        ARPrepaidNonJob = 9,
        [Display(Name = "Payroll Clearing")]
        PayrollClearing = 10

    }
    /// <summary>
    /// ControlAccount is the table name in Lajit
    /// </summary>
    [Table("CAPS_ControlAccount")]
    public class ControlAccountUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with ControlAccountId</summary>
        [Column("ControlAccountId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfControlAccountID field. </summary>
        public TypeOfControlAccount TypeOfControlAccountId { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public int? JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public int EntityId { get; set; }

        [ForeignKey("EntityId")]
        public EntityUnit Entity { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion
        public ControlAccountUnit()
        {
            IsActive = true;
            IsApproved = false;
        }
    }
}
