using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class UpdateJobLocationInput : IInputDto
    {
        /// <summary>Gets or sets the JobLocationId field. </summary>
        public int JobLocationId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the LocationId field. </summary>
        public virtual int LocationId { get; set; }

        /// <summary>Gets or sets the LocationSite1Date field. </summary>
        public DateTime? LocationSiteDate { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
