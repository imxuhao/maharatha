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
        [Range(1, Int32.MaxValue)]
        public int JobId { get; set; }

        /// <summary>Gets or sets the LocationId field. </summary>
        [Range(1, Int32.MaxValue)]
        public virtual int LocationId { get; set; }
       

        /// <summary>Gets or sets the LocationSiteDate field. </summary>
        public DateTime? LocationSiteDate { get; set; }

       
    }
}
