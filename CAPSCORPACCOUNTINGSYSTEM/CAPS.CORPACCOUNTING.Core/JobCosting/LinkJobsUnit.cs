using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobLink is the table name in lajit
    /// </summary>
    [Table("CAPS_JobLink")]
    public class LinkJobsUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobLinkId</summary>
        [Column("JobLinkId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int JobId { get; set; }
        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the LinkJobId field. </summary>
        public virtual int LinkJobId { get; set; }

        [ForeignKey("LinkJobId")]
        public JobUnit LinkJob { get; set; }

        /// <summary>Gets or sets the LinkCompanyId field. </summary>
        public virtual long LinkCompanyId { get; set; }

        [ForeignKey("LinkCompanyId")]
        public OrganizationUnit Organization { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }


        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public LinkJobsUnit()
        {
        }
        public LinkJobsUnit(int jobid, int linkjobid, int linkcompanyid, bool isapproved, bool isactive)
        {
            JobId = jobid;
            LinkJobId = linkjobid;
            LinkCompanyId = linkcompanyid;
            IsApproved = isapproved;
            IsActive = isactive;
        }
    }
}