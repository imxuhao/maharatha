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
using Abp.UI;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Authorization;


namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class DivisionUnitAppService : CORPACCOUNTINGServiceBase, IDivisionUnitAppService
    {
        private readonly JobUnitManager _jobUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        public DivisionUnitAppService(JobUnitManager jobUnitManager, IRepository<JobUnit> jobUnitRepository,
            IRepository<CoaUnit> coaUnitRepository)
        {   
            _jobUnitManager = jobUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _coaUnitRepository = coaUnitRepository;
        }
        /// <summary>
        /// To create the Division
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions_Create)]
        public async Task<JobUnitDto> CreateDivisionUnit(CreateJobUnitInput input)
        {
            var chartofaccount = 
                _coaUnitRepository.FirstOrDefault(
                    p => p.IsCorporate == true && p.OrganizationUnitId == input.OrganizationUnitId);
            if (ReferenceEquals(chartofaccount, null))
            {
                throw new UserFriendlyException(L("Pleasesetupchartofaccount"));
            }
            var jobUnit = new JobUnit(jobnumber: input.JobNumber, caption: input.Caption, iscorporatedefault: input.IsCorporateDefault, rollupaccountid: input.RollupAccountId,
                typeofcurrencyid: input.TypeOfCurrencyId, rollupjobid: input.RollupJobId, typeofjobstatusid: input.TypeOfJobStatusId, typeofbidsoftwareid: input.TypeOfBidSoftwareId,
                isapproved: input.IsApproved, isactive: input.IsActive, isictdivision: input.IsICTDivision, organizationunitid:input.OrganizationUnitId, typeofprojectid: input.TypeofProjectId,
                taxrecoveryid: input.TaxRecoveryId, chartofaccountid: chartofaccount.Id, rollupcenterid: input.RollupCenterId,isdivision:true, taxcreditid:input.TaxCreditId);
            await _jobUnitManager.CreateAsync(jobUnit);
            await CurrentUnitOfWork.SaveChangesAsync();            
            return jobUnit.MapTo<JobUnitDto>();
        }

        /// <summary>
        ///To update the Division
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions_Edit)]
        public async Task<JobUnitDto> UpdateDivisionUnit(UpdateJobUnitInput input)
        {
            var jobUnit = await _jobUnitRepository.GetAsync(input.JobId);            
            #region Setting the values to be updated

            jobUnit.JobNumber = input.JobNumber;
            jobUnit.Caption = input.Caption;            
            jobUnit.TypeOfCurrencyId = input.TypeOfCurrencyId;           
            jobUnit.IsActive = input.IsActive;          
            jobUnit.OrganizationUnitId = input.OrganizationUnitId;
            jobUnit.IsDivision = true;         

            #endregion

            await _jobUnitManager.UpdateAsync(jobUnit);

            await CurrentUnitOfWork.SaveChangesAsync();
           
            return jobUnit.MapTo<JobUnitDto>();
        }

        /// <summary>
        /// To Delete the Division
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>g
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions_Delete)]
        public async Task DeleteDivisionUnit(IdInput input)
        {           
            await _jobUnitManager.DeleteAsync(input.Id);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions)]
        public async Task<PagedResultOutput<JobUnitDto>> GetDivisionUnits(SearchInputDto input)
        {
            var query = from job in _jobUnitRepository.GetAll()                       
                        select new { Job = job };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), item => item.Job.OrganizationUnitId==input.OrganizationUnitId )
                .Where(item=>item.Job.IsDivision==true);
                

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
                return dto;
            }).ToList());
        }
    }
}
