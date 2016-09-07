using System.Collections.Generic;
using Abp.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.JobCosting;
using System.Data.Entity;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using System.Collections.ObjectModel;
using AutoMapper;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Sessions;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.CreditCard
{
    /// <summary>
    /// 
    /// </summary>
    [AbpAuthorize]
    public class CreditCardCompanyAppService : CORPACCOUNTINGServiceBase, ICreditCardCompanyAppService
    {
        private readonly BankAccountUnitManager _bankAccountUnitManager;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<BankAccountUnit, long> _bankAccountUnitRepository;
        private readonly IRepository<AddressUnit, long> _addressUnitRepository;
        private readonly IRepository<AccountUnit, long> _accountUnitRepository;
        private readonly IRepository<JobUnit, int> _jobUnitRepository;
        private readonly IRepository<TypeOfUploadFileUnit, int> _typeOfUploadFileUnitRepository;
        private readonly IRepository<TypeOfCheckStockUnit, int> _typeOfCheckStockUnitRepository;
        private readonly IRepository<VendorUnit, int> _vendorUnitRepository;
        private readonly IRepository<BatchUnit, int> _batchUnitRepository;
        private readonly IRepository<BankAccountPaymentRangeUnit> _bankAccountPaymentRangeRepository;
        private readonly BankAccountPaymentRangeUnitManager _bankAccountPaymentRangeUnitManager;
        private readonly IRepository<CoaUnit> _coaUnitRepository;
        private readonly IAccountCache _accountCache;
        private readonly CustomAppSession _customAppSession;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankAccountUnitManager"></param>
        /// <param name="bankAccountUnitRepository"></param>
        /// <param name="addressAppService"></param>
        /// <param name="addressUnitRepository"></param>
        /// <param name="accountUnitRepository"></param>
        /// <param name="jobUnitRepository"></param>
        /// <param name="typeOfUploadFileUnitRepository"></param>
        /// <param name="typeOfCheckStockUnitRepository"></param>
        /// <param name="vendorUnitRepository"></param>
        /// <param name="batchUnitRepository"></param>
        /// <param name="bankAccountPaymentRangeUnit"></param>
        /// <param name="bankAccountPaymentRangeUnitManager"></param>
        /// <param name="accountCache"></param>
        /// <param name="customAppSession"></param>
        /// <param name="coaUnitRepository"></param>
        public CreditCardCompanyAppService(BankAccountUnitManager bankAccountUnitManager, IRepository<BankAccountUnit, long> bankAccountUnitRepository,
            IAddressUnitAppService addressAppService, IRepository<AddressUnit, long> addressUnitRepository, IRepository<AccountUnit, long> accountUnitRepository,
            IRepository<JobUnit, int> jobUnitRepository, IRepository<TypeOfUploadFileUnit, int> typeOfUploadFileUnitRepository,
            IRepository<TypeOfCheckStockUnit, int> typeOfCheckStockUnitRepository, IRepository<VendorUnit, int> vendorUnitRepository,
            IRepository<BatchUnit, int> batchUnitRepository, IRepository<BankAccountPaymentRangeUnit> bankAccountPaymentRangeUnit,
            BankAccountPaymentRangeUnitManager bankAccountPaymentRangeUnitManager, IAccountCache accountCache, CustomAppSession customAppSession,
            IRepository<CoaUnit> coaUnitRepository)
        {
            _bankAccountUnitManager = bankAccountUnitManager;
            _bankAccountUnitRepository = bankAccountUnitRepository;
            _addressUnitRepository = addressUnitRepository;
            _addressAppService = addressAppService;
            _accountUnitRepository = accountUnitRepository;
            _jobUnitRepository = jobUnitRepository;
            _typeOfCheckStockUnitRepository = typeOfCheckStockUnitRepository;
            _typeOfUploadFileUnitRepository = typeOfUploadFileUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _batchUnitRepository = batchUnitRepository;
            _bankAccountPaymentRangeRepository = bankAccountPaymentRangeUnit;
            _bankAccountPaymentRangeUnitManager = bankAccountPaymentRangeUnitManager;
            _accountCache = accountCache;
            _customAppSession = customAppSession;
            _coaUnitRepository = coaUnitRepository;
        }


        /// <summary>
        /// Create the CreditCard Company.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_CreditCard_Entry_CreditCardCompanies_Create)]
        public async Task<IdOutputDto<long>> CreateCCCompanyDocumentUnit(CreateBankAccountUnitInput input)
        {
            var bankAccountUnit = input.MapTo<BankAccountUnit>();
            long id = await _bankAccountUnitManager.CreateAsync(bankAccountUnit);


            #region Address Insertion
            if (!ReferenceEquals(input.Addresses, null))
            {
                foreach (var address in input.Addresses)
                {
                    if (!string.IsNullOrEmpty(address.Line1) || !string.IsNullOrEmpty(address.Line2) ||
                          !string.IsNullOrEmpty(address.Line4) || !string.IsNullOrEmpty(address.Line4) ||
                          !string.IsNullOrEmpty(address.State) || !string.IsNullOrEmpty(address.Country) ||
                          !string.IsNullOrEmpty(address.Email) || !string.IsNullOrEmpty(address.Phone1) ||
                          !string.IsNullOrEmpty(address.Website))
                    {
                        address.ObjectId = id;
                        address.TypeofObjectId = TypeofObject.CreditCardBank;
                        await _addressAppService.CreateAddressUnit(address);
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return new IdOutputDto<long> { Id = id };
            #endregion
        }


        /// <summary>
        ///  Update the CreditCard Company 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_CreditCard_Entry_CreditCardCompanies_Edit)]
        public async Task UpdateCCCompanyDocumentUnit(UpdateBankAccountUnitInput input)
        {
            var bankAccountUnit = await _bankAccountUnitRepository.GetAsync(input.BankAccountId);

            Mapper.Map(input, bankAccountUnit);
            await _bankAccountUnitManager.UpdateAsync(bankAccountUnit);

            #region Address Insertion
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
                         !string.IsNullOrEmpty(address.Website))
                        {
                            address.TypeofObjectId = TypeofObject.CreditCardBank;
                            address.ObjectId = input.BankAccountId;
                            //AutoMapper.Mapper.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                            await
                                _addressAppService.CreateAddressUnit(
                                    AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(address));
                        }
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();

                }
            }
            #endregion
        }

        /// <summary>
        /// Delete the CreditCard Company
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_CreditCard_Entry_CreditCardCompanies_Delete)]
        public async Task DeleteCCCompanyDocumentUnit(IdInput<long> input)
        {
            await _bankAccountPaymentRangeRepository.DeleteAsync(p => p.BankAccountId == input.Id);
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput
            {
                TypeofObjectId = TypeofObject.CreditCardBank,
                ObjectId = input.Id
            };
           // await _addressAppService.DeleteAddressUnit(dto);
            await _bankAccountUnitManager.DeleteAsync(input);
        }

        /// <summary>
        /// Get the list of all Credit Card Companies
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_CreditCard_Entry_CreditCardCompanies)]
        public async Task<PagedResultOutput<BankAccountUnitDto>> GetCreditCardCompanies(SearchInputDto input)
        {
            var ccCompanyQuery = CreateCcCompanyQuery(input);
            var resultCount = await ccCompanyQuery.CountAsync();
            var results = await ccCompanyQuery
                .AsNoTracking()
                .OrderBy(Helper.GetSort("BankAccount.BankAccountName ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            var response = ConvertToCcCompanyDtos(results);

            return new PagedResultOutput<BankAccountUnitDto>(resultCount, response);
        }

        /// <summary>
        /// Converting Customer to outputdto of a CustomerUnitDto List
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private List<BankAccountUnitDto> ConvertToCcCompanyDtos(List<BankAccountAndAddressDto> results)
        {
            return results.Select(
                result =>
                {
                    var dto = result.BankAccount.MapTo<BankAccountUnitDto>();
                    dto.BankAccountId = result.BankAccount.Id;
                    dto.BatchDesc = result.Batch;
                    dto.TypeOfBankAccountDesc = result.BankAccount.TypeOfBankAccountId.ToDisplayName();
                    return dto;
                }).ToList();
        }

        /// <summary>
        /// Get the CreditCard Company Details By Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BankAccountUnitDto> GetCcCompanyById(IdInput<long> input)
        {
            var bankAccountItem = await _bankAccountUnitRepository.GetAsync(input.Id);
            var addressitems = await _addressUnitRepository.GetAllListAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.CreditCardBank);
            var result = bankAccountItem.MapTo<BankAccountUnitDto>();
            result.BankAccountId = bankAccountItem.Id;
            result.Address = new Collection<AddressUnitDto>();
            for (int i = 0; i < addressitems.Count; i++)
            {
                result.Address.Add(addressitems[i].MapTo<AddressUnitDto>());
                result.Address[i].AddressId = addressitems[i].Id;
            }
            return result;
        }

        private IQueryable<BankAccountAndAddressDto> CreateCcCompanyQuery(SearchInputDto input)
        {
            var ccCompanyQuery = from bankAccount in _bankAccountUnitRepository.GetAll()
                                 join batch in _batchUnitRepository.GetAll() on bankAccount.BatchId equals batch.Id into batch
                                 from batchacc in batch.DefaultIfEmpty()
                                 select new BankAccountAndAddressDto
                                 {
                                     BankAccount = bankAccount,
                                     Batch = batchacc.Description,
                                 };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    ccCompanyQuery = Helper.CreateFilters(ccCompanyQuery, mapSearchFilters);
            }
            return ccCompanyQuery;
        }

       
    }
}
