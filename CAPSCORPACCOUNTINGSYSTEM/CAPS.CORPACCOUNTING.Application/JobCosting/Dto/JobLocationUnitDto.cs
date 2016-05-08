using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapFrom(typeof(JobLocationUnit))]
    public class JobLocationUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the JobLocationId field. </summary>
        public int JobLocationId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }
        
        /// <summary>Gets or sets the LocationId field. </summary>
        public int LocationId { get; set; }
       
        /// <summary>Gets or sets the LocationSiteDate field. </summary>
        public DateTime? LocationSiteDate { get; set; }

        /// <summary>Gets or sets the LocationName field. </summary>
        public string  LocationName { get; set; }
       
    }
}
