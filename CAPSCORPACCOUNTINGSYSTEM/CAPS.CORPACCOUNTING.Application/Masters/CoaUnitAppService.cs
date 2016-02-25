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
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Authorization;

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
        public async Task<PagedResultOutput<CoaUnitDto>> GetCoaUnits(GetCoaInput input)
        {

            var query =
                from au in _coaUnitRepository.GetAll()
                select new {Coa = au};
            query = query
                .WhereIf(input.OrganizationUnitId != null,
                    item => item.Coa.OrganizationUnitId == input.OrganizationUnitId)
                .WhereIf(!input.Description.IsNullOrWhiteSpace(),
                    item => item.Coa.Description.Contains(input.Description))
                .WhereIf(input.ChartofAccountsType != null,
                    item => item.Coa.ChartofAccountsType == input.ChartofAccountsType);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<CoaUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.Coa.MapTo<CoaUnitDto>();
                dto.CoaId = item.Coa.Id;
                return dto;
            }).ToList());
        }

        [UnitOfWork]
        public async Task<CoaUnitDto> CreateCoaUnit(CreateCoaUnitInput input)
        {
            var coaUnit = new CoaUnit(caption: input.Caption, chartofaccounttype: input.ChartofAccountsType, organizationid: input.OrganizationId, desc: input.Description,
                displaysequence: input.DisplaySequence, isactive: input.IsActive, isapproved: input.IsApproved, isprivate: input.IsPrivate);
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
        public async Task DeleteCoaUnit(IdInput input)
        {
            await _coaunitManager.DeleteAsync(input.Id);
        }
    }
}