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
    public class CustomerUnitAppService : CORPACCOUNTINGServiceBase, ICustomerUnitAppService
    {
        private readonly CustomerUnitManager _customerUnitManager;
        private readonly IRepository<CustomerUnit> _customerUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AddressUnitAppService _addressUnitAppService;
        private readonly IRepository<AddressUnit,long> _addresRepository;



        public CustomerUnitAppService(CustomerUnitManager customerUnitManager, IRepository<CustomerUnit> customerUnitRepository,
            IUnitOfWorkManager unitOfWorkManager,AddressUnitAppService addressUnitAppService)
        {
            _customerUnitManager = customerUnitManager;
            _customerUnitRepository = customerUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressUnitAppService = addressUnitAppService;
        }

        [UnitOfWork]
        public async Task<CustomerUnitDto> CreateCustomerUnit(
            CreateCustomerUnitInput input)
        {
            var customerRepUnit = new CustomerUnit(lastname:input.LastName,firstname:input.FirstName,customernumber:input.CustomerNumber,creditlimit:input.CreditLimit,
                salesrepid:input.SalesRepId,isapproved:input.IsApproved,isactive:input.IsActive,organizationunitid:input.OrganizationUnitId,
               customerpaymenttermid:input.CustomerPayTermsId, typeofpaymentmethodid:input.TypeofPaymentMethodId);
            await _customerUnitManager.CreateAsync(customerRepUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            input.InputAddress.ObjectId = customerRepUnit.Id;
            input.InputAddress.TypeofObjectId=TypeofObject.Customer;
            input.InputAddress.AddressTypeId=TypeofAddress.Home;
            await _addressUnitAppService.CreateAddressUnit(input.InputAddress);
            await CurrentUnitOfWork.SaveChangesAsync();
            return customerRepUnit.MapTo<CustomerUnitDto>();
        }

        public async Task DeleteCustomerUnit(IdInput input)
        {

            await _customerUnitManager.DeleteAsync(input.Id);
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput();
            dto.TypeofObjectId = TypeofObject.Customer;
            dto.ObjectId = input.Id;
            await _addressUnitAppService.DeleteAddressUnit(dto);
           
        }

        public async Task<ListResultOutput<CustomerUnitDto>> GetCustomerUnits()
        {

            var query =
                from customer in _customerUnitRepository.GetAll()
                join ar in _addresRepository.GetAll() on customer.Id equals ar.ObjectId
                select new { customer };
            var items = await query.ToListAsync();

            return new ListResultOutput<CustomerUnitDto>(
                items.Select(item =>
                {
                    var dto = item.customer.MapTo<CustomerUnitDto>();
                    dto.CustomerPayTermsId = item.customer.Id;
                    return dto;
                }).ToList());
        }

        public async Task<CustomerUnitDto> UpdateCustomerUnit(UpdateCustomerUnitInput input)
        {
            var customerUnit = await _customerUnitRepository.GetAsync(input.CustomerId);
            input.InputAddress.ObjectId = input.CustomerId;
            input.InputAddress.TypeofObjectId=TypeofObject.Customer;
            await _addressUnitAppService.UpdateAddressUnit(input.InputAddress);
            await CurrentUnitOfWork.SaveChangesAsync();

            #region Setting the values to be updated

            customerUnit.LastName = input.LastName;
            customerUnit.FirstName = input.FirstName;
            customerUnit.IsActive = input.IsActive;
            customerUnit.CreditLimit = input.CreditLimit;
            customerUnit.CustomerNumber = input.CustomerNumber;
            customerUnit.CustomerPayTermsId = input.CustomerPayTermsId;
            customerUnit.CustomerNumber = input.CustomerNumber;
            customerUnit.SalesRepId = input.SalesRepId;
            customerUnit.IsApproved = input.IsApproved;
            customerUnit.OrganizationUnitId = input.OrganizationUnitId;
            customerUnit.TypeofPaymentMethodId = input.TypeofPaymentMethodId;
            customerUnit.CustomerPayTermsId = input.CustomerPayTermsId;
            #endregion

            await _customerUnitManager.UpdateAsync(customerUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Customer is Added*/
            };

            return customerUnit.MapTo<CustomerUnitDto>();
        }
    }
}
