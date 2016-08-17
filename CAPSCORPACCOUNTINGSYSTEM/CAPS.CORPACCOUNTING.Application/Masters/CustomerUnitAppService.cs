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
using Abp.Linq.Extensions;
using Abp.Authorization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize(AppPermissions.Pages_Receivables_Customers)] ///This is to ensure only logged in user has access to this module.
    public class CustomerUnitAppService : CORPACCOUNTINGServiceBase, ICustomerUnitAppService
    {
        private readonly CustomerUnitManager _customerUnitManager;
        private readonly IRepository<CustomerUnit> _customerUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AddressUnitAppService _addressUnitAppService;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit, long> _addressRepository;
        private readonly IRepository<CustomerPaymentTermUnit> _customerPaymentTermRepository;
        private readonly IRepository<SalesRepUnit> _salesRepRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerUnitManager"></param>
        /// <param name="customerUnitRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="addressUnitAppService"></param>
        /// <param name="addressAppService"></param>
        /// <param name="addressRepository"></param>
        /// <param name="customerPaymentTermRepository"></param>
        /// <param name="salesRepRepository"></param>
        public CustomerUnitAppService(CustomerUnitManager customerUnitManager,
            IRepository<CustomerUnit> customerUnitRepository,
            IUnitOfWorkManager unitOfWorkManager,
            AddressUnitAppService addressUnitAppService,
            IAddressUnitAppService addressAppService,
            IRepository<AddressUnit, long> addressRepository,
            IRepository<CustomerPaymentTermUnit> customerPaymentTermRepository,
            IRepository<SalesRepUnit> salesRepRepository)
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
        [AbpAuthorize(AppPermissions.Pages_Receivables_Customers_Create)]
        public async Task<CustomerUnitDto> CreateCustomerUnit(CreateCustomerUnitInput input)
        {
            var customerUnit = input.MapTo<CustomerUnit>();
            await _customerUnitManager.CreateAsync(customerUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            //address Information
            if (!ReferenceEquals(input.Addresses, null))
            {
                foreach (var address in input.Addresses)
                {
                    if (address.Line1 != null || address.Line2 != null ||
                        address.Line4 != null || address.Line4 != null ||
                        address.State != null || address.Country != null ||
                        address.Email != null || address.Phone1 != null ||
                        address.ContactNumber != null)
                    {
                        address.TypeofObjectId = TypeofObject.Customer;
                        address.ObjectId = customerUnit.Id;
                        await _addressAppService.CreateAddressUnit(address);
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            var customerUnitDto = customerUnit.MapTo<CustomerUnitDto>();
            customerUnitDto.CustomerId = customerUnit.Id;
            return customerUnitDto;
        }

        /// <summary>
        /// Delete the Customer and CustomerAddresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Delete)]
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
        public async Task<PagedResultOutput<CustomerUnitDto>> GetCustomerUnits(SearchInputDto input)
        {
            var query = CreateCustomerQuery(input);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Customer.LastName ASC", input.Sorting))
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
        private List<CustomerUnitDto> ConvertToCustomerDtos(List<CustomerAndAddressDto> results)
        {
            return results.Select(
                result =>
                {
                    var dto = result.Customer.MapTo<CustomerUnitDto>();
                    dto.CustomerId = result.Customer.Id;
                    dto.PaymentTermDescription = result.PaymentTerms;
                    dto.SalesRepName = result.SalesRepName;
                    dto.Address = new Collection<AddressUnitDto>();
                    if (result.Address != null)
                    {
                        dto.Address.Add(result.Address.MapTo<AddressUnitDto>());
                        dto.Address[0].AddressId = result.Address.Id;
                    }
                    return dto;
                }).ToList();
        }

        private IQueryable<CustomerAndAddressDto> CreateCustomerQuery(SearchInputDto input)
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

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), item => item.Customer.OrganizationUnitId == input.OrganizationUnitId);
            return query;
        }



        /// <summary>
        /// To update the Customers with addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Receivables_Customers_Edit)]
        public async Task<CustomerUnitDto> UpdateCustomerUnit(UpdateCustomerUnitInput input)
        {
            var customerUnit = await _customerUnitRepository.GetAsync(input.CustomerId);

            // update address Information
            if (!ReferenceEquals(input.Addresses, null))
            {
                foreach (var address in input.Addresses)
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

            var customerUnitDto = customerUnit.MapTo<CustomerUnitDto>();
            customerUnitDto.CustomerId = customerUnit.Id;
            return customerUnitDto;
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

            var result = customerUnit.MapTo<CustomerUnitDto>();
            result.CustomerId = customerUnit.Id;
            result.Address = new Collection<AddressUnitDto>();
            for (int i = 0; i < addressitems.Count; i++)
            {
                result.Address.Add(addressitems[i].MapTo<AddressUnitDto>());
                result.Address[i].AddressId = addressitems[i].Id;
            }
            return result;
        }

        /// <summary>
        /// Customer as List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetCustomerList(AutoSearchInput input)
        {
            var customerList = await _customerUnitRepository.GetAll()
                 .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.LastName.Contains(input.Query))
                 .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
                 .Select(u => new NameValueDto { Name = u.LastName, Value = u.Id.ToString() }).ToListAsync();
            return customerList;
        }

        /// <summary>
        /// Get Payment Method List
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofPaymentMethodList()
        {
            return EnumList.GetTypeofPaymentMethodList();
        }

        /// <summary>
        /// Get Customer Payment Terms
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetCustomerPaymentTermsList(AutoSearchInput input)
        {
            var customerPaymentTermList = await _customerPaymentTermRepository.GetAll()
          .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.Description.Contains(input.Query))
          .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
          .Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return customerPaymentTermList;
        }

        /// <summary>
        /// Get SalesRep as list
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetSalesRepList(AutoSearchInput input)
        {
            var salesRepList = await _salesRepRepository.GetAll()
          .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.LastName.Contains(input.Query))
          .WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.OrganizationUnitId == input.OrganizationUnitId)
          .Select(u => new NameValueDto { Name = u.LastName, Value = u.Id.ToString() }).ToListAsync();
            return salesRepList;
        }
    }
}
