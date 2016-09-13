using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapFrom(typeof(JobAccountUnit))]
    public class JobAccountUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the JobAccountId field. </summary>
        public long JobAccountId { get; set; }

        /// <summary>Gets or sets the JobId field.</summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public int? RollupJobId { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public long? RollupAccountId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the AccountNumber field. </summary>
        public string AccountNumber { get; set; }

        /// <summary>Gets or sets the RollupAccountNumber field. </summary>
        public string RollupAccountNumber { get; set; }

        /// <summary>Gets or sets the RollupAccountDescription field. </summary>
        public string RollupAccountDescription { get; set; }

        /// <summary>Gets or sets the RollupDivisionNumber field. </summary>
        public string RollupJobNumber { get; set; }

        /// <summary>Gets or sets the RollupDivisionDescription field. </summary>
        public string RollupJobDescription { get; set; }


    }
}
