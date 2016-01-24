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

        public CustomerUnitAppService(CustomerUnitManager customerUnitManager, IRepository<CustomerUnit> customerUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _customerUnitManager = customerUnitManager;
            _customerUnitRepository = customerUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [UnitOfWork]
        public async Task<CustomerUnitDto> CreateCustomerUnit(
            CreateCustomerUnitInput input)
        {
            var salesRepUnit = new CustomerUnit(lastname:input.LastName,firstname:input.FirstName,customernumber:input.CustomerNumber,creditlimit:input.CreditLimit,
                salesrepid:input.SalesRepId,isapproved:input.IsApproved,isactive:input.IsActive,organizationunitid:input.OrganizationUnitId,
               customerpaymenttermid:input.CustomerPayTermsId, typeofpaymentmethodid:input.TypeofPaymentMethodId);
            await _customerUnitManager.CreateAsync(salesRepUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            return salesRepUnit.MapTo<CustomerUnitDto>();
        }

        public async Task DeleteCustomerUnit(IdInput input)
        {
            await _customerUnitManager.DeleteAsync(input.Id);
        }

        public async Task<ListResultOutput<CustomerUnitDto>> GetCustomerUnits()
        {
            var query =
                from sr in _customerUnitRepository.GetAll()
                select new { sr };
            var items = await query.ToListAsync();

            return new ListResultOutput<CustomerUnitDto>(
                items.Select(item =>
                {
                    var dto = item.sr.MapTo<CustomerUnitDto>();
                    return dto;
                }).ToList());
        }

        public async Task<CustomerUnitDto> UpdateCustomerUnit(UpdateCustomerUnitInput input)
        {
            var customerUnit = await _customerUnitRepository.GetAsync(input.CustomerId);

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
                /*Do Something when the Chart of salesRep is Added*/
            };

            return customerUnit.MapTo<CustomerUnitDto>();
        }
    }
}
