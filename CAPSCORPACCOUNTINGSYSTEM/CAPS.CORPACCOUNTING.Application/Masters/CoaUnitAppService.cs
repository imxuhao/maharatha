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

namespace CAPS.CORPACCOUNTING.Masters
{

    [AbpAuthorize] ///This is to ensure only logged in user has access to this module. We will improvise accordingly
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

            var query =
                from au in _coaUnitRepository.GetAll()
                select new { Coa = au };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.Coa.OrganizationUnitId == input.OrganizationUnitId || item.Coa.OrganizationUnitId == null);

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
                return dto;
            }).ToList());
        }
      
        /// <summary>
        /// Creating COAUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        public async Task<CoaUnitDto> CreateCoaUnit(CreateCoaUnitInput input)
        {
            //var coaUnit = new CoaUnit(caption: input.Caption, chartofaccounttype: input.ChartofAccountsType, organizationid: input.OrganizationId, desc: input.Description,
            //    displaysequence: input.DisplaySequence, isactive: input.IsActive, isapproved: input.IsApproved, isprivate: input.IsPrivate);
            var coaUnit = input.MapTo<CoaUnit>();
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
        public async Task<CoaUnitDto> UpdateCoaUnit(UpdateCoaUnitInput input)
        {
            var coaUnit = await _coaUnitRepository.GetAsync(input.CoaId);

            #region Setting the values to be updated

            coaUnit.Caption = input.Caption;
            coaUnit.ChartofAccountsType = input.ChartofAccountsType;
            coaUnit.Description = input.Description;
            coaUnit.DisplaySequence = input.DisplaySequence;
            coaUnit.IsActive = input.IsActive;
            coaUnit.IsApproved = input.IsApproved;
            coaUnit.IsPrivate = input.IsPrivate;
            coaUnit.OrganizationUnitId = input.OrganizationId;
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
    }
}