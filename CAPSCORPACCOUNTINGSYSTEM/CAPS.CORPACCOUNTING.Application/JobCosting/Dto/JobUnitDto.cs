using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{   
    [AutoMapFrom(typeof(JobUnit))]
    public class JobUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the JobNumber field. </summary>
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the RollupCenterId field. </summary>
        public int? RollupCenterId { get; set; }

        /// <summary>Gets or sets the IsCorporateDefault field. </summary>
        public bool IsCorporateDefault { get; set; }

        /// <summary>Gets or sets the ChartOfAccountId field. </summary>
        public int? ChartOfAccountId { get; set; }

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
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsICTDivision field. </summary>
        public bool IsICTDivision { get; set; }

        /// <summary>Gets or sets the TypeofProject field. </summary>
        public TypeofProject? TypeofProjectId { get; set; }

        /// <summary>Gets or sets the TaxRecoveryId field. </summary>
        public int? TaxRecoveryId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long OrganizationUnitId { get; set; }

        /// <summary> Gets or sets the JobDetails.</summary>
        public List<JobCommercialUnitDto> JobDetails { get; set; }
        private string DetailReport { get; set; } = "Detail Report";
       
        /// <summary> Gets or sets the Director.</summary>
        public string DirectorName { get; set; }
       
        /// <summary> Gets or sets the Agency.</summary>
        public string Agency { get; set; }
        /// <summary>
        ///  Gets or sets the JobStatusName 
        /// </summary>
        public string JobStatusName { get; set; }
        /// <summary>
        ///  Gets or sets the TypeofProjectName
        /// </summary>
        public string TypeofProjectName { get; set; }

        /// <summary>
        /// Gets or sets the TransactionCount
        /// </summary>
        public string TransactionCount { get; set; }

        /// <summary>
        /// Gets or sets the POTransactionType
        /// </summary>
        public string POTransactionType { get; set; }

        /// <summary>Gets or sets the TaxCreditId field. </summary>
        public int? TaxCreditId { get; set; }


    }
}
