using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Reports
{

    /// <summary>
    ///  BatchReportItem is the table name in Lajit
    /// </summary>
    [Table("CAPS_BatchReportItem")]
    public class BatchReportItemUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        private const int MaxReportNameLength = 500;

        /// <summary> Overriding the ID column with BatchReportItemId field. </summary>
        [Column("BatchReportItemId")]
        public override int Id { get; set; }


        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the BatchReportId field. </summary>
        public virtual int BatchReportId { get; set; }
        [ForeignKey("BatchReportId")]
        public virtual BatchReportUnit BatchReportUnit { get; set; }
        
        public virtual long UserReportId { get; set; }

        /// <summary>Gets or sets the ReportName field. </summary>
        [MaxLength(MaxReportNameLength)]
        public virtual string ReportName { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsSavedView field. </summary>
        public virtual bool? IsSavedView { get; set; }

        /// <summary>Gets or sets the StartDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the EndDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the CompareSDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? CompareSDate { get; set; }

        /// <summary>Gets or sets the CompareEDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? CompareEDate { get; set; }

        /// <summary>Gets or sets the ReportStyle field. </summary>
        public virtual int? ReportStyle { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
