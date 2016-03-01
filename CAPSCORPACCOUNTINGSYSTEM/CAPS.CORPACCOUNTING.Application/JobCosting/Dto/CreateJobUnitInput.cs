using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    public class CreateJobUnitInput : IInputDto
    {
        /// <summary>Gets or sets the JobNumber field. </summary>
        [Required]
        [StringLength(JobUnit.MaxJobNumberLength)]
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(JobUnit.MaxCaptionLength)]
        [Required]
        public string Caption { get; set; }

        /// <summary>Gets or sets the RollupCenterId field. </summary>
        [Range(1,Int32.MaxValue,ErrorMessage ="RollupCenter is Required ")]
        public int RollupCenterId { get; set; }       

        /// <summary>Gets or sets the IsCorporateDefault field. </summary>
        public bool IsCorporateDefault { get; set; }

        /// <summary>Gets or sets the ChartOfAccountId field. </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "ChartOfAccount is Required ")]
        public int ChartOfAccountId { get; set; }       

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public long? RollupAccountId { get; set; }        

        /// <summary>Gets or sets the TypeOfCurrencyId field. </summary>
        public int? TypeOfCurrencyId { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public int? RollupJobId { get; set; }        

        /// <summary>Gets or sets the TypeOfJobStatusId field. </summary>
        public ProjectStatus? TypeOfJobStatusId { get; set; }

        /// <summary>Gets or sets the TypeOfBidSoftwareId field. </summary>
        public BudgetSoftware? TypeOfBidSoftwareId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the IsICTDivision field. </summary>
        public bool IsICTDivision { get; set; }

        /// <summary>Gets or sets the TypeofProject field. </summary>
        public TypeofProject? TypeofProjectId { get; set; }

        /// <summary>Gets or sets the TaxRecovery field. </summary>
        public TaxRecovery? TaxRecoveryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<CreateJobCommercialInput> JobDetails { get; set; }


        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

    }
}
