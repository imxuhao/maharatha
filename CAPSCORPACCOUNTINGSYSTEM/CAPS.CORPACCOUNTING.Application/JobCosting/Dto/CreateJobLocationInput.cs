using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class CreateJobLocationInput : IInputDto
    {      

        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }
        
        /// <summary>Gets or sets the LocationId field. </summary>
        public virtual int LocationId { get; set; }


        /// <summary>Gets or sets the LocationSite1Date field. </summary>
        public DateTime? LocationSiteDate { get; set; } = DateTime.Now;

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
