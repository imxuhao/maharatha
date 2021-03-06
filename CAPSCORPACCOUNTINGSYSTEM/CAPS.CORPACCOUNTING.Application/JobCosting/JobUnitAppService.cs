﻿using System;
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
using Abp.Collections.Extensions;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using Abp.Organizations;
using Abp.UI;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Runtime.Caching;
using AutoMapper;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Runtime.Session;
using CAPS.CORPACCOUNTING.JobCosting.CustomRepository;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobAppService
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects)]
    public class JobUnitAppService : CORPACCOUNTINGAppServiceBase, IJobUnitAppService
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
        private readonly ICacheManager _cacheManager;
        private readonly CustomAppSession _customAppSession;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<JobLocationUnit> _jobLocationRepository;
        private readonly DivisionCache _divisioncache;
        private readonly AccountCache _accountcache;
        private readonly CustomerCache _customercache;
        private readonly ICustomJobAccountRepository _jobAccountRepository;

        public JobUnitAppService(JobUnitManager jobUnitManager, IRepository<JobUnit> jobUnitRepository, IRepository<JobCommercialUnit> jobDetailUnitRepository,
            IRepository<EmployeeUnit> employeeUnitRepository, IRepository<CustomerUnit> customerUnitRepository, IJobCommercialAppService jobCommercialAppService,
            IRepository<OrganizationUnit, long> organizationUnitRepository, IRepository<JobAccountUnit, long> jobAccountUnitRepository,
            IRepository<CoaUnit> coaUnitRepository, IRepository<AccountUnit, long> accountUnitRepository, IRepository<ValueAddedTaxRecoveryUnit> valueAddedTaxRecoveryUnitRepository,
            IRepository<ValueAddedTaxTypeUnit> valueAddedTaxTypeUnitRepository, IRepository<TypeOfCountryUnit, short> typeOfCountryUnitRepository,
            IRepository<CountryUnit> countryUnitRepository, IJobAccountUnitAppService jobAccountUnitAppService, IRepository<TaxCreditUnit> taxCreditUnitRepository,
            ICacheManager cacheManager, CustomAppSession customAppSession, IUnitOfWorkManager unitOfWorkManager, IRepository<JobLocationUnit> jobLocationRepository,
            DivisionCache divisioncache, AccountCache accountcache, CustomerCache customercache,
            ICustomJobAccountRepository jobAccountRepository)
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
            _cacheManager = cacheManager;
            _customAppSession = customAppSession;
            _unitOfWorkManager = unitOfWorkManager;
            _jobLocationRepository = jobLocationRepository;
            _divisioncache = divisioncache;
            _accountcache = accountcache;
            _customercache = customercache;
            _jobAccountRepository = jobAccountRepository;
        }
        /// <summary>
        /// To create the Job.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Create)]
        public async Task<IdOutputDto<int>> CreateJobUnit(CreateJobUnitInput input)
        {

            //validating the  BudgetFormat(ChartofAccountId)
            if (input.ChartOfAccountId == null || input.ChartOfAccountId == 0)
            {
                throw new UserFriendlyException(L("BudgetFormatisRequired"));
            }
            CreateJobCommercialInput jobcommercialunit = new CreateJobCommercialInput
            {
                JobNumber = input.JobNumber,
                Caption = input.Caption,
                RollupCenterId = input.RollupCenterId,
                IsCorporateDefault = input.IsCorporateDefault,
                ChartOfAccountId = input.ChartOfAccountId,
                RollupAccountId = input.RollupAccountId,
                TypeOfCurrencyId = input.TypeOfCurrencyId,
                RollupJobId = input.RollupJobId,
                TypeOfJobStatusId = input.TypeOfJobStatusId,
                TypeOfBidSoftwareId = input.TypeOfBidSoftwareId,
                IsApproved = input.IsApproved,
                IsActive = input.IsActive,
                IsICTDivision = input.IsICTDivision,
                TypeofProjectId = input.TypeofProjectId,
                TaxRecoveryId = input.TaxRecoveryId
            };
            IdOutputDto<int> response = await _jobCommercialAppService.CreateJobDetailUnit(jobcommercialunit);
            await InsertJobAccounts(input.ChartOfAccountId.Value, response.Id);
            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {

            };
            await CurrentUnitOfWork.SaveChangesAsync();
            return response;

        }

        /// <summary>
        ///To update the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Edit)]
        [UnitOfWork]
        public async Task<IdOutputDto<int>> UpdateJobUnit(UpdateJobUnitInput input)
        {
            if (input.ChartOfAccountId == 0)
            {
                throw new UserFriendlyException(L("BudgetFormat is Required"));
            }
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
            jobUnit.TypeofProjectId = input.TypeofProjectId;
            jobUnit.TaxRecoveryId = input.TaxRecoveryId;
            jobUnit.ChartOfAccountId = input.ChartOfAccountId;
            jobUnit.RollupCenterId = input.RollupCenterId;
            #endregion


            await _jobUnitManager.UpdateAsync(jobUnit);


            //disable the SoftDelete Filter
            #region Adding the new lines to jobAccount
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                //get all jobaccounts and Lines 
                var jobaccountsList = (from lines in _accountUnitRepository.GetAll().Where(p => p.ChartOfAccountId == input.ChartOfAccountId)
                                       join jobacc in _jobAccountUnitRepository.GetAll() on lines.Id equals jobacc.AccountId into jobaccount
                                       from jobaccounts in jobaccount.DefaultIfEmpty()
                                       join rollUpAccount in _accountUnitRepository.GetAll() on lines.RollupAccountId equals rollUpAccount.Id into rollUpAccount
                                       from rollUpAccounts in rollUpAccount.DefaultIfEmpty()
                                       join rollUpDivision in _jobUnitRepository.GetAll().Where(u => u.IsDivision == true) on lines.RollupJobId equals rollUpDivision.Id into rollUpDivision
                                       from rollUpDivisions in rollUpDivision.DefaultIfEmpty()
                                       select new
                                       {
                                           lines,
                                           jobaccounts,
                                           rollUpAccountDescription = rollUpAccounts.Caption,
                                           rollUpDivisionDescription = rollUpDivisions.Caption
                                       }).ToList();
                //bulkinsertion
                foreach (var jobaccount in jobaccountsList)
                {
                    if (ReferenceEquals(jobaccount.jobaccounts, null) && !jobaccount.lines.IsDeleted)
                    {
                        CreateJobAccountUnitInput jobAccount = new CreateJobAccountUnitInput
                        {
                            JobId = input.JobId,
                            AccountId = jobaccount.lines.Id,
                            RollupAccountId = input.RollupAccountId,
                            RollupJobId = input.RollupJobId,
                            Description = jobaccount.lines.Caption,
                            RollupJobDescription = jobaccount.rollUpDivisionDescription,
                            RollupAccountDescription = jobaccount.rollUpAccountDescription,
                        };

                        await _jobAccountUnitAppService.CreateJobAccountUnit(jobAccount);

                    }
                }

            }
            #endregion



            #region updating all JobAccounts of Job
            //bulk update
            if (!ReferenceEquals(input.JobAccountList, null))
            {
                foreach (var jobAccounts in input.JobAccountList)
                {
                    await _jobAccountUnitAppService.UpdateJobAccountUnit(jobAccounts);
                }
            }
            IdOutputDto<int> responseDto = new IdOutputDto<int>
            {
                JobId = jobUnit.Id
            };
            return responseDto;

            #endregion
        }

        /// <summary>
        /// To Delete the Job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Delete)]
        public async Task DeleteJobUnit(IdInputExtensionDto input)
        {
            await _jobLocationRepository.DeleteAsync(p => p.JobId == input.Id);
            await _jobAccountUnitRepository.DeleteAsync(p => p.JobId == input.Id);
            await _jobUnitManager.DeleteAsync(input.Id);
        }
        /// <summary>
        /// To Get the Job with JobDetails to show in the Grid With searching and sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Projects_ProjectMaintenance_Projects)]
        public async Task<PagedResultOutput<JobCommercialUnitDto>> GetJobUnits(SearchInputDto input)
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
            query = query.Where(item => item.Job.IsDivision == false);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Job.JobNumber ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<JobCommercialUnitDto>(resultCount, results.Select(item =>
            {
                var dto = new JobCommercialUnitDto()
                {
                    JobId = item.Job.Id,
                    BidDate = item.Job.BidDate,
                    AwardDate = item.Job.AwardDate,
                    ShootingDate = item.Job.ShootingDate,
                    WrapDate = item.Job.WrapDate,
                    RoughCutDate = item.Job.RoughCutDate,
                    AirDate = item.Job.AirDate,
                    DateClosed = item.Job.DateClosed,
                    FinalShootDate = item.Job.FinalShootDate,
                    ProductOwner = item.Job.ProductOwner,
                    ProductName = item.Job.ProductName,
                    ExecutiveProducerId = item.Job.ExecutiveProducerId,
                    DirectorEmployeeId = item.Job.DirectorEmployeeId,
                    ProducerEmployeeId = item.Job.ProducerEmployeeId,

                    SetDesignerEmployeeId = item.Job.SetDesignerEmployeeId,
                    EditorEmployeeId = item.Job.EditorEmployeeId,
                    ArtDirectorEmployeeId = item.Job.ArtDirectorEmployeeId,
                    SalesRepId = item.Job.SalesRepId,
                    AgencyId = item.Job.AgencyId,
                    AgencyClientCustomerId = item.Job.AgencyClientCustomerId,
                    ThirdPartyCustomerId = item.Job.ThirdPartyCustomerId,
                    AgencyProducer = item.Job.AgencyProducer,
                    AgencyProducerContactInfo = item.Job.AgencyProducerContactInfo,
                    AgencyArtDirector = item.Job.AgencyArtDirector,
                    AgencyArtDirContactInfo = item.Job.AgencyArtDirContactInfo,
                    AgencyWriter = item.Job.AgencyWriter,
                    AgencyBusinessManager = item.Job.AgencyBusinessManager,
                    AgencyBusMgrContactInfo = item.Job.AgencyBusMgrContactInfo,
                    AgencyJobNumber = item.Job.AgencyJobNumber,
                    AgencyPONumber = item.Job.AgencyPONumber,
                    AgencyName = item.Job.AgencyName,
                    AgencyAddress = item.Job.AgencyAddress,
                    AgencyPhone = item.Job.AgencyPhone,
                    CommercialTitle1 = item.Job.CommercialTitle1,
                    CommercialTitle2 = item.Job.CommercialTitle2,
                    CommercialTitle3 = item.Job.CommercialTitle3,

                    JobNumber = item.Job.JobNumber,
                    Caption = item.Job.Caption,
                    RollupCenterId = item.Job.RollupCenterId,
                    IsCorporateDefault = item.Job.IsCorporateDefault,
                    ChartOfAccountId = item.Job.ChartOfAccountId,
                    RollupAccountId = item.Job.RollupAccountId,
                    TypeOfCurrencyId = item.Job.TypeOfCurrencyId,
                    RollupJobId = item.Job.RollupJobId,
                    TypeOfJobStatusId = item.Job.TypeOfJobStatusId,
                    TypeOfBidSoftwareId = item.Job.TypeOfBidSoftwareId,
                    IsActive = item.Job.IsActive,
                    IsApproved = item.Job.IsApproved,
                    IsICTDivision = item.Job.IsICTDivision,
                    TypeofProjectId = item.Job.TypeofProjectId,
                    TaxRecoveryId = item.Job.TaxRecoveryId

                };

                if (item.DirectorName != null)
                    dto.DirectorName = item.DirectorName;
                if (item.Agency != null)
                    dto.Agency = item.Agency;
                dto.JobStatusName = item.Job.TypeOfJobStatusId != null ? item.Job.TypeOfJobStatusId.ToDisplayName() : "";
                dto.TypeofProjectName = item.Job.TypeofProjectId != null ? item.Job.TypeofProjectId.ToDisplayName() : "";
                return dto;
            }).ToList());
        }
        public async Task<JobCommercialUnitDto> GetJobUnitById(IdInputExtensionDto<bool, int> input)
        {
            if (input.Value)
            {
                var jobitem = await _jobUnitRepository.GetAsync(input.Id);
                //Mapper.CreateMap<JobUnit, JobCommercialUnitDto>()
                //    .ForMember(u => u.JobId, ap => ap.MapFrom(src => src.Id));
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<JobUnit, JobCommercialUnitDto>().ForMember(u => u.JobId, ap => ap.MapFrom(src => src.Id));
                });

                JobCommercialUnitDto result = new JobCommercialUnitDto();
                Mapper.Map(jobitem, result);
                return result;

            }
            else
            {
                var jobitem = await _jobDetailUnitRepository.GetAsync(input.Id);
                JobCommercialUnitDto result = jobitem.MapTo<JobCommercialUnitDto>();
                result.JobId = jobitem.Id;
                return result;

            }

        }

        public async Task<List<NameValueDto>> GetOrganizationUnits(IdInput input)
        {
            var organizations = await _organizationUnitRepository.GetAll().Where(p => p.Id != input.Id)
                .Select(u => new NameValueDto { Name = u.DisplayName, Value = u.Id.ToString() }).ToListAsync();
            return organizations;
        }
        /// <summary>
        /// Get DivisionsList
        /// </summary>
        /// <returns></returns>
        public async Task<List<DivisionCacheItem>> GetDivisionList(AutoSearchInput input)
        {
            var cacheItem = await _divisioncache.GetDivisionCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(_customAppSession.TenantId)));
            return cacheItem.ToList().Where(p => p.IsDivision == true)
                .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.JobNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) ||
            p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

        public async Task<List<NameValueDto>> GetProjectCoaList(AutoSearchInput input)
        {
            var divisions = await _coaUnitRepository.GetAll().Where(p => p.IsCorporate == false)
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
        public async Task<List<AccountCacheItem>> GetRollupAccountList(AutoSearchInput input)
        {
            var accountList = await _accountcache.GetAccountCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId)));

            return accountList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.IsCorporate == true && p.IsRollupAccount == true).ToList();
        }

        /// <summary>
        /// Get JobAccounts by CoaId and JobId
        /// </summary>
        /// <returns></returns>
        public async Task<List<JobAccountUnitDto>> GetLineListByProjectCoa(GetJobAccountInputDto input)
        {
            var accounts = await (from jobaccount in _jobAccountUnitRepository.GetAll()
                                  join account in _accountUnitRepository.GetAll() on jobaccount.AccountId equals account.Id
                                  join rollUpAccount in _accountUnitRepository.GetAll() on jobaccount.RollupAccountId equals rollUpAccount.Id into rollUpAccount
                                  from rollUpAccounts in rollUpAccount.DefaultIfEmpty()
                                  join rollUpDivision in _jobUnitRepository.GetAll().Where(u => u.IsDivision == true) on jobaccount.RollupJobId equals rollUpDivision.Id into rollUpDivision
                                  from rollUpDivisions in rollUpDivision.DefaultIfEmpty()
                                  where jobaccount.JobId == input.JobId && account.ChartOfAccountId == input.ChartofAccountId
                                  select new
                                  {
                                      jobaccount,
                                      accountNumber = account.AccountNumber,
                                      rollUpAccountNumber = rollUpAccounts.AccountNumber,
                                      rollUpDivisionNumber = rollUpDivisions.JobNumber
                                  }).ToListAsync();
            return accounts.Select(
                result =>
                {
                    var dto = result.jobaccount.MapTo<JobAccountUnitDto>();
                    dto.JobAccountId = result.jobaccount.Id;
                    dto.AccountNumber = result.accountNumber;
                    dto.RollupAccountNumber = result.rollUpAccountNumber;
                    dto.RollupJobNumber = result.rollUpDivisionNumber;
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
                                  //join typeofcountry in _typeOfCountryUnitRepository.GetAll() on taxtype.TypeOfCountryId equals typeofcountry.Id
                                  join country in _countryUnitRepository.GetAll() on taxtype.CountryID equals country.Id
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
        public async Task<List<AutoFillDto>> GetTaxCreditList(AutoSearchInput input)
        {
            var taxCreditList = await _taxCreditUnitRepository.GetAll()
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query) || p.Number.Contains(input.Query))
                 .Select(u => new AutoFillDto { Name = u.Description, Value = u.Id.ToString(), Column1 = u.Number }).OrderBy(p=>p.Name).ToListAsync();
            return taxCreditList;
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<CustomerCacheItem>> GetCustomersList(AutoSearchInput input)
        {
            var customerList = await _customercache.GetCustomersCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.CustomerKey, Convert.ToInt32(_customAppSession.TenantId)));

            return customerList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.LastName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.FirstName.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.CustomerNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).ToList();
        }

        /// <summary>
        /// BulkInsert of Jobs
        /// </summary>
        /// <param name="listJobUnitDtos"></param>
        /// <returns></returns>

        public async Task<List<JobCommercialUnitDto>> BulkJobInsert(CreateJobListInput listJobUnitDtos)
        {
            var createJobList = listJobUnitDtos.JobList.Select((item, index) => { item.ExcelRowNumber = index; return item; }).ToList();
            var errorjobList = await ValidateDuplicateRecords(createJobList);
            var jobs = listJobUnitDtos.JobList.Where(p => errorjobList.All(p2 => p2.JobNumber != p.JobNumber)).ToList();
            foreach (var jobUnit in jobs)
            {
                var job = jobUnit.MapTo<JobCommercialUnit>();
                job.IsDivision = false;
                await InsertJobAccounts(job.ChartOfAccountId, await _jobDetailUnitRepository.InsertAndGetIdAsync(job));
            }
            return errorjobList;
        }

        /// <summary>
        /// BulkInsert JobAccount
        /// </summary>
        /// <param name="coaId"></param>
        /// <param name="jobId"></param>
        /// <returns></returns>
        private async Task InsertJobAccounts(int coaId, int jobId)
        {
            List<CreateJobAccountUnitInput> jobAccounts = await (from account in _accountUnitRepository.GetAll()
                                                                 join rollUpAccount in _accountUnitRepository.GetAll() on account.RollupAccountId equals rollUpAccount.Id into rollUpAccount
                                                                 from rollUpAccounts in rollUpAccount.DefaultIfEmpty()
                                                                 join rollUpDivision in _jobUnitRepository.GetAll().Where(u => u.IsDivision == true) on account.RollupJobId equals rollUpDivision.Id into rollUpDivision
                                                                 from rollUpDivisions in rollUpDivision.DefaultIfEmpty()
                                                                 where account.ChartOfAccountId == coaId
                                                                 select new CreateJobAccountUnitInput
                                                                 {
                                                                     JobId = jobId,
                                                                     AccountId = account.Id,
                                                                     RollupAccountId = account.RollupAccountId,
                                                                     RollupJobId = account.RollupJobId == 0 ? null : account.RollupJobId,
                                                                     Description = account.Caption,
                                                                     RollupAccountDescription = rollUpAccounts.Caption,
                                                                     RollupJobDescription = rollUpDivisions.Caption
                                                                 }).ToListAsync();


            var jobAccountList = jobAccounts.Select(accountunitDto => accountunitDto.MapTo<JobAccountUnit>()).ToList();
            jobAccountList.ForEach(x =>
            {
                x.TenantId = AbpSession.GetTenantId();
                x.CreatorUserId = AbpSession.GetUserId();
            });
            await _jobAccountRepository.BulkInsertJobAccountUnits(jobAccountList);
        }

        /// <summary>
        /// Checking DuplicateRecords
        /// </summary>
        /// <param name="jobsList"></param>
        /// <returns></returns>
        private async Task<List<JobCommercialUnitDto>> ValidateDuplicateRecords(List<CreateJobCommercialInput> jobsList)
        {
            var jobNumberList = string.Join(",", jobsList.Select(p => p.JobNumber).ToArray());
            //  var descriptionList = string.Join(",", jobsList.Select(p => p.Caption).ToArray());

            var duplicatejobs = await _divisioncache.GetDivisionCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(AbpSession.GetTenantId())));
            var duplicatejobItems = duplicatejobs.Where(p => p.IsDivision == false).ToList();

            var duplicatejobList =
                duplicatejobItems.Where(
                    p => jobNumberList.Contains(p.JobNumber)).ToList();


            //duplicateJobNumbers of JobList
            var duplicatejobsjobNumberList = (from p in jobsList
                                              join p2 in duplicatejobList on p.JobNumber equals p2.JobNumber
                                              select new { jobNumber = p.JobNumber, RowNumber = p.ExcelRowNumber, ErrorMesage = L("DuplicateJobNumber") + p.JobNumber }).ToList();
            //Get Joblist of unassigned BudgetFormat.
            var requiredcoaJobList = (from p in jobsList
                                      where (p.ChartOfAccountId == 0 || p.ChartOfAccountId == null)
                                      select new { jobNumber = string.Empty, RowNumber = p.ExcelRowNumber, ErrorMesage = L("BudgetFormatRequired") }).ToList();

            var jobUnits = (from job in jobsList
                            join duplicatenum in duplicatejobsjobNumberList
                           on job.ExcelRowNumber equals duplicatenum.RowNumber
                                                 into duplicatejobnumber
                            from duplicatejobnumberunit in duplicatejobnumber.DefaultIfEmpty()

                            join requiredcoajob in requiredcoaJobList
                          on job.ExcelRowNumber equals requiredcoajob.RowNumber
                                                into requiredcoajobs
                            from requiredcoajobunit in requiredcoajobs.DefaultIfEmpty()
                            select new
                            {
                                job,
                                ErrorMesage =
                                    (!ReferenceEquals(duplicatejobnumberunit, null) ? duplicatejobnumberunit.ErrorMesage : "") +
                                    (!ReferenceEquals(requiredcoajobunit, null) ? requiredcoajobunit.ErrorMesage : "")

                            }).Distinct().ToList();

            //invalid JobList
            var errorjobs = jobUnits.Where(u => u.ErrorMesage.Trim().Length > 0).ToList();


            return errorjobs.Select(job => new JobCommercialUnitDto
            {
                JobId = job.job.JobId,
                BidDate = job.job.BidDate,
                AwardDate = job.job.AwardDate,
                ShootingDate = job.job.ShootingDate,
                WrapDate = job.job.WrapDate,
                RoughCutDate = job.job.RoughCutDate,
                AirDate = job.job.AirDate,
                DateClosed = job.job.DateClosed,
                FinalShootDate = job.job.FinalShootDate,
                ProductOwner = job.job.ProductOwner,
                ProductName = job.job.ProductName,
                ExecutiveProducerId = job.job.ExecutiveProducerId,
                DirectorEmployeeId = job.job.DirectorEmployeeId,
                ProducerEmployeeId = job.job.ProducerEmployeeId,
                SetDesignerEmployeeId = job.job.SetDesignerEmployeeId,
                EditorEmployeeId = job.job.EditorEmployeeId,
                ArtDirectorEmployeeId = job.job.ArtDirectorEmployeeId,
                SalesRepId = job.job.SalesRepId,
                AgencyId = job.job.AgencyId,
                AgencyClientCustomerId = job.job.AgencyClientCustomerId,
                ThirdPartyCustomerId = job.job.ThirdPartyCustomerId,
                AgencyProducer = job.job.AgencyProducer,
                AgencyProducerContactInfo = job.job.AgencyProducerContactInfo,
                AgencyArtDirector = job.job.AgencyArtDirector,
                AgencyArtDirContactInfo = job.job.AgencyArtDirContactInfo,
                AgencyWriter = job.job.AgencyWriter,
                AgencyBusinessManager = job.job.AgencyBusinessManager,
                AgencyBusMgrContactInfo = job.job.AgencyBusMgrContactInfo,
                AgencyJobNumber = job.job.AgencyJobNumber,
                AgencyPONumber = job.job.AgencyPONumber,
                AgencyName = job.job.AgencyName,
                AgencyAddress = job.job.AgencyAddress,
                AgencyPhone = job.job.AgencyPhone,
                CommercialTitle1 = job.job.CommercialTitle1,
                CommercialTitle2 = job.job.CommercialTitle2,
                CommercialTitle3 = job.job.CommercialTitle3,
                JobNumber = job.job.JobNumber,
                Caption = job.job.Caption,
                RollupCenterId = job.job.RollupCenterId,
                IsCorporateDefault = job.job.IsCorporateDefault,
                ChartOfAccountId = job.job.ChartOfAccountId.Value,
                RollupAccountId = job.job.RollupAccountId,
                TypeOfCurrencyId = job.job.TypeOfCurrencyId,
                RollupJobId = job.job.RollupJobId,
                TypeOfJobStatusId = job.job.TypeOfJobStatusId,
                TypeOfBidSoftwareId = job.job.TypeOfBidSoftwareId,
                IsActive = job.job.IsActive,
                IsApproved = job.job.IsApproved,
                IsICTDivision = job.job.IsICTDivision,
                TypeofProjectId = job.job.TypeofProjectId,
                TaxRecoveryId = job.job.TaxRecoveryId,
                DivisionJobNumber = job.job.DivisionJobNumber,
                JobStatusName = job.job.DivisionJobNumber,
                TypeOfCurrency = job.job.TypeOfCurrency,
                TypeofProjectName = job.job.TypeofProjectName,
                BudgetFormatCaption = job.job.BudgetFormatCaption,
                ErrorMessage = job.ErrorMesage.TrimEnd(',').TrimStart(','),
                TaxCreditName = job.job.TaxCreditName

            }).ToList();
        }
    }
}
