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
using Abp.UI;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class JobUnitAppService : CORPACCOUNTINGServiceBase, IJobUnitAppService
    {
        private readonly JobUnitManager _jobUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<JobCommercialUnit> _jobDetailUnitRepository;
        private readonly IRepository<EmployeeUnit> _employeeUnitRepository;
        private readonly IRepository<CustomerUnit> _customerUnitRepository;
        private readonly IJobCommercialAppService _jobCommercialAppService;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<JobAccountUnit, long> _jobAccountUnitRepository;
        private readonly IRepository<ValueAddedTaxRecoveryUnit> _valueAddedTaxRecoveryUnitRepository;
        private readonly IRepository<ValueAddedTaxTypeUnit> _valueAddedTaxTypeUnitRepository;
        private readonly IRepository<TypeOfCountryUnit, short> _typeOfCountryUnitRepository;
        private readonly IRepository<CountryUnit> _countryUnitRepository;
        private readonly IJobAccountUnitAppService _jobAccountUnitAppService;
        private readonly IRepository<TaxCreditUnit> _taxCreditUnitRepository;

        public JobUnitAppService(JobUnitManager jobUnitManager, IRepository<JobUnit> jobUnitRepository, IRepository<JobCommercialUnit> jobDetailUnitRepository,
            IRepository<EmployeeUnit> employeeUnitRepository, IRepository<CustomerUnit> customerUnitRepository, IJobCommercialAppService jobCommercialAppService,
            IRepository<OrganizationUnit, long> organizationUnitRepository, IRepository<JobAccountUnit, long> jobAccountUnitRepository,
            IRepository<CoaUnit> coaUnitRepository, IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<ValueAddedTaxRecoveryUnit> valueAddedTaxRecoveryUnitRepository,
        IRepository<ValueAddedTaxTypeUnit> valueAddedTaxTypeUnitRepository,
        IRepository<TypeOfCountryUnit, short> typeOfCountryUnitRepository,
        IRepository<CountryUnit> countryUnitRepository, IJobAccountUnitAppService jobAccountUnitAppService, IRepository<TaxCreditUnit> taxCreditUnitRepository)
        {
            _jobUnitManager = jobUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _jobDetailUnitRepository = jobDetailUnitRepository;
            _employeeUnitRepository = employeeUnitRepository;
            _customerUnitRepository = customerUnitRepository;
            _jobCommercialAppService = jobCommercialAppService;
            _organizationUnitRepository = organizationUnitRepository;
            _coaUnitRepository = coaUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _jobAccountUnitRepository = jobAccountUnitRepository;
            _valueAddedTaxRecoveryUnitRepository = valueAddedTaxRecoveryUnitRepository;
            _valueAddedTaxTypeUnitRepository = valueAddedTaxTypeUnitRepository;
            _typeOfCountryUnitRepository = typeOfCountryUnitRepository;
            _countryUnitRepository = countryUnitRepository;
            _jobAccountUnitAppService = jobAccountUnitAppService;
            _taxCreditUnitRepository = taxCreditUnitRepository;

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
            //validating the  BudgetFormat(ChartofAccountId)
            if (ReferenceEquals(input.ChartOfAccountId, null))
            {
                throw new UserFriendlyException(L("BudgetFormatisRequired"));
            }
            CreateJobCommercialInput jobcommercialunit = new CreateJobCommercialInput();
            jobcommercialunit.JobNumber = input.JobNumber;
            jobcommercialunit.Caption = input.Caption;
            jobcommercialunit.RollupCenterId = input.RollupCenterId;
            jobcommercialunit.IsCorporateDefault = input.IsCorporateDefault;
            jobcommercialunit.ChartOfAccountId = input.ChartOfAccountId;
            jobcommercialunit.RollupAccountId = input.RollupAccountId;
            jobcommercialunit.TypeOfCurrencyId = input.TypeOfCurrencyId;
            jobcommercialunit.RollupJobId = input.RollupJobId;
            jobcommercialunit.TypeOfJobStatusId = input.TypeOfJobStatusId;
            jobcommercialunit.TypeOfBidSoftwareId = input.TypeOfBidSoftwareId;
            jobcommercialunit.IsApproved = input.IsApproved;
            jobcommercialunit.IsActive = input.IsActive;
            jobcommercialunit.IsICTDivision = input.IsICTDivision;
            jobcommercialunit.OrganizationUnitId = input.OrganizationUnitId;
            jobcommercialunit.TypeofProjectId = input.TypeofProjectId;
            jobcommercialunit.TaxRecoveryId = input.TaxRecoveryId;
            JobCommercialUnitDto result = await _jobCommercialAppService.CreateJobDetailUnit(jobcommercialunit);

            //Get the accounts of appropriate coa and constructing CreateJobAccountUnitInput
            List<CreateJobAccountUnitInput> jobAccounts = await (from account in _accountUnitRepository.GetAll()
                                                                 where account.ChartOfAccountId == input.ChartOfAccountId
                                                                 select new CreateJobAccountUnitInput
                                                                 {
                                                                     JobId = result.JobId,
                                                                     AccountId = account.Id,
                                                                     OrganizationUnitId = input.OrganizationUnitId,
                                                                     Description = account.Caption
                                                                 }).ToListAsync();
            #region Inserting JobId,AccountId,Description in JobAccount Table

            foreach (var jobAccount in jobAccounts)
            {
                await _jobAccountUnitAppService.CreateJobAccountUnit(jobAccount);
            }
            #endregion
            return result.MapTo<JobUnitDto>();
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
            jobUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobUnit.TypeofProjectId = input.TypeofProjectId;
            jobUnit.TaxRecoveryId = input.TaxRecoveryId;
            jobUnit.ChartOfAccountId = input.ChartOfAccountId;
            jobUnit.RollupCenterId = input.RollupCenterId;
            #endregion

            await _jobUnitManager.UpdateAsync(jobUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            #region updating all JobAccounts of Job
            if (!ReferenceEquals(input.JobAccountList, null))
            {
                foreach (var jobAccounts in input.JobAccountList)
                {
                    await _jobAccountUnitAppService.UpdateJobAccountUnit(jobAccounts);
                }
            }
            #endregion
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
            var query = from job in _jobDetailUnitRepository.GetAll()
                        join emp in _employeeUnitRepository.GetAll() on job.DirectorEmployeeId equals emp.Id
                                 into employee
                        from em in employee.DefaultIfEmpty()
                        join cust in _customerUnitRepository.GetAll() on job.AgencyId equals cust.Id
                                into tempcust
                        from cs in tempcust.DefaultIfEmpty()
                        select new { Job = job, DirectorName = em.LastName, Agency = cs.LastName };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), item => item.Job.OrganizationUnitId == input.OrganizationUnitId)
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
            var divisions = await query
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.account.Caption.Contains(input.Query))
                 .WhereIf(!ReferenceEquals(input.OrganizationId,null), p => p.account.OrganizationUnitId==input.OrganizationId)
                 .Select(u => new NameValueDto { Name = u.account.Caption, Value = u.account.Id.ToString() }).ToListAsync();
            return divisions;

        }

        public async Task<List<NameValueDto>> GetProjectCoaList(AutoSearchInput input)
        {
            var divisions = await _coaUnitRepository.GetAll().Where(p => p.IsCorporate == false)
                 .WhereIf(!ReferenceEquals(input.OrganizationId, null), p => p.OrganizationUnitId == input.OrganizationId)
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
                .WhereIf(!ReferenceEquals(input.OrganizationId, null), p => p.OrganizationUnitId == input.OrganizationId)
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
                             .WhereIf(!ReferenceEquals(input.OrganizationId, null), p => p.au.OrganizationUnitId == input.OrganizationId)
                            .Select(u => new NameValueDto { Name = u.au.Caption, Value = u.au.Id.ToString() }).ToListAsync();

            return accounts;
        }
        /// <summary>
        /// Get JobAccounts by CoaId and JobId
        /// </summary>
        /// <returns></returns>
        public async Task<List<JobAccountUnitDto>> GetLineListByProjectCoa(GetJobAccountInputDto input)
        {
            var accounts = await (from jobaccount in _jobAccountUnitRepository.GetAll()
                                  join account in _accountUnitRepository.GetAll() on jobaccount.AccountId equals account.Id
                                  where jobaccount.JobId == input.JobId && account.ChartOfAccountId == input.ChartofAccountId
                                  select new { jobaccount, account }).ToListAsync();
            return accounts.Select(
                result =>
                {
                    var dto = result.jobaccount.MapTo<JobAccountUnitDto>();
                    dto.JobAccountId = result.jobaccount.Id;
                    dto.AccountNumber = result.account.AccountNumber;
                    return dto;
                }).ToList();

        }
        /// <summary>
        /// Get TaxRecovery
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTaxRecovery()
        {
            var accounts = await (from valuetaxrecovery in _valueAddedTaxRecoveryUnitRepository.GetAll()
                                  join taxtype in _valueAddedTaxTypeUnitRepository.GetAll() on valuetaxrecovery.ValueAddedTaxTypeId equals taxtype.Id
                                  join typeofcountry in _typeOfCountryUnitRepository.GetAll() on taxtype.TypeOfCountryId equals typeofcountry.Id
                                  join country in _countryUnitRepository.GetAll() on typeofcountry.Id equals country.TypeOfCountryId
                                  select new { valuetaxrecovery }).ToListAsync();
            return accounts.Select(
                result =>
                {
                    NameValueDto dto = new NameValueDto();
                    dto.Name = result.valuetaxrecovery.TypeOfVatRecoveryId.ToDisplayName();
                    dto.Value = result.valuetaxrecovery.Id.ToString();
                    return dto;
                }).ToList();
        }
        /// <summary>
        /// Get TaxCreditList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetTaxCreditList(AutoSearchInput input)
        {
            var taxCreditList = await _taxCreditUnitRepository.GetAll()
                 .WhereIf(!ReferenceEquals(input.OrganizationId, null), p => p.OrganizationUnitId == input.OrganizationId)
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
                 .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return taxCreditList;
        }

    }
}
