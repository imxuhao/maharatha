using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CAPS.CORPACCOUNTING.Masters;
using Abp.Authorization;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobUnitAppService : CORPACCOUNTINGServiceBase, IJobUnitAppService
    {
        private readonly JobUnitManager _jobUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<JobCommercialUnit> _jobDetailUnitRepository;
        private readonly IRepository<EmployeeUnit> _employeeUnitRepository;
        private readonly IRepository<CustomerUnit> _customerUnitRepository;
        private readonly IJobCommercialAppService _jobCommercialAppService;

        public JobUnitAppService(JobUnitManager jobUnitManager, IRepository<JobUnit> jobUnitRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<JobCommercialUnit> jobDetailUnitRepository,
            IRepository<EmployeeUnit> employeeUnitRepository, IRepository<CustomerUnit> customerUnitRepository, IJobCommercialAppService jobCommercialAppService)
        {
            _jobUnitManager = jobUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _jobDetailUnitRepository = jobDetailUnitRepository;
            _employeeUnitRepository = employeeUnitRepository;
            _customerUnitRepository = customerUnitRepository;
            _jobCommercialAppService = jobCommercialAppService;
        }
        /// <summary>
        /// To create the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JobUnitDto> CreateJobUnit(CreateJobUnitInput input)
        {
            var jobUnit = new JobUnit(jobnumber: input.JobNumber, caption: input.Caption, iscorporatedefault: input.IsCorporateDefault, rollupaccountid: input.RollupAccountId,
                typeofcurrencyid: input.TypeOfCurrencyId, rollupjobid: input.RollupJobId, typeofjobstatusid: input.TypeOfJobStatusId, typeofbidsoftwareid: input.TypeOfBidSoftwareId,
                isapproved: input.IsApproved, isactive: input.IsActive, isictdivision: input.IsICTDivision, organizationunitid: input.OrganizationUnitId, typeofprojectid: input.TypeofProjectId, taxrecoveryid: input.TaxRecoveryId
                , chartofaccountid: input.ChartOfAccountId, rollupcenterid: input.RollupCenterId);
            await _jobUnitManager.CreateAsync(jobUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return jobUnit.MapTo<JobUnitDto>();
        }

        /// <summary>
        ///To update the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<JobUnitDto> UpdateJobUnit(UpdateJobUnitInput input)
        {
            var jobUnit = await _jobUnitRepository.GetAsync(input.JobId);

            #region Setting the values to be updated

            jobUnit.JobNumber = input.JobNumber;
            jobUnit.Caption = input.Caption;
            jobUnit.IsCorporateDefault = input.IsCorporateDefault;
            jobUnit.RollupAccountId = input.RollupAccountId;
            jobUnit.TypeOfCurrencyId = input.TypeOfCurrencyId;
            jobUnit.RollupJobId = input.RollupJobId;
            jobUnit.TypeOfJobStatusId = input.TypeOfJobStatusId;
            jobUnit.TypeOfBidSoftwareId = input.TypeOfBidSoftwareId;
            jobUnit.IsApproved = input.IsApproved;
            jobUnit.IsActive = input.IsActive;
            jobUnit.IsICTDivision = input.IsICTDivision;
            jobUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobUnit.TypeofProjectId = input.TypeofProjectId;
            jobUnit.TaxRecoveryId = input.TaxRecoveryId;
            jobUnit.ChartOfAccountId = input.ChartOfAccountId;
            jobUnit.RollupCenterId = input.RollupCenterId;

            #endregion

            await _jobUnitManager.UpdateAsync(jobUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Job is Added*/
            };

            return jobUnit.MapTo<JobUnitDto>();
        }

        /// <summary>
        /// To Delete the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task DeleteJobUnit(IdInput input)
        {
            await _jobCommercialAppService.DeleteJobDetailUnit(input);
            await _jobUnitManager.DeleteAsync(input.Id);
        }
        /// <summary>
        /// To Get the Job with JobDetails to show in the Grid With searching and sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<JobUnitDto>> GetJobUnits(GetJobInput input)
        {
            var query = from job in _jobUnitRepository.GetAll().Include(p => p.JobDetails)
                        join emp in _employeeUnitRepository.GetAll() on job.JobDetails.FirstOrDefault().DirectorEmployeeId equals emp.Id
                                 into temp1
                        from em in temp1.DefaultIfEmpty()
                        join cust in _customerUnitRepository.GetAll() on job.JobDetails.FirstOrDefault().AgencyId equals cust.Id
                                into tempcust
                        from cs in tempcust.DefaultIfEmpty()
                        select new { Job = job, Director = em.LastName, Agency = cs.LastName };
            query = query
                .WhereIf(input.OrganizationUnitId != null, item => item.Job.OrganizationUnitId == input.OrganizationUnitId)
                .WhereIf(!input.Caption.IsNullOrWhiteSpace(), item => item.Job.Caption.Contains(input.Caption))
                .WhereIf(!input.ProductName.IsNullOrWhiteSpace(), item => item.Job.JobDetails.FirstOrDefault().ProductName.Contains(input.ProductName))
                .WhereIf(!input.JobNumber.IsNullOrWhiteSpace(), item => item.Job.JobNumber.Contains(input.JobNumber))
                .WhereIf(!input.Director.IsNullOrWhiteSpace(), item => item.Director.Contains(input.Director))
                .WhereIf(!input.Agency.IsNullOrWhiteSpace(), item => item.Agency.Contains(input.Agency))
                .WhereIf(input.TypeOfJobStatusId != null, item => item.Job.TypeOfJobStatusId == input.TypeOfJobStatusId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<JobUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Job.MapTo<JobUnitDto>();
                dto.JobId = item.Job.Id;
                if (item.Director != null)
                    dto.Director = item.Director;
                if (item.Agency != null)
                    dto.Agency = item.Agency;
                return dto;
            }).ToList());
        }

        public async Task<JobUnitDto> GetJobUnitById(IdInput input)
        {
            JobUnit jobitem = await _jobUnitRepository.GetAsync(input.Id);
            JobUnitDto result = jobitem.MapTo<JobUnitDto>();
            result.JobId = jobitem.Id;
            return result;

        }        

    }
}
