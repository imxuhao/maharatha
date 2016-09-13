using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Data.Entity;
using Abp.Collections.Extensions;
using Abp.Linq.Extensions;
using System.Data.Entity.SqlServer;
using System;
using Abp.UI;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class JobPORangeAllocationUnitManager : DomainService
    {
        protected IRepository<JobPORangeAllocationUnit> JobPORangeAllocationUnitRepository { get; }

        public JobPORangeAllocationUnitManager(IRepository<JobPORangeAllocationUnit> jobporangeallocationunitrepository)
        {
            JobPORangeAllocationUnitRepository = jobporangeallocationunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting JobPORangeAllocation Entity 
        /// </summary>
        /// <param name="jobLocationUnit"></param>
        /// <returns></returns>       
        public virtual async Task CreateAsync(JobPORangeAllocationUnit jobporangeallocationunit)
        {
            await ValidateJobPORangeAllocationUnitAsync(jobporangeallocationunit);
            await JobPORangeAllocationUnitRepository.InsertAsync(jobporangeallocationunit);
        }

        /// <summary>
        /// Updating JobPORangeAllocation
        /// </summary>
        /// <param name="jobLocationUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(JobPORangeAllocationUnit jobporangeallocationunit)
        {
            await ValidateJobPORangeAllocationUnitAsync(jobporangeallocationunit);
            // await JobPORangeAllocationUnitRepository.UpdateAsync(jobporangeallocationunit);
        }

        /// <summary>
        /// delete JobPORangeAllocation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(IdInput input)
        {
            await JobPORangeAllocationUnitRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// Validating the JobPORangeAllocation
        /// </summary>
        /// <param name="jobporangeallocation"></param>
        /// <returns></returns>
        protected virtual async Task ValidateJobPORangeAllocationUnitAsync(JobPORangeAllocationUnit jobporangeallocation)
        {
            //Validating if Duplicate JobPORangeAllocation exists
            if (jobporangeallocation.PoRangeStartNumber >= jobporangeallocation.PoRangeEndNumber)
            { throw new UserFriendlyException(L("PoRangeStartNumbershouldalwayslessthanPoRangeEndNumber", jobporangeallocation.PoRangeStartNumber)); }
            else
            if (JobPORangeAllocationUnitRepository != null)
            {
                var JobPORangeAllocationUnit = await JobPORangeAllocationUnitRepository.GetAll()
                                        .Where(u =>
                                        ((u.PoRangeStartNumber <= jobporangeallocation.PoRangeStartNumber && u.PoRangeEndNumber >= jobporangeallocation.PoRangeStartNumber)
                                        ||
                                         (u.PoRangeStartNumber <= jobporangeallocation.PoRangeEndNumber && u.PoRangeEndNumber >= jobporangeallocation.PoRangeEndNumber)
                                         ||
                                         (u.PoRangeStartNumber >= jobporangeallocation.PoRangeStartNumber && u.PoRangeEndNumber <= jobporangeallocation.PoRangeEndNumber))
                                         && u.OrganizationUnitId == jobporangeallocation.OrganizationUnitId
                                         ).ToListAsync();

                var poRangeCount = JobPORangeAllocationUnit.Count;

                if (jobporangeallocation.Id == 0)
                {
                    if (poRangeCount > 0)
                    {
                        throw new UserFriendlyException(L("PoRangeAlreadyExist", jobporangeallocation.PoRangeStartNumber));
                    }
                }
                else
                {
                    if (poRangeCount == 1)
                    {
                        if (JobPORangeAllocationUnit.FirstOrDefault(p => p.Id == jobporangeallocation.Id
                            && p.PoRangeStartNumber == jobporangeallocation.PoRangeStartNumber
                            && p.PoRangeEndNumber == jobporangeallocation.PoRangeEndNumber
                            && p.OrganizationUnitId == jobporangeallocation.OrganizationUnitId) == null)
                        {
                            throw new UserFriendlyException(L("PoRangeAlreadyExist", jobporangeallocation.PoRangeStartNumber));
                        }
                    }
                    else if (poRangeCount > 1)
                    { throw new UserFriendlyException(L("PoRangeAlreadyExist", jobporangeallocation.PoRangeStartNumber)); }
                }
            }
        }

    }
}
