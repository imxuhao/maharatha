using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;
namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class JobAccountUnitManager : DomainService
    {
        protected IRepository<JobAccountUnit,long> JobAccountUnitRepository { get; }

        public JobAccountUnitManager(IRepository<JobAccountUnit,long> jobAccountUnitRepository)
        {
            JobAccountUnitRepository = jobAccountUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(JobAccountUnit jobAccountUnit)
        {           
            await JobAccountUnitRepository.InsertAsync(jobAccountUnit);
        }

        public virtual async Task UpdateAsync(JobAccountUnit jobAccountUnit)
        {
            await JobAccountUnitRepository.UpdateAsync(jobAccountUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await JobAccountUnitRepository.DeleteAsync(p=>p.JobId==input.Id);
        }
    }
}
