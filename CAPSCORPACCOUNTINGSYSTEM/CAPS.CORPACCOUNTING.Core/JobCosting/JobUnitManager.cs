using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class JobUnitManager : DomainService
    {
        protected IRepository<JobUnit> JobUnitRepository { get;  }

        public JobUnitManager(IRepository<JobUnit> jobunitrepository)
        {
            JobUnitRepository = jobunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Creating Job Entity 
        /// </summary>
        /// <param name="jobUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<int> CreateAsync(JobUnit jobUnit)
        {
            await ValidateJobUnitAsync(jobUnit);
          return  await JobUnitRepository.InsertAndGetIdAsync(jobUnit);
        }

        /// <summary>
        /// Updating Job Details
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(JobUnit job)
        {
            await ValidateJobUnitAsync(job);
            await JobUnitRepository.UpdateAsync(job);
        }

        /// <summary>
        /// Deleting Job Entity 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await JobUnitRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Validating the Job
        /// </summary>
        /// <param name="jobUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateJobUnitAsync(JobUnit jobUnit)
        {
            //Validating if Duplicate JobNumber exists
            if (JobUnitRepository != null)
            {
                var jobUnits = (await JobUnitRepository.GetAllListAsync(p => p.JobNumber == jobUnit.JobNumber));

                if (jobUnit.Id == 0)
                {
                    if (jobUnits.Count > 0)
                    {
                        throw new UserFriendlyException(L("DuplicateNumber") , jobUnit.JobNumber);
                    }
                }
                else
                {
                    if (jobUnits.FirstOrDefault(p => p.Id != jobUnit.Id && p.JobNumber == jobUnit.JobNumber) != null)
                    {
                        throw new UserFriendlyException(L("DuplicateNumber"),jobUnit.JobNumber);
                    }
                }
            }
        }
    }

}
