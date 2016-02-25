using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
        public class CreateJobBudgetUnitInput : IInputDto
        {
            /// <summary>Gets or sets the JobId field. </summary>
            public int? JobId { get; set; }

            /// <summary>Gets or sets the Description field. </summary>
            [Required]
            [StringLength(JobBudgetUnit.MaxDescLength)]
            public string Description { get; set; }

            /// <summary>Gets or sets the TypeofBudgetId field. </summary>
            [EnumDataType(typeof(TypeofBudget))]
            public TypeofBudget TypeofBudgetId { get; set; }

            /// <summary>Gets or sets the TypeofBudgetSoftwareId field. </summary>
            [EnumDataType(typeof(BudgetSoftware))]
            public BudgetSoftware TypeofBudgetSoftwareId { get; set; }

            /// <summary>Gets or sets the CompanyId field. </summary>
            public long? OrganizationUnitId { get; set; }
    }
}
