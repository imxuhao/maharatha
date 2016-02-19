using System.Collections.Generic;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class UpdateRollupCenterUnitInput : IInputDto
    {
        /// <summary>Gets or sets the RollupCenterId field. </summary>
        [Range(0, Int32.MaxValue)]
        public int RollupCenterId { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(RollupCenterUnit.MaxCaptionLength)]
        [Required]
        public string Caption { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public long? AccountId { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public int? JobId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the RollupTypeID field. </summary>
        public RollupType RollupTypeId { get; set; }

    }
}
