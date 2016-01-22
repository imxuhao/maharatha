using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using System.Linq;
using System.Data.Entity;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class VendorPaymentTermUnitAppService : CORPACCOUNTINGServiceBase, IVendorPaymentTermUnitAppService
    {
        private readonly VendorPaymentTermUnitManager _vendorPaymentTermUnitManager;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaymentTermUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VendorPaymentTermUnitAppService(VendorPaymentTermUnitManager vendorPaymentTermUnitManager, IRepository<VendorPaymentTermUnit> vendorPaymentTermUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _vendorPaymentTermUnitManager = vendorPaymentTermUnitManager;
            _vendorPaymentTermUnitRepository = vendorPaymentTermUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public IEventBus EventBus { get; set; }
        [UnitOfWork]
        public async Task<VendorPaymentTermUnitDto> CreateVendorPaymentTermUnit(CreateVendorPaymentTermUnitInput input)
        {
            var vendorPaymentTermUnit = new VendorPaymentTermUnit(description:input.Description,duedays:input.DueDays,discountdays:input.DiscountDays,isactive:input.IsActive,organizationid:input.OrganizationUnitId);
            await _vendorPaymentTermUnitManager.CreateAsync(vendorPaymentTermUnit);
            await   CurrentUnitOfWork.SaveChangesAsync();

            #region Example to show the usage of Event Bus as well Unit of Work Completion

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of VendorPaymentTerm is Added*/
            };

            EventBus.Register<EntityChangedEventData<VendorPaymentTermUnitDto>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when VendorPaymentTerm is added
                });

            #endregion

            return vendorPaymentTermUnit.MapTo<VendorPaymentTermUnitDto>();
        }

        public async Task DeleteVendorPaymentTermUnit(IdInput input)
        {
            await _vendorPaymentTermUnitManager.DeleteAsync(input.Id);
        }

        public async Task<ListResultOutput<VendorPaymentTermUnitDto>> GetVendorPaymentTermUnits()
        {
            var query =
                from vpt in _vendorPaymentTermUnitRepository.GetAll()
                select new {vpt};
            var items = await query.ToListAsync();

            return new ListResultOutput<VendorPaymentTermUnitDto>(
                items.Select(item =>
                {
                    var dto = item.vpt.MapTo<VendorPaymentTermUnitDto>();
                    return dto;
                }).ToList());
        }

        public async Task<VendorPaymentTermUnitDto> UpdateVendorPaymentTermUnit(UpdateVendorPaymentTermUnitInput input)
        {
            var vendorPaymentTermUnit = await _vendorPaymentTermUnitRepository.GetAsync(input.VendorPaymentTermId);

            #region Setting the values to be updated

            vendorPaymentTermUnit.Description = input.Description.Replace(" ", "").Trim();
            vendorPaymentTermUnit.DueDays = input.DueDays;
            vendorPaymentTermUnit.DiscountDays = input.DiscountDays;
            vendorPaymentTermUnit.OrganizationUnitId = input.OrganizationUnitId;
            vendorPaymentTermUnit.IsActive = input.IsActive;
            #endregion

            await _vendorPaymentTermUnitManager.UpdateAsync(vendorPaymentTermUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of VendorPaymentTerm is Added*/
            };

            EventBus.Register<EntityChangedEventData<CoaUnit>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when VendorPaymentTerm is added
                });

            return vendorPaymentTermUnit.MapTo<VendorPaymentTermUnitDto>();
        }
    }
}
