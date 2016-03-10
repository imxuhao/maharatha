using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Reports
{

    public enum TypeOfReport
    {
        [Display(Name = "Entry")]
        Entry = 1,
        [Display(Name = "Edit List")]
        EditList = 2,
        [Display(Name = "Posting")]
        Posting = 3,
        [Display(Name = "Formal")]
        Formal = 4,
        [Display(Name = "Other")]
        Other = 5,
    }



    public enum TypeOfReportCategory
    {
        [Display(Name = "Ledger")]
        Ledger = 1,
        [Display(Name = "Corporate Ledger")]
        CorporateLedger = 2,
        [Display(Name = "Project Ledger")]
        ProjectLedger = 3,
        [Display(Name = "Payables")]
        Payables = 4,
        [Display(Name = "Receivables")]
        Receivables = 5,
        [Display(Name = "Petty Cash")]
        PettyCash = 6,
        [Display(Name = "Purchasing")]
        Purchasing = 7,
        [Display(Name = "Banking")]
        Banking = 8,
        [Display(Name = "Credit Card")]
        CreditCard = 9,
        [Display(Name = "Shipping")]
        Shipping = 10,
        [Display(Name = "Payroll")]
        Payroll = 11,
        [Display(Name = "Per Diem")]
        PerDiem = 12,
        [Display(Name = "Miscellaneous")]
        Miscellaneous = 13,
    }


    /// <summary>
    /// Report is the table name in Lajit
    /// </summary>

    [Table("CAPS_Report")]
    public class ReportUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxPageInfoLength = 100;
        public const int MaxReportIdentificationLength = 100;
        public const int MaxReportTitleLength = 400;
        public const int MaxReportOrderByLength = 100;
        public const int MaxReportReportSortLength = 100;


        /// <summary> Overriding the ID column with ReportId field. </summary>
        [Column("ReportId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the PageInfo field. </summary>
        [StringLength(MaxPageInfoLength)]
        public virtual string PageInfo { get; set; }

        /// <summary>Gets or sets the FormId field. </summary>
        public virtual int? FormId { get; set; }

        /// <summary>Gets or sets the ReportIdentification field. </summary>
        [Required]
        [StringLength(MaxReportIdentificationLength)]
        public virtual string ReportIdentification { get; set; }

        /// <summary>Gets or sets the ReportTitle field. </summary>
        [Required]
        [StringLength(MaxPageInfoLength)]
        public virtual string ReportTitle { get; set; }

        /// <summary>Gets or sets the TypeOfReportId field. </summary>
        public virtual TypeOfReport TypeOfReportId { get; set; }

        /// <summary>Gets or sets the TypeOfReportCategoryId field. </summary>
        public virtual TypeOfReportCategory? TypeOfReportCategoryId { get; set; }

        /// <summary>Gets or sets the ReportDistributionId field. </summary>
        public virtual int? ReportDistributionId { get; set; }

        /// <summary>Gets or sets the ReportParameters field. </summary>
        [Column(TypeName = "xml")]
        public virtual string ReportParameters { get; set; }

        /// <summary>Gets or sets the ReportOrderBy field. </summary>
        [StringLength(MaxReportOrderByLength)]
        public virtual string ReportOrderBy { get; set; }

        /// <summary>Gets or sets the ReportSort field. </summary>
        [StringLength(MaxReportReportSortLength)]
        public virtual string ReportSort { get; set; }

        /// <summary>Gets or sets the BpeDocument field. </summary>
        [Column(TypeName = "xml")]
        public virtual string BpeDocument { get; set; }

        /// <summary>Gets or sets the ReportSize field. </summary>
        public virtual long? ReportSize { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }

        [ForeignKey("EntityId")]
        public virtual EntityUnit EntityUnit { get; set; }

        /// <summary>Gets or sets the RoleId field. </summary>
        public virtual int RoleId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
