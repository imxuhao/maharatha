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

namespace CAPS.CORPACCOUNTING.Banking
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class BankAccountUnitAppService : CORPACCOUNTINGServiceBase, IBankAccountUnitAppService
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



        private readonly IUnitOfWorkManager _unitOfWorkManager;


        public BankAccountUnitAppService(BankAccountUnitManager bankAccountUnitManager, IRepository<BankAccountUnit, long> bankAccountUnitRepository, IUnitOfWorkManager unitOfWorkManager,
                                        IAddressUnitAppService addressAppService, IRepository<AddressUnit, long> addressUnitRepository, IRepository<AccountUnit, long> accountUnitRepository,
                                         IRepository<JobUnit, int> jobUnitRepository, IRepository<TypeOfUploadFileUnit, int> typeOfUploadFileUnitRepository, IRepository<TypeOfCheckStockUnit, int> typeOfCheckStockUnitRepository,
                                         IRepository<VendorUnit, int> vendorUnitRepository, IRepository<BatchUnit, int> batchUnitRepository)
        {
            _bankAccountUnitManager = bankAccountUnitManager;
            _bankAccountUnitRepository = bankAccountUnitRepository;
            _addressUnitRepository = addressUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
            _accountUnitRepository = accountUnitRepository;
            _jobUnitRepository = jobUnitRepository;
            _typeOfCheckStockUnitRepository = typeOfCheckStockUnitRepository;
            _typeOfUploadFileUnitRepository = typeOfUploadFileUnitRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _batchUnitRepository = batchUnitRepository;
        }


        /// <summary>
        /// Create the BankAccount.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<BankAccountUnitDto> CreateBankAccountUnit(CreateBankAccountUnitInput input)
        {
            var bankAccountUnit = input.MapTo<BankAccountUnit>();
            await _bankAccountUnitManager.CreateAsync(bankAccountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (!ReferenceEquals(input.Addresses, null))
            {
                foreach (var address in input.Addresses)
                {
                    if (address.Line1 != null || address.Line2 != null || address.Line4 != null ||
                        address.Line4 != null || address.State != null ||
                        address.Country != null || address.Email != null || address.Phone1 != null || address.Website != null)
                    {
                        address.ObjectId = bankAccountUnit.Id;
                        address.TypeofObjectId = TypeofObject.Bank;
                        await _addressAppService.CreateAddressUnit(address);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
            }
            return bankAccountUnit.MapTo<BankAccountUnitDto>();


        }


        /// <summary>
        ///  Update the BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<BankAccountUnitDto> UpdateBankAccountUnit(UpdateBankAccountUnitInput input)
        {
            var bankAccountUnit = await _bankAccountUnitRepository.GetAsync(input.BankAccountId);
            Mapper.CreateMap<UpdateBankAccountUnitInput, BankAccountUnit>()
                          .ForMember(u => u.Id, ap => ap.MapFrom(src => src.BankAccountId));
            Mapper.Map(input, bankAccountUnit);
            await _bankAccountUnitManager.UpdateAsync(bankAccountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

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
                            address.TypeofObjectId = TypeofObject.Bank;
                            address.ObjectId = input.BankAccountId;
                            await
                                _addressAppService.CreateAddressUnit(
                                    AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(address));
                        }
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();

                }
            }


            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of employee is Added*/
            };

            return bankAccountUnit.MapTo<BankAccountUnitDto>();

        }


        /// <summary>
        /// Delete the BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task DeleteBankAccountUnit(IdInput input)
        {
            await _bankAccountUnitManager.DeleteAsync(input);
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Emp,
                ObjectId = input.Id
            };
            await _addressAppService.DeleteAddressUnit(dto);
        }

        /// <summary>
        /// Get the list of all BankAccounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<BankAccountAndAddressDto>> GetBankAccountUnits(SearchInputDto input)
        {

            var bankAccountUnitQuery = CreateBankAccountQuery(input);
            bankAccountUnitQuery = bankAccountUnitQuery.Where(item => item.BankAccount.OrganizationUnitId == input.OrganizationUnitId || item.BankAccount.OrganizationUnitId == null);
            var resultCount = await bankAccountUnitQuery.CountAsync();
            var results = await bankAccountUnitQuery
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var mapEnumResults = (from value in results
                                 select new BankAccountAndAddressDto
                                 {
                                     BankAccount = value.BankAccount,
                                     Address = value.Address,
                                     TypeOfBankAccount = value.BankAccount.TypeOfBankAccountId.ToDisplayName(),
                                     TypeOfInactiveStatus = value.BankAccount.TypeOfInactiveStatusId != null? value.BankAccount.TypeOfInactiveStatusId.ToDisplayName():""
                                 }).ToList();

            return new PagedResultOutput<BankAccountAndAddressDto>(resultCount, mapEnumResults);
        }

        /// <summary>
        /// Get the BankAccount Details By BankAccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BankAccountUnitDto> GetBankAccountUnitsById(IdInput input)
        {
            var bankAccountItem = await _bankAccountUnitRepository.GetAsync(input.Id);
            var addressitems = await _addressUnitRepository.GetAllListAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Bank);
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

        private IQueryable<BankAccountAndAddressDto> CreateBankAccountQuery(SearchInputDto input)
        {
            var bankAccountUnitQuery = from bankAccount in _bankAccountUnitRepository.GetAll()
                                       join address in _addressUnitRepository.GetAll() on bankAccount.Id equals address.ObjectId into bank
                                       from addr in bank.Where(p => p.IsPrimary == true && p.TypeofObjectId == TypeofObject.Bank).DefaultIfEmpty()
                                       join account in _accountUnitRepository.GetAll() on bankAccount.AccountId equals account.Id into acc
                                       from pt in acc.DefaultIfEmpty()
                                       join job in _jobUnitRepository.GetAll() on bankAccount.JobId equals job.Id into job
                                       from jobacc in job.DefaultIfEmpty()
                                       join clearingaccount in _accountUnitRepository.GetAll() on bankAccount.ClearingAccountId equals clearingaccount.Id into clearingaccount
                                       from clearAcc in clearingaccount.DefaultIfEmpty()
                                       join clearingjob in _jobUnitRepository.GetAll() on bankAccount.ClearingJobId equals clearingjob.Id into clearingjob
                                       from clearjobacc in clearingjob.DefaultIfEmpty()
                                       join vendor in _vendorUnitRepository.GetAll() on bankAccount.VendorId equals vendor.Id into vendor
                                       from vendoracc in vendor.DefaultIfEmpty()
                                       join typeOfUploadFile in _typeOfUploadFileUnitRepository.GetAll() on bankAccount.TypeOfUploadFileId equals typeOfUploadFile.Id into typeOfUploadFile
                                       from typfacc in typeOfUploadFile.DefaultIfEmpty()
                                       join typeOfCheckStock in _typeOfCheckStockUnitRepository.GetAll() on bankAccount.TypeOfCheckStockId equals typeOfCheckStock.Id into typeOfCheckStock
                                       from typcacc in typeOfCheckStock.DefaultIfEmpty()
                                       join batch in _batchUnitRepository.GetAll() on bankAccount.BatchId equals batch.Id into batch
                                       from batchacc in batch.DefaultIfEmpty()
                                       select new BankAccountAndAddressDto
                                       {
                                           BankAccount = bankAccount,
                                           Address = addr,
                                           Account = pt.Caption,
                                           Job = jobacc.Caption,
                                           ClearingAccount = clearAcc.Caption,
                                           ClearingJob = clearjobacc.Caption,
                                           Vendor = vendoracc.VendorNumber,
                                           TypeOfUploadFile = typfacc.Description,
                                           TypeofCheckStock = typcacc.Description,
                                           Batch = batchacc.Description
                                       };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    bankAccountUnitQuery = Helper.CreateFilters(bankAccountUnitQuery, mapSearchFilters);
            }
            return bankAccountUnitQuery;
        }

    }
}
