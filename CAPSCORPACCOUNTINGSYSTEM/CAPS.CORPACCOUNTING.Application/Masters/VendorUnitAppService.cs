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

namespace CAPS.CORPACCOUNTING.Masters
{
    public class VendorUnitAppService : CORPACCOUNTINGServiceBase, IVendorUnitAppService
    {
        private readonly VendorUnitManager _vendorUnitManager;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit,long> _addresRepository;

        public VendorUnitAppService(VendorUnitManager vendorUnitManager, IRepository<VendorUnit> vendorUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAddressUnitAppService addressAppService, IRepository<AddressUnit, long> addresRepository)
        {
            _vendorUnitManager = vendorUnitManager;
            _vendorUnitRepository = vendorUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
            _addresRepository = addresRepository;
           
        }
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// This method is for testing to Insert data in vendor  with 2 addresses.After UI development we need to remove this method.
        /// using this method we are calling CreateVendorUnit to insert vendor and Addresss Data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task InsertVendorData(CreateVendorUnitInput input)
        {
            CreateAddressUnitInput vendorAddr1 = new CreateAddressUnitInput();
            vendorAddr1.TypeofObjectId = TypeofObject.Vendor;
            vendorAddr1.AddressTypeId = TypeofAddress.PrimaryContact;
            vendorAddr1.Line1 = "Address1";
            CreateAddressUnitInput vendorAddr2 = new CreateAddressUnitInput();
            vendorAddr2.TypeofObjectId = TypeofObject.Vendor;
            vendorAddr2.AddressTypeId = TypeofAddress.Home;
            vendorAddr2.Line1 = "Address2";

            input.InputAddress = new List<CreateAddressUnitInput>();
            input.InputAddress.Add(vendorAddr1);
            input.InputAddress.Add(vendorAddr2);
            await CreateVendorUnit(input);
        }
        /// <summary>
        /// This method is  for testing to update vendor data with addresses.After UI development we need to remove this method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdatedVendorData(UpdateVendorUnitInput input)
        {

            UpdateAddressUnitInput vendorAddr1 = new UpdateAddressUnitInput();
            vendorAddr1.TypeofObjectId = TypeofObject.Vendor;
            vendorAddr1.AddressTypeId = TypeofAddress.PrimaryContact;
            vendorAddr1.Email = "test@gmail.com";
            vendorAddr1.Website = "https://www.google.co.in";
            vendorAddr1.AddressId = 1;
            vendorAddr1.ObjectId = input.VendorId;
            vendorAddr1.IsPrimary = true;

            UpdateAddressUnitInput vendorAddr2 = new UpdateAddressUnitInput();
            vendorAddr2.TypeofObjectId = TypeofObject.Vendor;
            vendorAddr2.AddressTypeId = TypeofAddress.Business;
            vendorAddr2.Email = "test1@gmail.com";
            vendorAddr2.AddressId = 2;
            vendorAddr2.ObjectId = input.VendorId;

            input.InputAddress = new List<UpdateAddressUnitInput>();
            input.InputAddress.Add(vendorAddr1);
            input.InputAddress.Add(vendorAddr2);
            await UpdateVendorUnit(input);
        }

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
            await   CurrentUnitOfWork.SaveChangesAsync();
            if (input.InputAddress != null)
            {
                foreach (var address in input.InputAddress)
                {
                    if (address.Line1 != null || address.Line2 != null || address.Line4 != null ||
                        address.Line4 != null || address.State != null ||
                        address.Country != null || address.Email != null)
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

        public async Task DeleteVendorUnit(IdInput input)
        {
            await _vendorUnitManager.DeleteAsync(input.Id);
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput();
            dto.TypeofObjectId = TypeofObject.Vendor;
            dto.ObjectId = input.Id;
            await _addressAppService.DeleteAddressUnit(dto);
        }

        public async Task<ListResultOutput<VendorUnitDto>> GetVendorUnitsById(IdInput input)
        {
            var vendorquery =
                from er in _vendorUnitRepository.GetAll()
                where er.Id == input.Id
                select new { er };
            var vendoritems = await vendorquery.ToListAsync();

            var addressquery =
               from er in _addresRepository.GetAll()
               where er.ObjectId == input.Id && er.TypeofObjectId == TypeofObject.Vendor
               select new { er };
            var addressitems = await addressquery.ToListAsync();
            return new ListResultOutput<VendorUnitDto>(
                vendoritems.Select(item =>
                {
                    var dto = item.er.MapTo<VendorUnitDto>();
                    dto.Address = new Collection<AddressUnitDto>();
                    foreach (var addr in addressitems)
                    {
                        dto.Address.Add(addr.er.MapTo<AddressUnitDto>());
                    }
                    return dto;
                }).ToList());
        }

        public async Task<ListResultOutput<VendorUnitDto>> GetVendorUnits(long? organizationUnitId=null)
        {
            var query =
                from vendor in _vendorUnitRepository.GetAll()
                join addr in _addresRepository.GetAll() on vendor.Id equals addr.ObjectId
                into temp
                from rt in temp.Where(p => p.IsPrimary == true).DefaultIfEmpty()
               
                where (organizationUnitId == null || vendor.OrganizationUnitId == organizationUnitId)
                select new { vendor, Address = rt };
            var items = await query.ToListAsync();

            return new ListResultOutput<VendorUnitDto>(
                items.Select(item =>
                {
                    var dto = item.vendor.MapTo<VendorUnitDto>();
                    dto.Address = new Collection<AddressUnitDto>();
                    if (item.Address != null)
                    {
                        dto.Address.Add(item.Address.MapTo<AddressUnitDto>());
                    }
                    return dto;
                }).ToList());
        }

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
                        address.Email != null)
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


                                
                                          