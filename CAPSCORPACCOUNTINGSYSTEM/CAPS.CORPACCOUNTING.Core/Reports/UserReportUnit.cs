using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Reports
{
    /// <summary>
    /// UserReport is the table name in Lajit
    /// </summary>

    [Table("CAPS_UserReport")]
    public class UserReportUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxCaptionLength = 100;

        /// <summary> Overriding the ID column with UserReportId field. </summary>
        [Column("UserReportId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the ReportParameters field. </summary>
        public virtual string ReportParameters { get; set; }

        /// <summary>Gets or sets the SentFromUserId field. </summary>
        public virtual int? SentFromUserId { get; set; }

        /// <summary>Gets or sets the SentUserReportId field. </summary>
        public virtual int? SentUserReportId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

        /// <summary>Gets or sets the IsShared field. </summary>
        public virtual bool? IsShared { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInActiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
