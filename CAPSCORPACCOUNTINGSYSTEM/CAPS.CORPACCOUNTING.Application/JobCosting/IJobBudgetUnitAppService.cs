using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on JobBudget.
    /// </summary>
    public interface IJobBudgetUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the JobBudget.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobBudgetUnitDto> CreateJobBudgetUnit(CreateJobBudgetUnitInput input);

        /// <summary>
        ///  Update the JobBudget based on JobBudgetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobBudgetUnitDto> UpdateJobBudgetUnit(UpdateJobBudgetUnitInput input);

        /// <summary>
        /// Delete the JobBudget based on JobBudgetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobBudgetUnit(IdInput input);

        /// <summary>
        /// Get the JobBudget based on JobBudgetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobBudgetUnitDto> GetJobBudgetUnitsById(IdInput input);
    }
}
