using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobAccountUnitAppService : CORPACCOUNTINGServiceBase, IJobAccountUnitAppService
    {
        private readonly JobAccountUnitManager _jobAccountUnitManager;
        private readonly IRepository<JobAccountUnit,long> _jobAccountUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JobAccountUnitAppService(JobAccountUnitManager jobAccountUnitManager, IRepository<JobAccountUnit, long> jobAccountUnitRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _jobAccountUnitManager = jobAccountUnitManager;
            _jobAccountUnitRepository = jobAccountUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<JobAccountUnitDto> CreateJobAccountUnit(CreateJobAccountUnitInput input)
        {
            var jobAccountUnit = new JobAccountUnit(jobid: input.JobId, accountid: input.AccountId, description: input.Description, rollupaccountId: input.RollupAccountId,
                rollupjobid: input.RollupJobId, organizationunitid: input.OrganizationUnitId);
            await _jobAccountUnitManager.CreateAsync(jobAccountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return jobAccountUnit.MapTo<JobAccountUnitDto>();

        }

        public async Task DeleteJobAccountUnit(IdInput input)
        {
            await _jobAccountUnitManager.DeleteAsync(input);
        }

        public async Task<ListResultOutput<JobAccountUnitDto>> GetJobAccountUnits(IdInput input)
        {
            var items =await _jobAccountUnitRepository.GetAllListAsync(p => p.JobId == input.Id);               

            return new ListResultOutput<JobAccountUnitDto>(
                items.Select(item =>
                {
                    var dto = item.MapTo<JobAccountUnitDto>();
                    dto.JobAccountId = item.Id;
                    return dto;
                }).ToList());
        }

        public async Task<JobAccountUnitDto> UpdateJobAccountUnit(UpdateJobAccountUnitInput input)
        {
            var jobAccountUnit = await _jobAccountUnitRepository.GetAsync(input.JobAccountId);

            #region Setting the values to be updated
            jobAccountUnit.JobId = input.JobId;
            jobAccountUnit.AccountId = input.AccountId;
            jobAccountUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobAccountUnit.RollupJobId = input.RollupJobId;
            jobAccountUnit.RollupAccountId = input.RollupAccountId;
            jobAccountUnit.Description = input.Description;
            #endregion
            await _jobAccountUnitManager.UpdateAsync(jobAccountUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            return jobAccountUnit.MapTo<JobAccountUnitDto>();
        }
    }
}
