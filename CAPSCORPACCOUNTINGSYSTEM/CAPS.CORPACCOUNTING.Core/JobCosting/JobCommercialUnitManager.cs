using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.UI;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class JobCommercialUnitManager : DomainService
    {
        protected IRepository<JobCommercialUnit> JobDetailUnitRepository { get;  }

        public JobCommercialUnitManager(IRepository<JobCommercialUnit> jobdetailunitrepository)
        {
            JobDetailUnitRepository = jobdetailunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting JobDetail Entity 
        /// </summary>
        /// <param name="jobDetailUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<int> CreateAsync(JobCommercialUnit jobDetailUnit)
        {
            await ValidateJobDetailUnitAsync(jobDetailUnit);
          return await JobDetailUnitRepository.InsertAndGetIdAsync(jobDetailUnit);
        }

        /// <summary>
        /// Updating Job Details
        /// </summary>
        /// <param name="jobDetailUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(JobCommercialUnit jobDetailUnit)
        {
            await ValidateJobDetailUnitAsync(jobDetailUnit);
            await JobDetailUnitRepository.UpdateAsync(jobDetailUnit);
        }
       

        /// <summary>
        /// Validating the Job
        /// </summary>
        /// <param name="jobUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateJobDetailUnitAsync(JobCommercialUnit jobUnit)
        {
           // Validating if Duplicate Caption exists
            if (JobDetailUnitRepository != null)
            {
                var jobUnits = (await JobDetailUnitRepository.GetAllListAsync(p => p.Caption == jobUnit.Caption && p.OrganizationUnitId == jobUnit.OrganizationUnitId));

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
