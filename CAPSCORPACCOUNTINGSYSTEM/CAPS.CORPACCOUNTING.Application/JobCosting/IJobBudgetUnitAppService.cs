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
        /// This is used to create the JobBudget.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobBudgetUnitDto> CreateJobBudgetUnit(CreateJobBudgetUnitInput input);

        /// <summary>
        ///  This is used to update the JobBudget based on JobBudgetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobBudgetUnitDto> UpdateJobBudgetUnit(UpdateJobBudgetUnitInput input);

        /// <summary>
        /// This is used to delete the JobBudget based on JobBudgetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobBudgetUnit(IdInput input);

        /// <summary>
        /// This is used to get the JobBudget based on JobBudgetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobBudgetUnitDto> GetJobBudgetUnitsById(IdInput input);
    }
}
