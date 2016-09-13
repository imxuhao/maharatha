using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;
namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class JobBudgetUnitManager : DomainService
    {
        protected IRepository<JobBudgetUnit> JobBudgetUnitRepository { get; }

        public JobBudgetUnitManager(IRepository<JobBudgetUnit> jobBudgetUnitRepository)
        {
            JobBudgetUnitRepository = jobBudgetUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(JobBudgetUnit jobBudgetUnit)
        {           
            await JobBudgetUnitRepository.InsertAsync(jobBudgetUnit);
        }

        public virtual async Task UpdateAsync(JobBudgetUnit jobBudgetUnit)
        {
            await JobBudgetUnitRepository.UpdateAsync(jobBudgetUnit);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await JobBudgetUnitRepository.DeleteAsync(id);
        }
    }
}
