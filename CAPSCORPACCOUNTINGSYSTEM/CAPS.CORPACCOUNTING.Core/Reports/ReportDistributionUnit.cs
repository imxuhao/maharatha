using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAPS.CORPACCOUNTING.Reports
{


    public enum TypeOfReportDistribution
    {
        [Display(Name = "Accounting")]
        Accounting = 1,
        [Display(Name = "Production")]
        Production = 2,
        [Display(Name = "Officers")]
        Officers = 3,
        [Display(Name = "None")]
        None = 4,
    }

    /// <summary>
    /// ReportDistribution is the table name in Lajit
    /// </summary>

    [Table("CAPS_ReportDistribution")]
   public class ReportDistributionUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {


        /// <summary> Overriding the ID column with ReportDistributionId field. </summary>
        [Column("ReportDistributionId")]
        public override int Id { get; set; }


        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfReportDistributionId field. </summary>
        public virtual TypeOfReportDistribution TypeOfReportDistributionId { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }
        [ForeignKey("EntityId")]
        public virtual EntityUnit EntityUnit { get; set; }

        /// <summary>Gets or sets the RoleId field. </summary>
        public virtual int RoleId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
