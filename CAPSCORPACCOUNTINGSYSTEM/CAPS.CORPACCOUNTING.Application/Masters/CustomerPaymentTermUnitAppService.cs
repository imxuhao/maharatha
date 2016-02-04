using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class CustomerPaymentTermUnitAppService : CORPACCOUNTINGServiceBase, ICustomerPaymentTermUnitAppService
    {
        private readonly CustomerPaymentTermUnitManager _customerPaymentTermUnitManager;
        private readonly IRepository<CustomerPaymentTermUnit> _customerPaymentTermUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerPaymentTermUnitAppService(CustomerPaymentTermUnitManager customerPaymentTermUnitManager, IRepository<CustomerPaymentTermUnit> customerPaymentTermUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _customerPaymentTermUnitManager = customerPaymentTermUnitManager;
            _customerPaymentTermUnitRepository = customerPaymentTermUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public async Task<CustomerPaymentTermUnitDto> CreateCustomerPaymentTermUnit(
            CreateCustomerPaymentTermUnitInput input)
        {
            var customerPaymentTermUnit = new CustomerPaymentTermUnit(description: input.Description,
                duedays: input.DueDays, paymentinstruction: input.PaymentInstruction,
                discountpercent: input.DiscountPercent, discountdays: input.DiscountDays,
                overnightinstructions: input.OvernightInstructions, wiringinstructions: input.WiringInstructions,
                footermessage: input.FooterMessage, logocaption: input.LogoCaption, isdefault: input.IsDefault,
                organizationid: input.OrganizationUnitId);
            await _customerPaymentTermUnitManager.CreateAsync(customerPaymentTermUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return customerPaymentTermUnit.MapTo<CustomerPaymentTermUnitDto>();
        }

        public async Task DeleteCustomerPaymentTermUnit(IdInput input)
        {
            await _customerPaymentTermUnitManager.DeleteAsync(input.Id);
        }

        public async Task<ListResultOutput<CustomerPaymentTermUnitDto>> GetCustomerPaymentTermUnits(long? organizationUnitId)
        {
            var query =
                from cpt in _customerPaymentTermUnitRepository.GetAll()
                where organizationUnitId == null || cpt.OrganizationUnitId == organizationUnitId
                select new { cpt };
            var items = await query.ToListAsync();

            return new ListResultOutput<CustomerPaymentTermUnitDto>(
                items.Select(item =>
                {
                    var dto = item.cpt.MapTo<CustomerPaymentTermUnitDto>();
                    dto.CustomerPaymentTermId = item.cpt.Id;
                    return dto;
                }).ToList());
        }

        public async Task<CustomerPaymentTermUnitDto> UpdateCustomerPaymentTermUnit(UpdateCustomerPaymentTermUnitInput input)
        {
            var customerPaymentTermUnit = await _customerPaymentTermUnitRepository.GetAsync(input.CustomerPaymentTermId);

            #region Setting the values to be updated

            customerPaymentTermUnit.Description = input.Description;
            customerPaymentTermUnit.DueDays = input.DueDays;
            customerPaymentTermUnit.DiscountDays = input.DiscountDays;
            customerPaymentTermUnit.OrganizationUnitId = input.OrganizationUnitId;
            customerPaymentTermUnit.IsDefault = input.IsDefault;
            customerPaymentTermUnit.FooterMessage = input.FooterMessage;
            customerPaymentTermUnit.LogoCaption = input.LogoCaption;
            customerPaymentTermUnit.DiscountPercent = input.DiscountPercent;
            customerPaymentTermUnit.OvernightInstructions = input.OvernightInstructions;
            customerPaymentTermUnit.PaymentInstruction = input.PaymentInstruction;
            customerPaymentTermUnit.WiringInstructions = input.WiringInstructions;
            #endregion

            await _customerPaymentTermUnitManager.UpdateAsync(customerPaymentTermUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of customerPaymentTerm is Added*/
            };

            return customerPaymentTermUnit.MapTo<CustomerPaymentTermUnitDto>();
        }
    }
}
