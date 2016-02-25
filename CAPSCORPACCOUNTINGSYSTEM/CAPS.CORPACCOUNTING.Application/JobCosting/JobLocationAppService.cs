using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;
using System.Linq;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobLocationAppService : CORPACCOUNTINGServiceBase, IJobLocationAppService
    {
        private readonly JobLocationUnitManager _jobLocationUnitManager;
        private readonly IRepository<JobLocationUnit> _jobLocationUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JobLocationAppService(JobLocationUnitManager jobLocationUnitManager, IRepository<JobLocationUnit> jobLocationUnitRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _jobLocationUnitManager = jobLocationUnitManager;
            _jobLocationUnitRepository = jobLocationUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<JobLocationUnitDto> CreateJobLocationUnit(CreateJobLocationInput input)
        {
            var jobLocationUnit = new JobLocationUnit(jobid: input.JobId, locationsitedate: input.LocationSiteDate, organizationunitid: input.OrganizationUnitId, locationid: input.LocationId);
            await _jobLocationUnitManager.CreateAsync(jobLocationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return jobLocationUnit.MapTo<JobLocationUnitDto>();
        }

        public async Task<JobLocationUnitDto> UpdateJobLocationUnit(UpdateJobLocationInput input)
        {

            var jobLocationUnit = await _jobLocationUnitRepository.GetAsync(input.JobLocationId);

            #region Setting the values to be updated
            jobLocationUnit.JobId = input.JobId;
            jobLocationUnit.LocationSiteDate = input.LocationSiteDate;
            jobLocationUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobLocationUnit.LocationId = input.LocationId;
            #endregion
            await _jobLocationUnitManager.UpdateAsync(jobLocationUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return jobLocationUnit.MapTo<JobLocationUnitDto>();
        }

        public async Task<ListResultOutput<JobLocationUnitDto>> GetJobLocationUnitsByJobId(IdInput input)
        {
            var items = await _jobLocationUnitRepository.GetAllListAsync(p => p.JobId == input.Id);

            return new ListResultOutput<JobLocationUnitDto>(
                items.Select(item =>
                {
                    var dto = item.MapTo<JobLocationUnitDto>();
                    dto.JobLocationId = item.Id;
                    return dto;
                }).ToList());
        }

        public async Task DeleteJobLocationUnit(IdInput input)
        {
            await _jobLocationUnitManager.DeleteAsync(input);
        }
    }
}
