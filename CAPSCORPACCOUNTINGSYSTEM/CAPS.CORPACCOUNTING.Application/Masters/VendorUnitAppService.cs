using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using Abp.Authorization;
using Abp.Collections.Extensions;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Sessions;
using Abp.Runtime.Caching;
using AutoMapper;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize(AppPermissions.Pages_Payables_Vendors)]///This is to ensure only logged in user has access to this module.
    public class VendorUnitAppService : CORPACCOUNTINGServiceBase, IVendorUnitAppService
    {
        private readonly VendorUnitManager _vendorUnitManager;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit, long> _addresRepository;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaytermRepository;
        private readonly IRepository<TypeOfCountryUnit, short> _typeOfCountryRepository;
        private readonly IRepository<RegionUnit> _regionRepository;
        private readonly IRepository<CountryUnit> _countryRepository;
        private readonly IRepository<VendorAliasUnit> _vendorAliasUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly VendorAliasUnitManager _vendorAliasUnitManager;
        private readonly CustomAppSession _customAppSession;
        private readonly ICacheManager _cacheManager;
        private readonly IAccountCache _accountCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vendorUnitManager"></param>
        /// <param name="vendorUnitRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="addressAppService"></param>
        /// <param name="addresRepository"></param>
        /// <param name="vendorPaytermRepository"></param>
        /// <param name="typeOfCountryRepository"></param>
        /// <param name="regionRepository"></param>
        /// <param name="countryRepository"></param>
        /// <param name="vendorAliasUnitRepository"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="coaUnitRepository"></param>
        /// <param name="vendorAliasUnitManager"></param>
        public VendorUnitAppService(VendorUnitManager vendorUnitManager, IRepository<VendorUnit> vendorUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAddressUnitAppService addressAppService,
            IRepository<AddressUnit, long> addresRepository, IRepository<VendorPaymentTermUnit> vendorPaytermRepository,
            IRepository<TypeOfCountryUnit, short> typeOfCountryRepository, IRepository<RegionUnit> regionRepository,
            IRepository<CountryUnit> countryRepository, IRepository<VendorAliasUnit> vendorAliasUnitRepository,
            IRepository<AccountUnit, long> accountUnitRepository, IRepository<CoaUnit> coaUnitRepository,
            VendorAliasUnitManager vendorAliasUnitManager, CustomAppSession customAppSession, ICacheManager cacheManager, IAccountCache accountCache)
        {
            _vendorUnitManager = vendorUnitManager;
            _vendorUnitRepository = vendorUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
            _addresRepository = addresRepository;
            _vendorPaytermRepository = vendorPaytermRepository;
            _typeOfCountryRepository = typeOfCountryRepository;
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            _vendorAliasUnitRepository = vendorAliasUnitRepository;
            _accountUnitRepository = accountUnitRepository;
            _coaUnitRepository = coaUnitRepository;
            _vendorAliasUnitManager = vendorAliasUnitManager;
            _customAppSession = customAppSession;
            _cacheManager = cacheManager;
            _accountCache = accountCache;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Creating the Vendor,Addresses and Vendor Alias Information
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payables_Vendors_Create)]
        [UnitOfWork]
        public async Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input)
        {
            var vendorUnit = input.MapTo<VendorUnit>();
            await _vendorUnitManager.CreateAsync(vendorUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            //address Information
            if (!ReferenceEquals(input.Addresses, null))
            {
                foreach (var address in input.Addresses)
                {
                    if (!string.IsNullOrEmpty(address.Line1)  || !string.IsNullOrEmpty(address.Line2 )||
                        !string.IsNullOrEmpty(address.Line4 ) || !string.IsNullOrEmpty(address.Line4 )||
                        !string.IsNullOrEmpty(address.State ) || !string.IsNullOrEmpty(address.Country) ||
                        !string.IsNullOrEmpty(address.Email ) || !string.IsNullOrEmpty(address.Phone1 )||
                        !string.IsNullOrEmpty(address.ContactNumber))
                    {
                        address.TypeofObjectId = TypeofObject.Vendor;
                        address.ObjectId = vendorUnit.Id;
                        await _addressAppService.CreateAddressUnit(address);
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }

            //vendorAlias Information
            if (input.VendorAlias != null)
            {
                foreach (var vendorAlias in input.VendorAlias)
                {
                    if (!string.IsNullOrEmpty(vendorAlias.AliasName))
                    {
                        var vendorAliasUnit = vendorAlias.MapTo<VendorAliasUnit>();
                        vendorAliasUnit.VendorId = vendorUnit.Id;
                        await _vendorAliasUnitManager.CreateAsync(vendorAliasUnit);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
            }

            #region Example to show the usage of Event Bus as well Unit of Work Completion

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Vendor is Added*/
            };

            EventBus.Register<EntityChangedEventData<VendorUnitDto>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when Vendor is added
                });

            #endregion


            var VendorUnitDto = vendorUnit.MapTo<VendorUnitDto>();
            VendorUnitDto.VendorId = vendorUnit.Id;
            return VendorUnitDto;

        }

        /// <summary>
        /// Delete Vendor,VendorAlias and its Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payables_Vendors_Delete)]
        [UnitOfWork]
        public async Task DeleteVendorUnit(IdInput input)
        {
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Vendor,
                ObjectId = input.Id
            };
            await _addressAppService.DeleteAddressUnit(dto);
            await _vendorAliasUnitManager.DeleteAsync(Convert.ToInt32(dto.ObjectId));
            await _vendorUnitManager.DeleteAsync(input.Id);
        }

        /// <summary>
        /// Get the Vendor Details By VendorId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<VendorUnitDto> GetVendorUnitsById(IdInput input)
        {
            var vendorItem = await _vendorUnitRepository.GetAsync(input.Id);
            var addressitems = await _addresRepository.GetAllListAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Vendor);
            var result = vendorItem.MapTo<VendorUnitDto>();
            result.VendorId = vendorItem.Id;
            result.Address = new Collection<AddressUnitDto>();
            for (int i = 0; i < addressitems.Count; i++)
            {
                result.Address.Add(addressitems[i].MapTo<AddressUnitDto>());
                result.Address[i].AddressId = addressitems[i].Id;
            }
            return result;
        }
        /// <summary>
        /// This method is for retrieve the records for showing in the grid with filters and SortOrder
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Payables_Vendors)]
        public async Task<PagedResultOutput<VendorUnitDto>> GetVendorUnits(SearchInputDto input)
        {
            var query = CreateVendorQuery(input);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Vendor.LastName ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            var vendorListDtos = ConvertToVendorDtos(results);
            return new PagedResultOutput<VendorUnitDto>(resultCount, vendorListDtos);
        }

        /// <summary>
        /// Converting vendor to outputdto of a VendorList
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private List<VendorUnitDto> ConvertToVendorDtos(List<VendorAndAddressDto> results)
        {
            return results.Select(
                result =>
                {
                    var dto = result.Vendor.MapTo<VendorUnitDto>();
                    dto.VendorId = result.Vendor.Id;
                    dto.PaymentTerms = result.PaymentTerms;
                    dto.TypeofCurrencyId = result.Vendor.TypeofCurrencyId;
                    dto.TypeofVendorId = result.Vendor.TypeofVendorId;
                    dto.TypeofTaxId = result.Vendor.TypeofTaxId;
                    dto.TypeofPaymentMethod = result.Vendor.TypeofPaymentMethodId != null ? result.Vendor.TypeofPaymentMethodId.ToDisplayName() : "";
                    dto.Typeof1099Box = result.Vendor.Typeof1099BoxId != null ? result.Vendor.Typeof1099BoxId.ToDisplayName() : "";
                    dto.TypeofTax = result.Vendor.TypeofTaxId != null ? result.Vendor.TypeofTaxId.ToDisplayName() : "";
                    dto.TypeofVendor = result.Vendor.TypeofVendorId.ToDisplayName();
                    dto.Address = new Collection<AddressUnitDto>();
                    if (result.Address != null)
                    {
                        dto.Address.Add(result.Address.MapTo<AddressUnitDto>());
                        dto.Address[0].AddressId = result.Address.Id;
                    }
                    dto.VendorAlias = new Collection<VendorAliasUnitDto>();

                    if (result.VendorAlias != null)
                    {
                        foreach (var item in result.VendorAlias)
                        {
                            dto.VendorAlias.Add(item.MapTo<VendorAliasUnitDto>());
                        }
                    }
                    return dto;
                }).ToList();
        }

        private IQueryable<VendorAndAddressDto> CreateVendorQuery(SearchInputDto input)
        {
            var query = from vendor in _vendorUnitRepository.GetAll()
                        join addr in _addresRepository.GetAll() on vendor.Id equals addr.ObjectId
                            into temp
                        from rt in temp.Where(p => p.IsPrimary == true && p.TypeofObjectId == TypeofObject.Vendor).DefaultIfEmpty()
                        join payterms in _vendorPaytermRepository.GetAll() on vendor.PaymentTermsId equals payterms.Id
                            into paymentperms
                        from pt in paymentperms.DefaultIfEmpty()
                        select new VendorAndAddressDto
                        {
                            Vendor = vendor,
                            Address = rt,
                            PaymentTerms = pt.Description,
                            // VendorAlias = vendor.VendorAlias
                        };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Where(item => item.Vendor.OrganizationUnitId == input.OrganizationUnitId || item.Vendor.OrganizationUnitId == null);
            return query;
        }

        /// <summary>
        /// Updating Vendor,vendoralias with Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [AbpAuthorize(AppPermissions.Pages_Payables_Vendors_Edit)]
        [UnitOfWork]
        public async Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input)
        {
            var vendorUnit = await _vendorUnitRepository.GetAsync(input.VendorId);

            // update address Information
            if (!ReferenceEquals(input.Addresses, null))
            {
                foreach (var address in input.Addresses)
                {
                    if (address.AddressId != 0)
                        await _addressAppService.UpdateAddressUnit(address);
                    else
                    {
                        if (!string.IsNullOrEmpty(address.Line1) || !string.IsNullOrEmpty(address.Line2) ||
                        !string.IsNullOrEmpty(address.Line4) || !string.IsNullOrEmpty(address.Line4) ||
                        !string.IsNullOrEmpty(address.State) || !string.IsNullOrEmpty(address.Country) ||
                        !string.IsNullOrEmpty(address.Email) || !string.IsNullOrEmpty(address.Phone1) ||
                        !string.IsNullOrEmpty(address.ContactNumber))
                        {
                            address.TypeofObjectId = TypeofObject.Vendor;
                            address.ObjectId = input.VendorId;
                            //AutoMapper.Mapper.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                            //var config = new MapperConfiguration(cfg => {
                            //    cfg.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                            //});
                            await
                                _addressAppService.CreateAddressUnit(
                                    AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(address));
                        }
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();

                }
            }


            //Update vendorAlias Information
            if (input.VendorAlias != null)
            {
                foreach (var vendorAlias in input.VendorAlias)
                {
                    if (!string.IsNullOrEmpty(vendorAlias.AliasName))
                    {
                        VendorAliasUnit vendorAliasUnit = new VendorAliasUnit();
                        vendorAliasUnit.VendorId = vendorUnit.Id;
                        vendorAliasUnit.AliasName = vendorAlias.AliasName;
                        if (vendorAlias.VendorAliasId.Equals(0))
                        {
                            await _vendorAliasUnitManager.CreateAsync(vendorAliasUnit);
                        }
                        else
                        {
                            vendorAliasUnit.Id = vendorAlias.VendorAliasId;
                            await _vendorAliasUnitManager.UpdateAsync(vendorAliasUnit);
                        }
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
            }


            #region Setting the values to be updated

            vendorUnit.LastName = input.LastName;
            vendorUnit.PayToName = input.PayToName;
            vendorUnit.VendorAccountInfo = input.VendorAccountInfo;
            vendorUnit.CreditLimit = input.CreditLimit;
            vendorUnit.PaymentTermsId = input.PaymentTermsId;
            vendorUnit.IsCorporation = input.IsCorporation;
            vendorUnit.IsIndependentContractor = input.IsIndependentContractor;
            vendorUnit.Isw9OnFile = input.Isw9OnFile;
            vendorUnit.TypeofVendorId = input.TypeofvendorId;
            vendorUnit.EDDContractStartDate = input.EDDContractStartDate;
            vendorUnit.EDDContractStopDate = input.EDDContractStopDate;
            vendorUnit.WorkRegion = input.WorkRegion;
            vendorUnit.ACHBankName = input.ACHBankName;
            vendorUnit.ACHWireFromBankName = input.ACHWireFromBankName;
            vendorUnit.ACHWireFromSwiftCode = input.ACHWireFromSwiftCode;
            vendorUnit.ACHWireFromAccountNumber = input.ACHWireFromAccountNumber;
            vendorUnit.ACHWireToSwiftCode = input.ACHWireToSwiftCode;
            vendorUnit.ACHWireToAccountNumber = input.ACHWireToAccountNumber;
            vendorUnit.ACHWireToIBAN = input.ACHWireToIBAN;
            vendorUnit.OrganizationUnitId = input.OrganizationUnitId;
            vendorUnit.VendorNumber = input.VendorNumber;
            vendorUnit.FedralTaxId = input.FedralTaxId;
            vendorUnit.FirstName = input.FirstName;
            vendorUnit.DbaName = input.DbaName;
            vendorUnit.SSNTaxId = input.SSNTaxId;
            vendorUnit.TypeofPaymentMethodId = input.TypeofPaymentMethodId;
            vendorUnit.TypeofCurrencyId = input.TypeofCurrencyId;
            vendorUnit.Is1099 = input.Is1099;
            vendorUnit.ACHRoutingNumber = input.ACHRoutingNumber;
            vendorUnit.Typeof1099BoxId = input.Typeof1099BoxId;
            vendorUnit.EDDConctractAmount = input.EDDConctractAmount;
            vendorUnit.IsEDDContractOnGoing = input.IsEDDContractOnGoing;
            vendorUnit.ACHAccountNumber = input.ACHAccountNumber;
            vendorUnit.ACHWireFromBankAddress = input.ACHWireFromBankAddress;
            vendorUnit.ACHWireToBankName = input.ACHWireToBankName;
            vendorUnit.ACHWireToBeneficiary = input.ACHWireToBeneficiary;
            vendorUnit.IsApproved = input.IsApproved;
            vendorUnit.OrganizationUnitId = input.OrganizationUnitId;
            vendorUnit.IsActive = input.IsActive;
            vendorUnit.TypeofTaxId = input.TypeofTaxId;
            vendorUnit.BillingAccount = input.BillingAccount;
            vendorUnit.GLAccountId = input.GLAccountId;
            vendorUnit.AccountId = input.AccountId;
            vendorUnit.Notes = input.Notes;
            vendorUnit.JobId = input.JobId;
            vendorUnit.TaxCreditId = input.TaxCreditId;
            #endregion

            await _vendorUnitManager.UpdateAsync(vendorUnit);

            await CurrentUnitOfWork.SaveChangesAsync();

            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of Vendor is Added*/
            };

            EventBus.Register<EntityChangedEventData<CoaUnit>>(
                eventData =>
                {
                    // http://www.aspnetboilerplate.com/Pages/Documents/EventBus-Domain-Events#DocTriggerEvents
                    //Do something when Vendor is added
                });


            var VendorUnitDto = vendorUnit.MapTo<VendorUnitDto>();
            VendorUnitDto.VendorId = vendorUnit.Id;
            return VendorUnitDto;
        }

        /// <summary>
        /// Get TypeofPaymentMethod 
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofPaymentMethodList()
        {
            return EnumList.GetTypeofPaymentMethodList();
        }

        /// <summary>
        /// Get Typeof1099T4
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeof1099T4List()
        {
            return EnumList.GetTypeof1099T4List();
        }

        /// <summary>
        /// Get TypeofVendor
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofVendorList()
        {
            return EnumList.GetTypeofVendorList();
        }

        /// <summary>
        /// Get TypeofAddress
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofAddressList()
        {
            return EnumList.GetTypeofAddressList();
        }

        /// <summary>
        /// Get TypeofObject
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeofObjectList()
        {
            return EnumList.GetTypeofObjectList();
        }

        /// <summary>
        /// Get TypeOfTax
        /// </summary>
        /// <returns></returns>
        public List<NameValueDto> GetTypeOfTaxList()
        {
            return EnumList.GetTypeOfTaxList();
        }

        /// <summary>
        /// Get PaymentTerms
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetPaymentTermsList()
        {
            var payterms = await _vendorPaytermRepository.GetAll().Select(u => new NameValueDto { Name = u.Description, Value = u.Id.ToString() }).ToListAsync();
            return payterms;
        }

        /// <summary>
        /// Get CountryList
        /// </summary>
        /// <returns></returns>
        public async Task<List<CountryListDto>> GetCountryList()
        {
            var countryList = await (from country in _countryRepository.GetAll()
                                     select new CountryListDto
                                     {
                                         Description = country.Description,
                                         CountryId = country.Id,
                                         IsoCode = country.TwoLetterAbbreviation
                                     }).ToListAsync();
            return countryList;
        }

        /// <summary>
        /// Get RegionList
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionListDto>> GetRegionList()
        {
            var regionList = await _regionRepository.GetAll().Select(u => new RegionListDto
            {
                Description = u.Description + " (" + u.RegionAbbreviation + ")",
                RegionId = u.Id,
                StateCode = u.RegionAbbreviation
            }).ToListAsync();
            return regionList;
        }


        /// <summary>
        /// Get the ProjectCoa AccountList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<AccountCacheItem>> GetAccountsList(AutoSearchInput input)
        {

            var accountList = await _accountCache.GetAccountCacheItemAsync(
                 CacheKeyStores.CalculateCacheKey(CacheKeyStores.AccountKey, Convert.ToInt32(_customAppSession.TenantId)));

            return accountList.ToList().WhereIf(!string.IsNullOrEmpty(input.Query),
                p => p.Caption.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper()) || p.AccountNumber.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())
                || p.Description.EmptyIfNull().ToUpper().Contains(input.Query.ToUpper())).Where(p => p.IsCorporate == input.Value).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<VendorAliasUnitDto>> GetVendorAliasUnits(AutoSearchInput input)
        {
            var query = _vendorAliasUnitRepository.GetAll()
                    .Where(au => au.VendorId == input.VendorId);

            var items = await query.ToListAsync();

            return new ListResultOutput<VendorAliasUnitDto>(
                items.Select(item =>
                {
                    var dto = item.MapTo<VendorAliasUnitDto>();
                    dto.VendorAliasId = item.Id;
                    return dto;
                }).ToList());
        }


        /// <summary>
        /// delete vendoralias
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteVendorAliasUnit(IdInput input)
        {
            await _vendorAliasUnitManager.DeleteAsync(input.Id);
        }

    }

}



