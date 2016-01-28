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

namespace CAPS.CORPACCOUNTING.Masters
{
    public class VendorUnitAppService : CORPACCOUNTINGServiceBase, IVendorUnitAppService
    {
        private readonly VendorUnitManager _vendorUnitManager;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAddressUnitAppService _addressAppService;

        public VendorUnitAppService(VendorUnitManager vendorUnitManager, IRepository<VendorUnit> vendorUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAddressUnitAppService addressAppService)
        {
            _vendorUnitManager = vendorUnitManager;
            _vendorUnitRepository = vendorUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
        }
        public IEventBus EventBus { get; set; }
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

             
            input.InputAddress.EmployeeId = vendorUnit.Id;
            await _addressAppService.CreateAddressUnit(input.InputAddress);
            await CurrentUnitOfWork.SaveChangesAsync();

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
            GetAddressUnitInput dto = new GetAddressUnitInput();
            dto.TypeofObjectId = TypeofObject.Vendor;
            dto.ObjectId = input.Id;
            await _addressAppService.DeleteAddressUnit(dto);
        }

        public async Task<ListResultOutput<VendorUnitDto>> GetVendorUnits()
        {
            var query =
                from vpt in _vendorUnitRepository.GetAll()
                select new {vpt};
            var items = await query.ToListAsync();

            return new ListResultOutput<VendorUnitDto>(
                items.Select(item =>
                {
                    var dto = item.vpt.MapTo<VendorUnitDto>();
                    return dto;
                }).ToList());
        }

        public async Task<VendorUnitDto> UpdateVendorUnit(UpdateVendorUnitInput input)
        {
            var vendorUnit = await _vendorUnitRepository.GetAsync(input.VendorId);
            await _addressAppService.UpdateAddressUnit(input.InputAddress);
            await CurrentUnitOfWork.SaveChangesAsync();

            #region Setting the values to be updated

            vendorUnit.LastName = input.LastName;
            vendorUnit.PayToName = input.PayToName;
            vendorUnit.VendorAccountInfo = input.VendorAccountInfo;
            vendorUnit.CreditLimit = input.CreditLimit;
            vendorUnit.PaymentTermsId = input.PaymentTermsId;
            vendorUnit.IsCorporation = input.IsCorporation;
            vendorUnit.IsIndependentContractor = input.IsIndependentContractor;
            vendorUnit.Isw9OnFile = input.Isw9OnFile;
            vendorUnit.TypeOFvendorId = input.TypeOFvendorId;
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
            vendorUnit.TypeOF1099Box = input.TypeOF1099Box;
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


                                
                                          