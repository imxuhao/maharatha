using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>/// Enum for TypeofBudget/// </summary>
    public enum TypeofBudget
    {
        [Display(Name = "Standard Budget")]
        StandardBudget = 1,
        [Display(Name = "Initial Budget")]
        InitialBudget = 2,
        [Display(Name = "Revised Budget")]
        RevisedBudget = 3,
        [Display(Name = "Final Budget")]
        FinalBudget = 4,
        [Display(Name = "Producer's Actuals")]
        ProducerActuals = 5,
        [Display(Name = "Master Budget")]
        MasterBudget = 6,
        [Display(Name = "Location Budget")]
        LocationBudget = 7,
        [Display(Name = "Department Budget")]
        DepartmentBudget = 8,
        [Display(Name = "Crew Budget")]
        CrewBudget = 9,
        [Display(Name = "Alternate Budget")]
        AlternateBudget = 10,
        [Display(Name = "Pattern Budget")]
        PatternBudget = 11,
        [Display(Name = "Episode Budget")]
        EpisodeBudget = 12,
        [Display(Name = "Amortized Budget")]
        AmortizedBudget = 13,
        [Display(Name = "Pilot Budget")]
        PilotBudget = 14,
        [Display(Name = "Corporate Budget")]
        CorporateBudget = 15,
        [Display(Name = "Corporate Projection ")]
        CorporateProjection = 16
    }

    /// <summary>
    /// JobBudget is the table name in lajit
    /// </summary>
    [Table("CAPS_JobBudget")]
    public class JobBudgetUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 500;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobBudgetId</summary>
        [Column("JobBudgetId")]
        public override int  Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TypeofBudgetId field. </summary>
        [EnumDataType(typeof(TypeofBudget))]
        public virtual TypeofBudget TypeofBudgetId { get; set; }

        /// <summary>Gets or sets the TypeofBudgetSoftwareId field. </summary>
        [EnumDataType(typeof(BudgetSoftware))]
        public virtual BudgetSoftware TypeofBudgetSoftwareId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion

        public JobBudgetUnit() { }

        public JobBudgetUnit(int? jobid, string description, TypeofBudget typeofbudgetid, BudgetSoftware typeofbudgetsoftwareid, long? organizationunitid)
        {
            JobId = jobid;
            Description = description;
            TypeofBudgetId = typeofbudgetid;
            TypeofBudgetSoftwareId = typeofbudgetsoftwareid;
            OrganizationUnitId = organizationunitid;
        }
    }
}