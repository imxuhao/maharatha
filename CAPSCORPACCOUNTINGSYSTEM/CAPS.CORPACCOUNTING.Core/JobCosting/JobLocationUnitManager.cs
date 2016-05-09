using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;


namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class JobLocationUnitManager : DomainService
    {
        protected IRepository<JobLocationUnit> JobLocationUnitRepository { get; }
          
        public JobLocationUnitManager(IRepository<JobLocationUnit> joblocationunitrepository)
        {
            JobLocationUnitRepository = joblocationunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting JobLocation Entity 
        /// </summary>
        /// <param name="jobLocationUnit"></param>
        /// <returns></returns>       
        public virtual async Task CreateAsync(JobLocationUnit jobLocationUnit)
        {
            await JobLocationUnitRepository.InsertAsync(jobLocationUnit);
        }

        /// <summary>
        /// Updating JobLocation
        /// </summary>
        /// <param name="jobLocationUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(JobLocationUnit jobLocationUnit)
        {          
            await JobLocationUnitRepository.UpdateAsync(jobLocationUnit);
        }
        public virtual async Task DeleteAsync(IdInput input)
        {
            await JobLocationUnitRepository.DeleteAsync(input.Id);
        }

    }
}
