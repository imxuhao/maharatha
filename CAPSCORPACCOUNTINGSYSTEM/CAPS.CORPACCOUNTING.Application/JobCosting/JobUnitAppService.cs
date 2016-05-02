using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using CAPS.CORPACCOUNTING.Masters;
using Abp.Authorization;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Sessions;
using System;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Masters.Dto;

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
        private readonly IJobBudgetUnitAppService _jobBudgetUnitAppService;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly CustomAppSession _customAppSessionSession;
        long? OrgnizationId = null;

        public JobUnitAppService(JobUnitManager jobUnitManager, IRepository<JobUnit> jobUnitRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<JobCommercialUnit> jobDetailUnitRepository,
            IRepository<EmployeeUnit> employeeUnitRepository, IRepository<CustomerUnit> customerUnitRepository, IJobCommercialAppService jobCommercialAppService, IJobBudgetUnitAppService jobBudgetUnitAppService,
            IRepository<OrganizationUnit, long> organizationUnitRepository, CustomAppSession customAppSessionSession,
            IRepository<CoaUnit> coaUnitRepository, IRepository<AccountUnit, long> accountUnitRepository)
        {
            _jobUnitManager = jobUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _jobDetailUnitRepository = jobDetailUnitRepository;
            _employeeUnitRepository = employeeUnitRepository;
            _customerUnitRepository = customerUnitRepository;
            _jobCommercialAppService = jobCommercialAppService;
            _jobBudgetUnitAppService = jobBudgetUnitAppService;
            _organizationUnitRepository = organizationUnitRepository;
            _customAppSessionSession = customAppSessionSession;
            _coaUnitRepository = coaUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            if (!ReferenceEquals(_customAppSessionSession.OrganizationId, null))
                OrgnizationId = Convert.ToInt64(_customAppSessionSession.OrganizationId);

        }
        /// <summary>
        /// To create the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Create)]
        public async Task<JobUnitDto> CreateJobUnit(CreateJobUnitInput input)
        {
            var jobUnit = new JobUnit(jobnumber: input.JobNumber, caption: input.Caption, iscorporatedefault: input.IsCorporateDefault, rollupaccountid: input.RollupAccountId,
                typeofcurrencyid: input.TypeOfCurrencyId, rollupjobid: input.RollupJobId, typeofjobstatusid: input.TypeOfJobStatusId, typeofbidsoftwareid: input.TypeOfBidSoftwareId,
                isapproved: input.IsApproved, isactive: input.IsActive, isictdivision: input.IsICTDivision, organizationunitid: OrgnizationId, typeofprojectid: input.TypeofProjectId,
                taxrecoveryid: input.TaxRecoveryId, chartofaccountid: input.ChartOfAccountId, rollupcenterid: input.RollupCenterId, isdivision: false);
            await _jobUnitManager.CreateAsync(jobUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            //create job details

            if (ReferenceEquals(input.JobDetails, null))
                input.JobDetails = new List<CreateJobCommercialInput>() {new CreateJobCommercialInput() };

            input.JobDetails.ForEach(u => { u.JobId = jobUnit.Id; });
            
            foreach (var jobdetails in input.JobDetails)
            {
                await _jobCommercialAppService.CreateJobDetailUnit(jobdetails);
            }

            //create jobbudget
            //if(ReferenceEquals(input.JobBudget, null))
            //    input.JobBudget = new List<CreateJobBudgetUnitInput>() { new CreateJobBudgetUnitInput() };

            //input.JobBudget.ForEach(u => { u.JobId = jobUnit.Id; });
            //foreach (var jobBudget in input.JobBudget)
            //{
            //    await _jobBudgetUnitAppService.CreateJobBudgetUnit(jobBudget);
            //}
            return jobUnit.MapTo<JobUnitDto>();
        }

        /// <summary>
        ///To update the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Edit)]
        [UnitOfWork]
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
            jobUnit.OrganizationUnitId = OrgnizationId;
            jobUnit.TypeofProjectId = input.TypeofProjectId;
            jobUnit.TaxRecoveryId = input.TaxRecoveryId;
            jobUnit.ChartOfAccountId = input.ChartOfAccountId;
            jobUnit.RollupCenterId = input.RollupCenterId;

            #endregion

            await _jobUnitManager.UpdateAsync(jobUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (!ReferenceEquals(input.JobDetails, null))
            {
                await _jobCommercialAppService.UpdateJobDetailUnit(input.JobDetails);
            } 
            return jobUnit.MapTo<JobUnitDto>();
        }

        /// <summary>
        /// To Delete the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Delete)]
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
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects)]
        public async Task<PagedResultOutput<JobUnitDto>> GetJobUnits(SearchInputDto input)
        {
            var query = from job in _jobUnitRepository.GetAll().Include(p => p.JobDetails)
                        join emp in _employeeUnitRepository.GetAll() on job.JobDetails.FirstOrDefault().DirectorEmployeeId equals emp.Id
                                 into employee
                        from em in employee.DefaultIfEmpty()
                        join cust in _customerUnitRepository.GetAll() on job.JobDetails.FirstOrDefault().AgencyId equals cust.Id
                                into tempcust
                        from cs in tempcust.DefaultIfEmpty()
                        select new { Job = job, DirectorName = em.LastName, Agency = cs.LastName };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.WhereIf( !ReferenceEquals(OrgnizationId,null), item => item.Job.OrganizationUnitId == OrgnizationId)
                     .Where(item => item.Job.IsDivision == false);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Job.JobNumber ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<JobUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Job.MapTo<JobUnitDto>();
                dto.JobId = item.Job.Id;
                if (item.DirectorName != null)
                    dto.DirectorName = item.DirectorName;
                if (item.Agency != null)
                    dto.Agency = item.Agency;
                dto.JobStatusName = item.Job.TypeOfJobStatusId != null ? item.Job.TypeOfJobStatusId.ToDisplayName() : "";
                dto.TypeofProjectName = item.Job.TypeofProjectId != null ? item.Job.TypeofProjectId.ToDisplayName() : "";                
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

        public async Task<List<NameValueDto>> GetOrganizationUnits(IdInput input)
        {
            var organizations = await _organizationUnitRepository.GetAll().Where(p => p.Id != input.Id)
                .Select(u => new NameValueDto { Name = u.DisplayName, Value = u.Id.ToString() }).ToListAsync();
            return organizations;
        }
        public async Task<List<NameValueDto>> GetRollupAccountList(AutoSearchInput input)
        {
            var query = from account in _accountUnitRepository.GetAll()
                        join coa in _coaUnitRepository.GetAll() on account.ChartOfAccountId equals coa.Id
                        where coa.IsCorporate == true && account.IsRollupAccount == true
                        select new { account };
            var divisions = await query.
                WhereIf(!ReferenceEquals(OrgnizationId, null), p => p.account.OrganizationUnitId == OrgnizationId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.account.Caption.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.account.Caption, Value = u.account.Id.ToString() }).ToListAsync();
            return divisions;

        }

        public async Task<List<NameValueDto>> GetProjectCoaList(AutoSearchInput input)
        {
            var divisions = await _coaUnitRepository.GetAll().Where(p => p.IsCorporate == false)
                 .WhereIf(!ReferenceEquals(OrgnizationId, null), p => p.OrganizationUnitId == OrgnizationId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Caption, Value = u.Id.ToString() }).ToListAsync();
            return divisions;

        }

        /// <summary>
        /// Get DivisionsList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetDivisionList(AutoSearchInput input)
        {
            var divisions = await _jobUnitRepository.GetAll().Where(p => p.IsDivision == true)
                 .WhereIf(!ReferenceEquals(OrgnizationId, null), p => p.OrganizationUnitId == OrgnizationId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Caption.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Caption, Value = u.Id.ToString() }).ToListAsync();
            return divisions;
        }
        /// <summary>
        ///  Get BudgetSoftwareList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetBudgetSoftwareList()
        {
            return EnumList.GetBudgetSoftwareList();
        }
        /// <summary>
        /// Get ProjectStatusList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetProjectStatusList()
        {
            return EnumList.GetProjectStatusList();
        }

        /// <summary>
        /// Get ProjectTypeList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetProjectTypeList()
        {
            return EnumList.GetProjectTypeList();
        }

        /// <summary>
        /// Get AllFinancialRollupAccountList 
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetGenericRollupAccountsList(AutoSearchInput input)
        {
            var query = from au in _accountUnitRepository.GetAll()
                        join coa in _coaUnitRepository.GetAll() on au.ChartOfAccountId equals coa.Id
                        select new { au, coa };
            var accounts = await query.Where(p => p.au.IsRollupAccount == true && p.coa.IsCorporate == true)
                            .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.au.Caption.Contains(input.Query))
                            .WhereIf(!ReferenceEquals(OrgnizationId, null), p => p.au.OrganizationUnitId == OrgnizationId)
                            .Select(u => new NameValueDto { Name = u.au.Caption, Value = u.au.Id.ToString() }).ToListAsync();

            return accounts;
        }


    }
}
