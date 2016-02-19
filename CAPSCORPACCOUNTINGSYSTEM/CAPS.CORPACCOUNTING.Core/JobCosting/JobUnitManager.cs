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
        /// Inserting Job Entity 
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(JobUnit jobUnit)
        {
            await ValidateJobUnitAsync(jobUnit);
            await JobUnitRepository.InsertAsync(jobUnit);
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
            //Validating if Duplicate Caption exists
            if (JobUnitRepository != null)
            {
                var jobUnits = (await JobUnitRepository.GetAllListAsync(p => p.Caption == jobUnit.Caption && p.OrganizationUnitId == jobUnit.OrganizationUnitId));

                if (jobUnit.Id == 0)
                {
                    if (jobUnits.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Caption", jobUnit.Caption));
                    }
                }
                else
                {
                    if (jobUnits.FirstOrDefault(p => p.Id != jobUnit.Id && p.Caption == jobUnit.Caption) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Caption", jobUnit.Caption));
                    }
                }
            }
        }
    }

}
