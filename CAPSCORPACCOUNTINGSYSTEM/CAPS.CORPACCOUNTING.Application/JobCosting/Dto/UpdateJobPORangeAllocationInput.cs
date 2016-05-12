using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapTo(typeof(JobPORangeAllocationUnit))]
    public class UpdateJobPORangeAllocationInput : IInputDto
    {

        /// <summary>Gets or sets the  PORangeAllocationId</summary>
        public int PORangeAllocationId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the poRangeStartNumber field. </summary>
        [Required]
        //[MaxLength(JobPORangeAllocationUnit.MaxPoRangeStartNumberLength)]
        public long PoRangeStartNumber { get; set; }

        /// <summary>Gets or sets the poRangeEndNumber field. </summary>
        [Required]
        //[MaxLength(JobPORangeAllocationUnit.MaxPoRangeEndNumberLength)]
        public long PoRangeEndNumber { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long OrganizationUnitId { get; set; }
    }
}
