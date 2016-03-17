using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.Reports
{

    public enum TypeOfDistribution
    {
        [Display(Name = "My Reports")]
        MyReports=1,
        [Display(Name = "Email")]
        Email = 2,
        [Display(Name = "My Reports + Email")]
        MyReportsEmail = 3
    }

    /// <summary>
    /// ReportDistributionList is the table name in Lajit
    /// </summary>

    [Table("CAPS_ReportDistributionList")]
  public class ReportDistributionListUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with ReportDistributionListId field. </summary>
        [Column("ReportDistributionListId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the ReportDistributionId field. </summary>
        public virtual int? ReportDistributionId { get; set; }
        [ForeignKey("ReportDistributionId")]
        public virtual ReportDistributionUnit ReportDistributionUnit { get; set; }

        /// <summary>Gets or sets the TypeOfDistributionId field. </summary>
        public virtual TypeOfDistribution? TypeOfDistributionId { get; set; }

        /// <summary>Gets or sets the RoleId field. </summary>
        public virtual int? RoleId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual int? UserId { get; set; }

        /// <summary>Gets or sets the IsNotified field. </summary>
        public virtual bool? IsNotified { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
