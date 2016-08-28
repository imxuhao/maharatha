using System;
using System.Collections.Generic;
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
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Abp.UI;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.JobCosting.CustomRepository;
using CAPS.CORPACCOUNTING.Masters.Dto;


namespace CAPS.CORPACCOUNTING.JobCosting
{
    [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions)] ///This is to ensure only logged in user has access to this module.
    public class DivisionUnitAppService : CORPACCOUNTINGAppServiceBase, IDivisionUnitAppService
    {
        private readonly JobUnitManager _jobUnitManager;
        private readonly IRepository<JobUnit> _jobUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IRepository<TypeOfCurrencyUnit,short> _typeOfCurrencyUnitRepository;
        private readonly DivisionCache _divisioncache;
        private readonly ICustomDivisionRepository _customDivisionRepository;
        private readonly ICacheManager _cacheManager;
        public DivisionUnitAppService(JobUnitManager jobUnitManager, IRepository<JobUnit> jobUnitRepository,
            IRepository<CoaUnit> coaUnitRepository,
            IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyUnitRepository, DivisionCache divisioncache,
            ICustomDivisionRepository customDivisionRepository, ICacheManager cacheManager)
        {   
            _jobUnitManager = jobUnitManager;
            _jobUnitRepository = jobUnitRepository;
            _coaUnitRepository = coaUnitRepository;
            _typeOfCurrencyUnitRepository=typeOfCurrencyUnitRepository;
            _divisioncache = divisioncache;
            _customDivisionRepository = customDivisionRepository;
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// To create the Division
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions_Create)]
        public async Task<IdOutputDto<int>> CreateDivisionUnit(CreateJobUnitInput input)
        {
            var chartofaccount = _coaUnitRepository.FirstOrDefault(p => p.IsCorporate == true );
            if (ReferenceEquals(chartofaccount, null))
            {
                throw new UserFriendlyException(L("Please setup chartofaccount"));
            }
            var jobUnit = new JobUnit(jobnumber: input.JobNumber, caption: input.Caption, iscorporatedefault: input.IsCorporateDefault, rollupaccountid: input.RollupAccountId,
                typeofcurrencyid: input.TypeOfCurrencyId, rollupjobid: input.RollupJobId, typeofjobstatusid: input.TypeOfJobStatusId, typeofbidsoftwareid: input.TypeOfBidSoftwareId,
                isapproved: input.IsApproved, isactive: input.IsActive, isictdivision: input.IsICTDivision, organizationunitid:input.OrganizationUnitId, typeofprojectid: input.TypeofProjectId,
                taxrecoveryid: input.TaxRecoveryId, chartofaccountid: chartofaccount.Id, rollupcenterid: input.RollupCenterId,isdivision:true, taxcreditid:input.TaxCreditId);
            IdOutputDto<int> response = new IdOutputDto<int>()
            {
                JobId = await _jobUnitManager.CreateAsync(jobUnit)
            };
            return response;
        }

        /// <summary>
        ///To update the Division
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_Divisions_Edit)]
        public async Task<IdOutputDto<int>> UpdateDivisionUnit(UpdateJobUnitInput input)
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

            IdOutputDto<int> response = new IdOutputDto<int>()
            {
                JobId = jobUnit.Id
            };

            return response;
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
                        join   currency in _typeOfCurrencyUnitRepository.GetAll() on job.TypeOfCurrencyId equals currency.Id
                        into currency
                        from currencyData in currency.DefaultIfEmpty()
                        select new { Job = job,currency= currencyData.Description };
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
                dto.TypeOfCurrency = item.currency;
                return dto;
            }).ToList());
        }
        /// <summary>
        /// BulkInsert of Divisions
        /// </summary>
        /// <param name="listDivisionUnitDtos"></param>
        /// <returns></returns>

        public async Task<List<JobUnitDto>> BulkDivisionInsert(CreateDivisionListInput listDivisionUnitDtos)
        {
            List<JobUnit> divisionList = new List<JobUnit>();
            var chartofaccount = _coaUnitRepository.FirstOrDefault(p => p.IsCorporate == true);
            if (ReferenceEquals(chartofaccount, null))
            {
                throw new UserFriendlyException(L("Please setup chartofaccount"));
            }
            var createDividionList = listDivisionUnitDtos.DivisionList.Select((item, index) => { item.ExcelRowNumber = index; return item; }).ToList();
            var errorjobList = await ValidateDuplicateRecords(createDividionList);
            var divisions = listDivisionUnitDtos.DivisionList.Where(p => errorjobList.All(p2 => p2.JobNumber != p.JobNumber)).ToList();
            foreach (var accountUnit in divisions)
            {
                var account = accountUnit.MapTo<JobUnit>();
                account.ChartOfAccountId = chartofaccount.Id;
                account.TenantId = AbpSession.GetTenantId();
                account.IsDivision = true;
                account.CreatorUserId = AbpSession.GetUserId();
                divisionList.Add(account);
            }
            if (divisionList.Count > 0)
            {
                await _customDivisionRepository.BulkInsertDivisionUnits(divisionList: divisionList);
                _cacheManager.GetCacheItem(CacheStoreName: CacheKeyStores.CacheDivisionStore).Remove(CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(AbpSession.GetTenantId())));
            }
            return errorjobList;
        }


        /// <summary>
        /// Checking DuplicateRecords
        /// </summary>
        /// <param name="divisionsList"></param>
        /// <returns></returns>
        private async Task<List<JobUnitDto>> ValidateDuplicateRecords(List<CreateJobUnitInput> divisionsList)
        {
            var divisionunitDtoList = new List<JobUnitDto>();
            var divisionNumberList = string.Join(",", divisionsList.Select(p => p.JobNumber).ToArray());
            var descriptionList = string.Join(",", divisionsList.Select(p => p.Caption).ToArray());

            var duplicatedivisions = await _divisioncache.GetDivisionCacheItemAsync(
                CacheKeyStores.CalculateCacheKey(CacheKeyStores.DivisionKey, Convert.ToInt32(AbpSession.GetTenantId())));
            var duplicatedivisionItems = duplicatedivisions.Where(p => p.IsDivision == true).ToList();

            var duplicatedivisionList =
                duplicatedivisionItems.Where(
                    p => divisionNumberList.Contains(p.JobNumber) || descriptionList.Contains(p.Caption)).ToList();

            //duplicatedivisionNames of divisionList
            var duplicatedivisionCaptionList = (from p in divisionsList
                                           join p2 in duplicatedivisionList on p.Caption equals p2.Caption
                                           select new { Caption = p.Caption, divisionNumber = string.Empty, RowNumber = p.ExcelRowNumber, ErrorMesage = L("DuplicatedivisionName") + p.Caption }).ToList();
            //duplicatedivisionNumbers of divisionList
            var duplicatedivisionsdivisionNumberList = (from p in divisionsList
                                              join p2 in duplicatedivisionList on p.JobNumber equals p2.JobNumber
                                              select new { Caption = string.Empty, divisionNumber = p.JobNumber, RowNumber = p.ExcelRowNumber, ErrorMesage = L("DuplicatedivisionNumber") + p.JobNumber }).ToList();

            var divisionUnits = (from division in divisionsList
                            join duplicatecaption in duplicatedivisionCaptionList
                            on division.ExcelRowNumber equals duplicatecaption.RowNumber
                                                  into duplicatecaptiondivision
                            from duplicatecaptiondivisionunit in duplicatecaptiondivision.DefaultIfEmpty()
                            join duplicatenum in duplicatedivisionsdivisionNumberList
                           on division.ExcelRowNumber equals duplicatenum.RowNumber
                                                 into duplicatedivisionnumber
                            from duplicatedivisionnumberunit in duplicatedivisionnumber.DefaultIfEmpty()
                            select new
                            {
                                division,
                                ErrorMesage =
                                    (!ReferenceEquals(duplicatedivisionnumberunit, null) ? duplicatedivisionnumberunit.ErrorMesage : "") +
                                    (!ReferenceEquals(duplicatecaptiondivisionunit, null) ? duplicatecaptiondivisionunit.ErrorMesage : "") 
                            }).Distinct().ToList();

            //invalid divisionList
            var errordivisions = divisionUnits.Where(u => u.ErrorMesage.Trim().Length > 0).ToList();


            foreach (var division in errordivisions)
            {
                var divisiondto = division.division.MapTo<JobUnit>().MapTo<JobUnitDto>();
                divisiondto.ErrorMessage = division.ErrorMesage.TrimEnd(',').TrimStart(',');
                divisionunitDtoList.Add(divisiondto);
            }
            return divisionunitDtoList;
        }

    }
}
