using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobBudgetUnitAppService : CORPACCOUNTINGServiceBase, IJobBudgetUnitAppService
    {
        private readonly JobBudgetUnitManager _jobBudgetUnitManager;
        private readonly IRepository<JobBudgetUnit> _jobBudgetUnitRepository;

        public JobBudgetUnitAppService(JobBudgetUnitManager jobBudgetUnitManager, IRepository<JobBudgetUnit> jobBudgetUnitRepository)
        {
            _jobBudgetUnitManager = jobBudgetUnitManager;
            _jobBudgetUnitRepository = jobBudgetUnitRepository;
        }
        public async Task<JobBudgetUnitDto> CreateJobBudgetUnit(CreateJobBudgetUnitInput input)
        {
            var jobBudgetUnit = new JobBudgetUnit(jobid: input.JobId, description: input.Description, typeofbudgetid: input.TypeofBudgetId,
                typeofbudgetsoftwareid: input.TypeofBudgetSoftwareId, organizationunitid: input.OrganizationUnitId);
            await _jobBudgetUnitManager.CreateAsync(jobBudgetUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return jobBudgetUnit.MapTo<JobBudgetUnitDto>();
        }

        public async Task DeleteJobBudgetUnit(IdInput input)
        {
            await _jobBudgetUnitManager.DeleteAsync(input.Id);
        }

        public async Task<JobBudgetUnitDto> GetJobBudgetUnitsById(IdInput input)
        {
            var jobBudget = await _jobBudgetUnitRepository.GetAsync(input.Id);
            var result=jobBudget.MapTo<JobBudgetUnitDto>();
            result.JobBudgetId = jobBudget.Id;
            return result;
        }

        public async Task<JobBudgetUnitDto> UpdateJobBudgetUnit(UpdateJobBudgetUnitInput input)
        {
            var jobBudgetUnit = await _jobBudgetUnitRepository.GetAsync(input.JobBudgetId);

            #region Setting the values to be updated

            jobBudgetUnit.JobId = input.JobId;
            jobBudgetUnit.TypeofBudgetId = input.TypeofBudgetId;
            jobBudgetUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobBudgetUnit.TypeofBudgetSoftwareId = input.TypeofBudgetSoftwareId;
            jobBudgetUnit.Description = input.Description;
           
            #endregion
            await _jobBudgetUnitManager.UpdateAsync(jobBudgetUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return jobBudgetUnit.MapTo<JobBudgetUnitDto>();
        }
    }
}
