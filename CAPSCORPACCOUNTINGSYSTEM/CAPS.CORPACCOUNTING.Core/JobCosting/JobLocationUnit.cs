using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_JobLocation")]
    public class JobLocationUnit : Entity
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

        /// <summary>Gets or sets the LocationSite1Date field. </summary>
        public virtual DateTime? LocationSiteDate { get; set; }
      
        #endregion

        public JobLocationUnit()
        {
        }
        public JobLocationUnit(int jobid, DateTime? locationsitedate,  int locationid)
        {
            JobId = jobid;
            LocationSiteDate = locationsitedate;
            LocationId = locationid;
        }
    }
}