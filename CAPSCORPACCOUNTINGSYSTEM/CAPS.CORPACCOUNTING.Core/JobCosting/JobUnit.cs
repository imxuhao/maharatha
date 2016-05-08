using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.JobCosting
{

    /// <summary>
    /// Enum for TypeofProject
    /// </summary>
    public enum TypeofProject
    {
        [Display(Name = "Division")]
        Division = 1,
        [Display(Name = "Commercial")]
        Commercial = 2,
        [Display(Name = "Features")]
        Features = 3
    }

    /// <summary>
    /// Enum for TaxRecovery
    /// </summary>
    public enum TaxRecovery
    {
        [Display(Name = "OK")]
        Ok = 1,
        [Display(Name = "NO")]
        No = 2,
       
    }
    /// <summary> /// Enum for BudgetSoftware /// </summary>
    public enum BudgetSoftware
    {
        [Display(Name = "PointZero Version 2000")]
        PointZeroVersion2000 = 1,
        [Display(Name = "PointZero Mac Version 3.3")]
        PointZeroMacVersion3 = 2,
        [Display(Name = "PointZero Version 4")]
        PointZeroVersion4 = 3,
        [Display(Name = "PointZero Music Video")]
        PointZeroMusicVideo = 4,
        [Display(Name = "ShowBiz Budgeting")]
        ShowBizBudgeting = 5,
        [Display(Name = "Hot Budget Version 1")]
        HotBudgetVersion1 = 6,
        [Display(Name = "Hot Budget Version 1.1")]
        HotBudgetVersion2 = 7,
        [Display(Name = "Hot Budget Version 1.2")]
        HotBudgetVersion3 = 8,
        [Display(Name = "Hot Budget Version 1.5")]
        HotBudgetVersion4 = 9,
        [Display(Name = "Custom Excel")]
        CustomExcel = 10,
        [Display(Name = "AICE Budgeting")]
        AICEBudgeting = 11,
        [Display(Name = "VFX Budget")]
        VFXBudget = 12,
        [Display(Name = "EP/Movie Magic XML embedded hyphen")]
        Ep = 13,
        [Display(Name = "EP/Movie Magic XML 2/2 implied hyphen")]
        Ep2 = 14,
        [Display(Name = "EP/Movie Magic XML 3/2 implied hyphen")]
        Ep3 = 15,
        [Display(Name = "EP/Movie Magic XML 4/2 implied hyphen")]
        Ep4 = 16,
        [Display(Name = "Free From Excel")]
        FreeFromExcel = 17,
        [Display(Name = "Corp Budget1")]
        CorpBudget1 = 18

    }
    /// <summary> /// Enum for ProjectStatus /// </summary>
    public enum ProjectStatus
    {
        [Display(Name = "Awarded")]
        Awarded = 1,
        [Display(Name = "Production")]
        Production = 2,
        [Display(Name = "Wrap")]
        Wrap = 3,
        [Display(Name = "Locked")]
        WrLockedap = 4,
       [Display(Name = "Closed")]
        Closed = 5,
        [Display(Name = "Start Up")]
        StartUp = 6,
        [Display(Name = "Cancelled")]
        Cancelled = 7
    }

    /// <summary>
    /// Job is the table name in lajit
    /// </summary>
    [Table("CAPS_Job")]
    public class JobUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxJobNumberLength = 50;
        public const int MaxCaptionLength = 200;

        #region Class Property Declarations

        /// <summary>Overrides Id with JobId field. </summary>
        [Column("JobId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobNumber field. </summary>
        [Required]
        [StringLength(MaxJobNumberLength)]
        public virtual string JobNumber { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        [Required]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the RollupCenterId field. </summary>
        public virtual int? RollupCenterId { get; set; }

        [ForeignKey("RollupCenterId")]
        public RollupCenterUnit RollupCenter { get; set; }

        /// <summary>Gets or sets the IsCorporateDefault field. </summary>
        public virtual bool IsCorporateDefault { get; set; }

        /// <summary>Gets or sets the ChartOfAccountId field. </summary>
        public virtual int? ChartOfAccountId { get; set; }

        [ForeignKey("ChartOfAccountId")]
        public CoaUnit ChartofAccontUnit { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public virtual long? RollupAccountId { get; set; }

        [ForeignKey("RollupAccountId")]
        public virtual AccountUnit RollupAccount { get; set; }

        /// <summary>Gets or sets the TypeOfCurrencyId field. </summary>
        public virtual int? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public virtual int? RollupJobId { get; set; }

        [ForeignKey("RollupJobId")]
        public virtual JobUnit RollupJob { get; set; }

        /// <summary>Gets or sets the TypeOfJobStatusId field. </summary>
        public virtual ProjectStatus? TypeOfJobStatusId { get; set; }

        /// <summary>Gets or sets the TypeOfBidSoftwareId field. </summary>
        public virtual BudgetSoftware? TypeOfBidSoftwareId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsICTDivision field. </summary>
        public virtual bool IsICTDivision { get; set; }

        /// <summary>Gets or sets the TypeofProject field. </summary>
        public virtual TypeofProject? TypeofProjectId { get; set; }

        /// <summary>Gets or sets the TaxRecovery field. </summary>
        public virtual int? TaxRecoveryId { get; set; }        


        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set;}

        /// <summary>Gets or sets the IsDivision field. </summary>
        public virtual bool IsDivision { get; set; }


       // public virtual List<JobCommercialUnit> JobDetails { get; set; }

        //public List<JobLocationUnit> joblocations { get; set; }

        #endregion

        public JobUnit()
        {
        }

        public JobUnit(string jobnumber, string caption, bool iscorporatedefault, int? chartofaccountid,
            long? rollupaccountid, int? typeofcurrencyid, int? rollupjobid, ProjectStatus? typeofjobstatusid,
            BudgetSoftware? typeofbidsoftwareid, int? rollupcenterid, bool isapproved, bool isactive, bool isictdivision,
            long? organizationunitid, TypeofProject? typeofprojectid, int? taxrecoveryid,bool isdivision)
        {
            JobNumber = jobnumber;
            Caption = caption;
            IsCorporateDefault = iscorporatedefault;
            RollupAccountId = rollupaccountid;
            TypeOfCurrencyId = typeofcurrencyid;
            TypeOfJobStatusId = typeofjobstatusid;
            TypeOfBidSoftwareId = typeofbidsoftwareid;
            IsApproved = isapproved;
            IsActive = isactive;
            IsICTDivision = isictdivision;
            OrganizationUnitId = organizationunitid;
            RollupCenterId = rollupcenterid;
            TypeofProjectId = typeofprojectid;
            TaxRecoveryId = taxrecoveryid;
            ChartOfAccountId = chartofaccountid;
            IsDivision = isdivision;
        }
    }
}
