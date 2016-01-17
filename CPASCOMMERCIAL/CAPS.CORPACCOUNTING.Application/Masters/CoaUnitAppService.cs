using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class CoaUnitAppService : CORPACCOUNTINGServiceBase, ICoaUnitAppService
    {
        private readonly CoaUnitManager _coaunitManager;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public IEventBus EventBus { get; set; }
        public CoaUnitAppService(CoaUnitManager coaunitManager, IRepository<CoaUnit> coaUnitRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _coaunitManager = coaunitManager;
            _coaUnitRepository = coaUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<ListResultOutput<CoaUnitDto>> GetCoaUnits()
        {

            var query =
                from au in _coaUnitRepository.GetAll()

                select new { au, memberCount = au };

            var items = await query.ToListAsync();

            return new ListResultOutput<CoaUnitDto>(
                items.Select(item =>
                {
                    var dto = item.au.MapTo<CoaUnitDto>();
                    //dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }

        [UnitOfWork]
        public async Task<CoaUnitDto> CreateCoaUnit(CreateCoaUnitInput input)
        {

            var coaUnit = new CoaUnit(caption: input.Caption, chartofaccounttype: input.ChartofAccountsType);
            await _coaunitManager.CreateAsync(coaUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) => {/*Do Something when the Chart of Account is Added*/};

            EventBus.Register<EntityChangedEventData<CoaUnit>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when COA is added
                });

            return coaUnit.MapTo<CoaUnitDto>();

        }


    }
}
