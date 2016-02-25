using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobBudgetUnitAppService : IApplicationService
    {
        Task<JobBudgetUnitDto> CreateJobBudgetUnit(CreateJobBudgetUnitInput input);

        Task<JobBudgetUnitDto> UpdateJobBudgetUnit(UpdateJobBudgetUnitInput input);
        Task DeleteJobBudgetUnit(IdInput input);
        Task<JobBudgetUnitDto> GetJobBudgetUnitsById(IdInput input);
    }
}
