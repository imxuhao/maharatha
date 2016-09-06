using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Linq.Extensions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Authorization;

namespace CAPS.CORPACCOUNTING.Masters
{

    [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts)] ///This is to ensure only logged in user has access to this module. We will improvise accordingly
    public class CoaUnitAppService : CORPACCOUNTINGServiceBase, ICoaUnitAppService
    {
        private readonly CoaUnitManager _coaunitManager;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public CoaUnitAppService(CoaUnitManager coaunitManager, IRepository<CoaUnit> coaUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _coaunitManager = coaunitManager;
            _coaUnitRepository = coaUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Get the Records for Grid with paging and  sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<CoaUnitDto>> GetCoaUnits(SearchInputDto input)
        {

            var query = from coa in _coaUnitRepository.GetAll()
                        join linkcoa in _coaUnitRepository.GetAll()
                        on coa.LinkChartOfAccountID equals linkcoa.Id
                        into tempCoa
                        from coaunit in tempCoa.DefaultIfEmpty()
                        select new { Coa = coa, LinkChartOfAccountName = coaunit.Caption };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(p => p.Coa.IsCorporate);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Coa.Description ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<CoaUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Coa.MapTo<CoaUnitDto>();
                dto.CoaId = item.Coa.Id;
                dto.StandardGroupTotal = item.Coa.StandardGroupTotalId != null ? item.Coa.StandardGroupTotalId.ToDisplayName() : "";
                dto.LinkChartOfAccountName = item.LinkChartOfAccountName;
                return dto;
            }).ToList());
        }

        /// <summary>
        /// Creating COAUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Create)]
        public async Task<CoaUnitDto> CreateCoaUnit(CreateCoaUnitInput input)
        {
            var coaUnit = input.MapTo<CoaUnit>();
            coaUnit.OrganizationUnitId = input.OrganizationUnitId;
            await _coaunitManager.CreateAsync(coaUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            #region Example to show the usage of Event Bus as well Unit of Work Completion

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Account is Added*/
            };

            EventBus.Register<EntityChangedEventData<CoaUnit>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when COA is added
                });

            #endregion

            return coaUnit.MapTo<CoaUnitDto>();
        }
        /// <summary>
        /// Updating COAUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Edit)]
        public async Task<CoaUnitDto> UpdateCoaUnit(UpdateCoaUnitInput input)
        {
            var coaUnit = await _coaUnitRepository.GetAsync(input.CoaId);

            #region Setting the values to be updated

            coaUnit.Caption = input.Caption;
            coaUnit.Description = input.Description;
            coaUnit.DisplaySequence = input.DisplaySequence;
            coaUnit.IsActive = input.IsActive;
            coaUnit.IsApproved = input.IsApproved;
            coaUnit.IsPrivate = input.IsPrivate;
            coaUnit.OrganizationUnitId = input.OrganizationUnitId;
            coaUnit.IsActive = input.IsActive;
            coaUnit.IsCorporate = input.IsCorporate;
            coaUnit.IsNumeric = input.IsNumeric;
            coaUnit.LinkChartOfAccountID = input.LinkChartOfAccountID;
            coaUnit.StandardGroupTotalId = input.StandardGroupTotalId;
            #endregion

            await _coaunitManager.UpdateAsync(coaUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Account is Added*/
            };

            EventBus.Register<EntityChangedEventData<CoaUnit>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when COA is added
                });

            return coaUnit.MapTo<CoaUnitDto>();
        }
        /// <summary>
        /// Delete COAUnit by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Delete)]
        public async Task DeleteCoaUnit(IdInput input)
        {
            await _coaunitManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// Get COAUnit by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CoaUnitDto> GetCoaUnitById(IdInput input)
        {
            CoaUnit coaUnit = await _coaUnitRepository.GetAsync(input.Id);
            CoaUnitDto result = coaUnit.MapTo<CoaUnitDto>();
            result.CoaId = coaUnit.Id;
            return result;
        }
        /// <summary>
        /// Gets all the COA for that company except input Coa ( ConvertNewCOA Dropdown Data)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetCoaList(GetCoaInput input)
        {
            return await (from au in _coaUnitRepository.GetAll()
                          .WhereIf(input.CoaId != null, p => p.Id != input.CoaId)
                          select new NameValueDto { Name = au.Caption, Value = au.Id.ToString() }).OrderBy(p => p.Name).ToListAsync();
        }

        /// <summary>
        /// Get StandardGroupTotals as List
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> StandardGroupTotalList()
        {
            return EnumList.GetStandardGroupTotalList();
        }

    }
}