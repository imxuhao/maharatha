using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Authorization;
using System.Data.Entity;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// 
    /// </summary>
     [AbpAuthorize]
    public class JobPORangeAllocationUnitAppService  : CORPACCOUNTINGServiceBase, IJobPORangeAllocationUnitAppService
    {
        private readonly JobPORangeAllocationUnitManager _jobPORangeAllocationUnitManager;
        private readonly IRepository<JobPORangeAllocationUnit> _jobPORangeAllocationUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public JobPORangeAllocationUnitAppService(JobPORangeAllocationUnitManager jobPORangeAllocationUnitManager, IRepository<JobPORangeAllocationUnit> jobPORangeAllocationUnitRepository,
          IUnitOfWorkManager unitOfWorkManager)
        {
            _jobPORangeAllocationUnitManager = jobPORangeAllocationUnitManager;
            _jobPORangeAllocationUnitRepository = jobPORangeAllocationUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        /// <summary>
        /// Create the JobPORangeAllocation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<JobPORangeAllocationUnitDto> CreateJobPORangeAllocationUnit(CreateJobPORangeAllocationInput input)
        {
            var jobPORangeAllocation = input.MapTo<JobPORangeAllocationUnit>();
            await _jobPORangeAllocationUnitManager.CreateAsync(jobPORangeAllocation);

            await CurrentUnitOfWork.SaveChangesAsync();
            return jobPORangeAllocation.MapTo<JobPORangeAllocationUnitDto>();
        }


        /// <summary>
        /// Update the JobPORangeAllocation based on JobLocationId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JobPORangeAllocationUnitDto> UpdateJobPORangeAllocationUnit(UpdateJobPORangeAllocationInput input)
        {
            var jobPORangeAllocation = await _jobPORangeAllocationUnitRepository.GetAsync(input.PORangeAllocationId);

            #region Setting the values to be updated
            jobPORangeAllocation.PoRangeStartNumber = input.PoRangeStartNumber;
            jobPORangeAllocation.PoRangeEndNumber = input.PoRangeEndNumber;
            #endregion
            await _jobPORangeAllocationUnitManager.UpdateAsync(jobPORangeAllocation);

            await CurrentUnitOfWork.SaveChangesAsync();

            return jobPORangeAllocation.MapTo<JobPORangeAllocationUnitDto>();
        }



        /// <summary>
        /// Get the list of all JobPORangeAllocation based on PORangeAllocationId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<JobPORangeAllocationUnitDto>> GetJobPORangeAllocationByJobId(GetJobInput input)
        {
            var query = _jobPORangeAllocationUnitRepository.GetAll().Where(u => u.JobId == input.JobId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .ToListAsync();

            return new ListResultOutput<JobPORangeAllocationUnitDto>(results.Select(item =>
            {
                var dto = item.MapTo<JobPORangeAllocationUnitDto>();
                dto.PORangeAllocationId = item.Id;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// Delete the JobPORangeAllocation based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteJobPORangeAllocationUnit(IdInput input)
        {
            await _jobPORangeAllocationUnitRepository.DeleteAsync(input.Id);
        }
    }
}
