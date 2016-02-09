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

namespace CAPS.CORPACCOUNTING.Masters
{
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
        /// This method is for testing to Insert data in customer  with 2 addresses.After UI development we need to remove this method.
        /// using this method we are calling CreatecustomerUnit to insert customer and Addresss Data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task InsertCustomerData(CreateCustomerUnitInput input)
        {
            CreateAddressUnitInput customerAddr1 = new CreateAddressUnitInput();
            customerAddr1.TypeofObjectId = TypeofObject.Customer;
            customerAddr1.AddressTypeId = TypeofAddress.PrimaryContact;
            customerAddr1.IsPrimary = true;
            customerAddr1.Line1 = "Address1Customer";
            CreateAddressUnitInput customerAddr2 = new CreateAddressUnitInput();
            customerAddr2.TypeofObjectId = TypeofObject.Customer;
            customerAddr2.AddressTypeId = TypeofAddress.Home;
            customerAddr2.Line1 = "Address2Customer";

            input.InputAddresses = new List<CreateAddressUnitInput>();
            input.InputAddresses.Add(customerAddr1);
            input.InputAddresses.Add(customerAddr2);
            await CreateCustomerUnit(input);
        }
        /// <summary>
        /// This method is  for testing to update customer data with addresses.After UI development we need to remove this method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdatedCustomerData(UpdateCustomerUnitInput input)
        {

            UpdateAddressUnitInput customerAddr1 = new UpdateAddressUnitInput();
            customerAddr1.TypeofObjectId = TypeofObject.Customer;
            customerAddr1.AddressTypeId = TypeofAddress.PrimaryContact;
            customerAddr1.Email = "test@gmail.com";
            customerAddr1.Website = "https://www.google.co.in";
            customerAddr1.AddressId = 1;
            customerAddr1.ObjectId = input.CustomerId;
            customerAddr1.IsPrimary = true;

            UpdateAddressUnitInput customerAddr2 = new UpdateAddressUnitInput();
            customerAddr2.TypeofObjectId = TypeofObject.Customer;
            customerAddr2.AddressTypeId = TypeofAddress.Business;
            customerAddr2.Email = "test1@gmail.com";
            customerAddr2.AddressId = 2;
            customerAddr2.ObjectId = input.CustomerId;

            input.InputAddresses = new List<UpdateAddressUnitInput>();
            input.InputAddresses.Add(customerAddr1);
            input.InputAddresses.Add(customerAddr2);
            await UpdateCustomerUnit(input);
        }

        [UnitOfWork]
        public async Task<CustomerUnitDto> CreateCustomerUnit(
            CreateCustomerUnitInput input)
        {
            var customerUnit = new CustomerUnit(lastname:input.LastName,firstname:input.FirstName,customernumber:input.CustomerNumber,creditlimit:input.CreditLimit,
                salesrepid:input.SalesRepId,isapproved:input.IsApproved,isactive:input.IsActive,organizationunitid:input.OrganizationUnitId,
               customerpaymenttermid:input.CustomerPayTermsId, typeofpaymentmethodid:input.TypeofPaymentMethodId);
            await _customerUnitManager.CreateAsync(customerUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.InputAddresses != null)
            {
                foreach (var address in input.InputAddresses)
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
        /// This method is for retrieve the records for showing in the grid with filteration and Order
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<CustomerUnitDto>> GetCustomerUnits(GetCustomerInput input)
        {
            input.SortOrder = input.SortOrder == "ASC" ? " ascending" : " descending";
            
            var query =
                (from customer in
                    _customerUnitRepository.GetAll()
                        .OrderBy(input.SortColumn + input.SortOrder)
                        .Skip((input.PageNumber-1)*input.NumberofColumnsperPage)
                        .Take(input.NumberofColumnsperPage)
                    join addr in _addressRepository.GetAll() on customer.Id equals addr.ObjectId
                        into temp
                    from rt in temp.Where(p => p.IsPrimary == true).DefaultIfEmpty()
                    join payterms in _customerPaymentTermRepository.GetAll() on customer.Id equals payterms.Id
                        into paymentperms
                    from pt in paymentperms.DefaultIfEmpty()
                 join salesrep in _salesRepRepository.GetAll() on customer.SalesRepId equals salesrep.Id
                     into salesreps
                 from sr in salesreps.DefaultIfEmpty()
                 where
                        (input.OrganizationUnitId == null || customer.OrganizationUnitId == input.OrganizationUnitId) &&
                        (input.LastName == null || customer.LastName.Contains(input.LastName)) &&
                        (input.FirstName == null || customer.FirstName.Contains(input.FirstName)) &&
                        (input.CustomerNumber == null || customer.CustomerNumber.Contains(input.CustomerNumber))
                    select new {customer, Address = rt, Description = pt.Description,SalesRepName=sr.LastName});

            var items = await query.ToListAsync();

            return new ListResultOutput<CustomerUnitDto>(
                items.Select(item =>
                {
                    var dto = item.customer.MapTo<CustomerUnitDto>();
                    dto.CustomerId = item.customer.Id;
                    dto.PaymentTermDescription = item.Description;
                    dto.SalesRepName = item.SalesRepName;
                    dto.Addresses = new Collection<AddressUnitDto>();
                    if (item.Address != null)
                    {
                        dto.Addresses.Add(item.Address.MapTo<AddressUnitDto>());
                        dto.Addresses[0].AddressId = item.Address.Id;
                    }
                    return dto;
                }).ToList());
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
                        AutoMapper.Mapper.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
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
            var customerquery =
               from customer in _customerUnitRepository.GetAll()
               where customer.Id == input.Id
               select new { customer };
            var customeritems = await customerquery.ToListAsync();

            var addressquery =
               from addr in _addressRepository.GetAll()
               where addr.ObjectId == input.Id && addr.TypeofObjectId == TypeofObject.Customer
               select new { addr };

            var addressitems = await addressquery.ToListAsync();

            var result= customeritems[0].customer.MapTo<CustomerUnitDto>();
            result.CustomerId = customeritems[0].customer.Id;
            result.Addresses = new Collection<AddressUnitDto>();
            for (int i=0;i<addressitems.Count;i++)
            {
                result.Addresses.Add(addressitems[i].addr.MapTo<AddressUnitDto>());
                result.Addresses[i].AddressId = addressitems[i].addr.Id;
            }

            return result;
        }
    }
}
