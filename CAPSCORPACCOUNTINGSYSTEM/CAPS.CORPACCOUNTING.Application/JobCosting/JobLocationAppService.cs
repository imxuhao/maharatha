using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Authorization;
using System.Data.Entity;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobLocationAppService : CORPACCOUNTINGServiceBase, IJobLocationAppService
    {
        private readonly JobLocationUnitManager _jobLocationUnitManager;
        private readonly IRepository<JobLocationUnit> _jobLocationUnitRepository;
        private readonly IRepository<SubAccountUnit, long> _subAccountUnitRepository;
        public JobLocationAppService(JobLocationUnitManager jobLocationUnitManager,
            IRepository<JobLocationUnit> jobLocationUnitRepository,
            IRepository<SubAccountUnit, long> subAccountUnitRepository)
        {
            _jobLocationUnitManager = jobLocationUnitManager;
            _jobLocationUnitRepository = jobLocationUnitRepository;
            _subAccountUnitRepository = subAccountUnitRepository;
        }

        [UnitOfWork]
        public async Task<JobLocationUnitDto> CreateJobLocationUnit(CreateJobLocationInput input)
        {
            var jobLocationUnit = new JobLocationUnit(jobid: input.JobId, locationsitedate: input.LocationSiteDate,
                locationid: input.LocationId);
            await _jobLocationUnitManager.CreateAsync(jobLocationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return jobLocationUnit.MapTo<JobLocationUnitDto>();
        }

        public async Task<JobLocationUnitDto> UpdateJobLocationUnit(UpdateJobLocationInput input)
        {

            var jobLocationUnit = await _jobLocationUnitRepository.GetAsync(input.JobLocationId);

            #region Setting the values to be updated
            jobLocationUnit.LocationSiteDate = input.LocationSiteDate;
            jobLocationUnit.LocationId = input.LocationId;
            #endregion
            await _jobLocationUnitManager.UpdateAsync(jobLocationUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return jobLocationUnit.MapTo<JobLocationUnitDto>();
        }

        public async Task<ListResultOutput<JobLocationUnitDto>> GetJobLocationUnitsByJobId(GetJobInput input)
        {
            var items = await (from joblocation in _jobLocationUnitRepository.GetAll()
                               join location in _subAccountUnitRepository.GetAll()
                               .Where(p => p.TypeofSubAccountId == TypeofSubAccount.Locations || p.TypeofSubAccountId == TypeofSubAccount.Sets)
                               on joblocation.LocationId equals location.Id
                               where joblocation.JobId == input.JobId
                               select new { JobLocation = joblocation, LocationName = location.Description }).ToListAsync();

            return new ListResultOutput<JobLocationUnitDto>(
                items.Select(item =>
                {
                    var dto = item.JobLocation.MapTo<JobLocationUnitDto>();
                    dto.JobLocationId = item.JobLocation.Id;
                    dto.LocationName = item.LocationName;
                    return dto;
                }).ToList());
        }

        public async Task DeleteJobLocationUnit(IdInput input)
        {
            await _jobLocationUnitManager.DeleteAsync(input);
        }
    }
}
