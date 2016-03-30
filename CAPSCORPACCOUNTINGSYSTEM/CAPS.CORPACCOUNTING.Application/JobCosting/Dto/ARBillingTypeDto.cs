using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapFrom(typeof(ARBillingTypeUnit))]
    public class ARBillingTypeDto : IOutputDto
    {
        /// <summary>Gets or sets the ARBillingTypeId field.</summary>
        public int ARBillingTypeId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }
        /// <summary>Gets or sets the AccountId field. </summary>
        public long AccountId { get; set; }

        /// <summary>Gets or sets the IsIctBillingType field. </summary>
        public bool IsIctBillingType { get; set; }

        /// <summary>Gets or sets the IsProjectBilling field. </summary>
        public bool IsProjectBilling { get; set; }

        /// <summary>Gets or sets the TypeofBillingId field. </summary>
        public TypeofBilling TypeofBillingId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
