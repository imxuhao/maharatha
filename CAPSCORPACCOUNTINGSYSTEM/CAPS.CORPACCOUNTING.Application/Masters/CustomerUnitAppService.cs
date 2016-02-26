using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Dynamic;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class CustomerUnitAppService : CORPACCOUNTINGServiceBase, ICustomerUnitAppService
    {
        private readonly CustomerUnitManager _customerUnitManager;
        private readonly IRepository<CustomerUnit> _customerUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AddressUnitAppService _addressUnitAppService;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit,long> _addressRepository;
        private readonly IRepository<CustomerPaymentTermUnit> _customerPaymentTermRepository;
        private readonly IRepository<SalesRepUnit> _salesRepRepository;

        public CustomerUnitAppService(CustomerUnitManager customerUnitManager,
            IRepository<CustomerUnit> customerUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, AddressUnitAppService addressUnitAppService,
            IAddressUnitAppService addressAppService, IRepository<AddressUnit, long> addressRepository,
            IRepository<CustomerPaymentTermUnit> customerPaymentTermRepository, IRepository<SalesRepUnit> salesRepRepository)
        {
            _customerUnitManager = customerUnitManager;
            _customerUnitRepository = customerUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressUnitAppService = addressUnitAppService;
            _addressAppService = addressAppService;
            _addressRepository = addressRepository;
            _customerPaymentTermRepository = customerPaymentTermRepository;
            _salesRepRepository = salesRepRepository;
        }       

        /// <summary>
        /// Creating the Customer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<CustomerUnitDto> CreateCustomerUnit(CreateCustomerUnitInput input)
        {
            var customerUnit = new CustomerUnit(lastname:input.LastName,firstname:input.FirstName,customernumber:input.CustomerNumber,creditlimit:input.CreditLimit,
                salesrepid:input.SalesRepId,isapproved:input.IsApproved,isactive:input.IsActive,organizationunitid:input.OrganizationUnitId,
               customerpaymenttermid:input.CustomerPayTermsId, typeofpaymentmethodid:input.TypeofPaymentMethodId);
            await _customerUnitManager.CreateAsync(customerUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Addresses != null)
            {
                foreach (var address in input.Addresses)
                {
                    if (address.Line1 != null || address.Line2 != null || address.Line4 != null ||
                        address.Line4 != null || address.State != null ||
                        address.Country != null || address.Email != null || address.Phone1!=null || address.Website !=null)
                    {
                        address.ObjectId = customerUnit.Id;
                        await _addressAppService.CreateAddressUnit(address);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
            }
            return customerUnit.MapTo<CustomerUnitDto>();
        }
        /// <summary>
        /// Delete the Customer and CustomerAddresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteCustomerUnit(IdInput input)
        {
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Customer,
                ObjectId = input.Id
            };
            await _addressUnitAppService.DeleteAddressUnit(dto);
            await _customerUnitManager.DeleteAsync(input.Id);

        }

        /// <summary>
        /// This method is for retrieve the records for showing in the grid with filters and SortOrder
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<CustomerUnitDto>> GetCustomerUnits(GetCustomerInput input)
        {
            var query = CreateCustomerQuery(input);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var customerListDtos = ConvertToCustomerDtos(results);
            return new PagedResultOutput<CustomerUnitDto>(resultCount, customerListDtos);
        }

        /// <summary>
        /// Converting Customer to outputdto of a CustomerUnitDto List
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private  List<CustomerUnitDto> ConvertToCustomerDtos(List<CustomerAndAddressDto> results)
        {
            return results.Select(
                result =>
                {
                    var dto = result.Customer.MapTo<CustomerUnitDto>();
                    dto.CustomerId = result.Customer.Id;
                    dto.PaymentTermDescription = result.PaymentTerms;
                    dto.SalesRepName = result.SalesRepName;
                    dto.Addresses = new Collection<AddressUnitDto>();
                    if (result.Address != null)
                    {
                        dto.Addresses.Add(result.Address.MapTo<AddressUnitDto>());
                        dto.Addresses[0].AddressId = result.Address.Id;
                    }
                    return dto;
                }).ToList();
        }

        private IQueryable<CustomerAndAddressDto> CreateCustomerQuery(GetCustomerInput input)
        {
            var query = from customer in _customerUnitRepository.GetAll()
                join addr in _addressRepository.GetAll() on customer.Id equals addr.ObjectId
                    into temp
                from rt in temp.Where(p => p.IsPrimary == true && p.TypeofObjectId == TypeofObject.Customer).DefaultIfEmpty()
                join payterms in _customerPaymentTermRepository.GetAll() on customer.Id equals payterms.Id
                    into paymentperms
                from pt in paymentperms.DefaultIfEmpty()
                join salesrep in _salesRepRepository.GetAll() on customer.SalesRepId equals salesrep.Id
                    into salesreps
                from sr in salesreps.DefaultIfEmpty()
                select
                    new CustomerAndAddressDto
                    {
                        Customer = customer,
                        Address = rt,
                        PaymentTerms = pt.Description,
                        SalesRepName = sr.LastName
                    };

            query = query
                .WhereIf(input.OrganizationUnitId != null,
                    item => item.Customer.OrganizationUnitId == input.OrganizationUnitId)
                .WhereIf(!input.FirstName.IsNullOrWhiteSpace(),
                    item => item.Customer.FirstName.Contains(input.FirstName))
                .WhereIf(!input.LastName.IsNullOrWhiteSpace(), item => item.Customer.LastName.Contains(input.LastName))
                .WhereIf(!input.CustomerNumber.IsNullOrWhiteSpace(),
                    item => item.Customer.CustomerNumber.Contains(input.CustomerNumber));
            return query;
        }



        /// <summary>
        /// To update the Customers with addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerUnitDto> UpdateCustomerUnit(UpdateCustomerUnitInput input)
        {
            var customerUnit = await _customerUnitRepository.GetAsync(input.CustomerId);
            foreach (var address in input.InputAddresses)
            {
                if (address.AddressId != 0)
                    await _addressAppService.UpdateAddressUnit(address);
                else
                {
                    if (address.Line1 != null || address.Line2 != null ||
                        address.Line4 != null || address.Line4 != null ||
                        address.State != null || address.Country != null ||
                        address.Email != null || address.Phone1 != null || address.Website != null)
                    {
                        address.TypeofObjectId = TypeofObject.Customer;
                        address.ObjectId = input.CustomerId;
                        await
                            _addressAppService.CreateAddressUnit(
                                AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(address));
                    }
                }
                await CurrentUnitOfWork.SaveChangesAsync();

            }

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

        /// <summary>
        /// To get the CustomerDetails with Addresses by CustomerId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<CustomerUnitDto> GetCustomerUnitsById(IdInput input)
        {
            var customerUnit = await _customerUnitRepository.GetAsync(input.Id);
            var addressitems = await _addressRepository.GetAllListAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Customer);
            
            var result= customerUnit.MapTo<CustomerUnitDto>();
            result.CustomerId = customerUnit.Id;
            result.Addresses = new Collection<AddressUnitDto>();
            for (int i = 0; i < addressitems.Count; i++)
            {
                result.Addresses.Add(addressitems[i].MapTo<AddressUnitDto>());
                result.Addresses[i].AddressId = addressitems[i].Id;
            }
            return result;
        }
    }
}
