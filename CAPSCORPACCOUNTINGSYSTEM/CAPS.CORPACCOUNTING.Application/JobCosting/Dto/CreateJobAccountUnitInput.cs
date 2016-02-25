using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class CreateJobAccountUnitInput : IInputDto
    {
        [Range(0, Int32.MaxValue)]
        public int JobId { get; set; }     

        /// <summary>Gets or sets the AccountId field. </summary>
        [Range(1, Int32.MaxValue)]
        public long AccountId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(JobAccountUnit.MaxDescriptionLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public int? RollupJobId { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public long? RollupAccountId { get; set; }        

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
