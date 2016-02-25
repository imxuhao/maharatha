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
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Authorization;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class VendorUnitAppService : CORPACCOUNTINGServiceBase, IVendorUnitAppService
    {
        private readonly VendorUnitManager _vendorUnitManager;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit, long> _addresRepository;
        private readonly IRepository<VendorPaymentTermUnit> _vendorPaytermRepository;



        public VendorUnitAppService(VendorUnitManager vendorUnitManager, IRepository<VendorUnit> vendorUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAddressUnitAppService addressAppService,
            IRepository<AddressUnit, long> addresRepository, IRepository<VendorPaymentTermUnit> vendorPaytermRepository)
        {
            _vendorUnitManager = vendorUnitManager;
            _vendorUnitRepository = vendorUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
            _addresRepository = addresRepository;
            _vendorPaytermRepository = vendorPaytermRepository;
        }

        public IEventBus EventBus { get; set; }        

        /// <summary>
        /// Creating the Vendor with Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<VendorUnitDto> CreateVendorUnit(CreateVendorUnitInput input)
        {
            var vendorUnit = new VendorUnit(lastname: input.LastName, firstname: input.FirstName,
                paytoname: input.PayToName, dbaname: input.DbaName, vendornumber: input.VendorNumber,
                vendoraccountinfo: input.VendorAccountInfo, ssntaxid: input.SSNTaxId, fedraltaxid: input.FedralTaxId,
                creditlimit: input.CreditLimit, typeofpaymentmethod: input.TypeofPaymentMethod,
                paymenttermsid: input.PaymentTermsId, typeofcurrency: input.TypeofCurrency,
                iscorporation: input.IsCorporation, is1099: input.Is1099,
                isindependentcontractor: input.IsIndependentContractor,
                isw9Onfile: input.Isw9OnFile, achroutingnumber: input.ACHRoutingNumber,
                typeofvendorid: input.TypeOFvendorId, typeof1099Box: input.TypeOF1099Box,
                eddcontractstartdate: input.EDDContractStartDate,
                eddcontractstopdate: input.EDDContractStopDate, eddconctractamount: input.EDDConctractAmount,
                workregion: input.WorkRegion, iseddcontractongoing: input.IsEDDContractOnGoing,
                achbankname: input.ACHBankName, achaccountnumber: input.ACHAccountNumber,
                achwirefrombankname: input.ACHWireFromBankName, achwirefrombankaddress: input.ACHWireFromBankAddress,
                achwirefromswiftcode: input.ACHWireFromSwiftCode,
                achwirefromaccountnumber: input.ACHWireFromAccountNumber, achwiretobankname: input.ACHWireToBankName,
                achwiretoswiftcode: input.ACHWireToSwiftCode, achwiretobeneficiary: input.ACHWireToBeneficiary,
                achwiretoaccountnumber: input.ACHWireToAccountNumber,
                achwiretoiban: input.ACHWireToIBAN, isactive: input.IsActive, isapproved: input.IsApproved,
                organizationunitid: input.OrganizationUnitId);
            await _vendorUnitManager.CreateAsync(vendorUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (input.InputAddress != null)
            {
                foreach (var address in input.InputAddress)
                {
                    if (address.Line1 != null || address.Line2 != null || address.Line4 != null ||
                        address.Line4 != null || address.State != null ||
                        address.Country != null || address.Email != null || address.Phone1 != null ||
                        address.Website != null)
                    {
                        address.ObjectId = vendorUnit.Id;
                        await _addressAppService.CreateAddressUnit(address);
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

            return vendorUnit.MapTo<VendorUnitDto>();
        }

        /// <summary>
        /// Delete Vendor and its Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task DeleteVendorUnit(IdInput input)
        {
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Vendor,
                ObjectId = input.Id
            };
            await _addressAppService.DeleteAddressUnit(dto);
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
            var addressitems = await  _addresRepository.GetAllListAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Vendor);
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
        public async Task<PagedResultOutput<VendorUnitDto>> GetVendorUnits(GetVendorInput input)
        {
            var query = CreateVendorQuery(input);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var auditLogListDtos = ConvertToVendorDtos(results);
            return new PagedResultOutput<VendorUnitDto>(resultCount, auditLogListDtos);
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
                    dto.PaymentTermDescription = result.PaymentTerms;
                    dto.Address = new Collection<AddressUnitDto>();
                    if (result.Address != null)
                    {
                        dto.Address.Add(result.Address.MapTo<AddressUnitDto>());
                        dto.Address[0].AddressId = result.Address.Id;
                    }
                    return dto;
                }).ToList();
        }

        private IQueryable<VendorAndAddressDto> CreateVendorQuery(GetVendorInput input)
        {
            var query = from vendor in _vendorUnitRepository.GetAll()
                join addr in _addresRepository.GetAll() on vendor.Id equals addr.ObjectId
                    into temp
                from rt in temp.Where(p => p.IsPrimary == true && p.TypeofObjectId==TypeofObject.Vendor).DefaultIfEmpty()
                join payterms in _vendorPaytermRepository.GetAll() on vendor.Id equals payterms.Id
                    into paymentperms
                from pt in paymentperms.DefaultIfEmpty()
                select new VendorAndAddressDto {Vendor = vendor, Address = rt, PaymentTerms = pt.Description};

            query = query
                .WhereIf(input.OrganizationUnitId != null,
                    item => item.Vendor.OrganizationUnitId == input.OrganizationUnitId)
                .WhereIf(!input.FirstName.IsNullOrWhiteSpace(), item => item.Vendor.FirstName.Contains(input.FirstName))
                .WhereIf(!input.LastName.IsNullOrWhiteSpace(), item => item.Vendor.LastName.Contains(input.LastName))
                .WhereIf(!input.SSNTaxId.IsNullOrWhiteSpace(), item => item.Vendor.SSNTaxId.Contains(input.SSNTaxId))
                .WhereIf(!input.PayToName.IsNullOrWhiteSpace(), item => item.Vendor.PayToName.Contains(input.PayToName))
                .WhereIf(!input.VendorNumber.IsNullOrWhiteSpace(),
                    item => item.Vendor.VendorNumber.Contains(input.VendorNumber))
                .WhereIf(!input.VendorAccountInfo.IsNullOrWhiteSpace(),
                    item => item.Vendor.VendorAccountInfo.Contains(input.VendorAccountInfo))
                .WhereIf(!input.FedralTaxId.IsNullOrWhiteSpace(),
                    item => item.Vendor.FedralTaxId.Contains(input.FedralTaxId))
                .WhereIf(input.Typeof1099Box != null, item => item.Vendor.Typeof1099Box == input.Typeof1099Box)
                .WhereIf(input.TypeofVendorId != null, item => item.Vendor.TypeofVendorId == input.TypeofVendorId)
                .WhereIf(!input.PhoneorEmail.IsNullOrWhiteSpace(), item => item.Address.Phone1 == input.PhoneorEmail)
                .WhereIf(!input.PhoneorEmail.IsNullOrWhiteSpace(), item => item.Address.Phone2 == input.PhoneorEmail)
                .WhereIf(!input.PhoneorEmail.IsNullOrWhiteSpace(), item => item.Address.Email == input.PhoneorEmail);
            return query;
        }

        /// <summary>
        /// Updating Vendor with Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input)
        {
            var vendorUnit = await _vendorUnitRepository.GetAsync(input.VendorId);
            foreach (var address in input.InputAddress)
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
                        address.TypeofObjectId = TypeofObject.Vendor;
                        address.ObjectId = input.VendorId;
                        AutoMapper.Mapper.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                        await
                            _addressAppService.CreateAddressUnit(
                                AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(address));
                    }
                }
                await CurrentUnitOfWork.SaveChangesAsync();

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
            vendorUnit.TypeofPaymentMethod = input.TypeofPaymentMethod;
            vendorUnit.TypeofCurrency = input.TypeofCurrency;
            vendorUnit.Is1099 = input.Is1099;
            vendorUnit.ACHRoutingNumber = input.ACHRoutingNumber;
            vendorUnit.Typeof1099Box = input.Typeof1099Box;
            vendorUnit.EDDConctractAmount = input.EDDConctractAmount;
            vendorUnit.IsEDDContractOnGoing = input.IsEDDContractOnGoing;
            vendorUnit.ACHAccountNumber = input.ACHAccountNumber;
            vendorUnit.ACHWireFromBankAddress = input.ACHWireFromBankAddress;
            vendorUnit.ACHWireToBankName = input.ACHWireToBankName;
            vendorUnit.ACHWireToBeneficiary = input.ACHWireToBeneficiary;
            vendorUnit.IsApproved = input.IsApproved;
            vendorUnit.OrganizationUnitId = input.OrganizationUnitId;
            vendorUnit.IsActive = input.IsActive;

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

            return vendorUnit.MapTo<VendorUnitDto>();
        }

    }
}



