using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapFrom(typeof(JobBudgetUnit))]
    public class JobBudgetUnitDto:IOutputDto
    {
        /// <summary>Gets or sets the JobBudgetId field. </summary>
        public int JobBudgetId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int? JobId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the TypeofBudgetId field. </summary>
        public TypeofBudget TypeofBudgetId { get; set; }

        /// <summary>Gets or sets the TypeofBudgetSoftwareId field. </summary>
        public BudgetSoftware TypeofBudgetSoftwareId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
