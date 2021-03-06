﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class CreateJobLocationInput : IInputDto
    {
        /// <summary>Gets or sets the JobId field. </summary>
        [Range(1,Int32.MaxValue)]
        public int JobId { get; set; }

        /// <summary>Gets or sets the LocationId field. </summary>
        [Range(1, Int32.MaxValue)]
        public int LocationId { get; set; }

       /// <summary>Gets or sets the LocationSite1Date field. </summary>
        public DateTime? LocationSiteDate { get; set; } = DateTime.Now;
      
    }
}
