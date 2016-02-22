using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_JobLocation")]
    public class JobLocationUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobLocationId</summary>
        [Column("JobLocationId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the LocationId field. </summary>
        public virtual int LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual LocationSetUnit Location { get; set; }

        /// <summary>Gets or sets the LocationSite1Date field. </summary>
        public virtual DateTime? LocationSiteDate { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public JobLocationUnit()
        {
        }
        public JobLocationUnit(int jobid, DateTime? locationsitedate, long? organizationunitid,int locationid)
        {
            JobId = jobid;
            LocationSiteDate = locationsitedate;
            OrganizationUnitId = organizationunitid;
            LocationId = locationid;
        }
    }
}