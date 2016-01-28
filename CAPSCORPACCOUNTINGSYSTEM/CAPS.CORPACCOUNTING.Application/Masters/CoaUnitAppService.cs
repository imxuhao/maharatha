using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
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

        public async Task<ListResultOutput<CoaUnitDto>> GetCoaUnits(long? organizationUnitId)
        {

            var items =
                     from au in await _coaUnitRepository.GetAllListAsync(p => p.OrganizationUnitId == organizationUnitId)
                     select new { au, memberCount = au };

            //  var items = await query.ToList();

            return new ListResultOutput<CoaUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<CoaUnitDto>();
                    //dto.MemberCount = item.memberCount;
                    dto.CoaId = item.au.Id;
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