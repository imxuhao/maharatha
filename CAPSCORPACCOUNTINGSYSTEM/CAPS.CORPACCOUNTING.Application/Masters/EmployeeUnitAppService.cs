using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Linq;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class EmployeeUnitAppService : CORPACCOUNTINGServiceBase, IEmployeeUnitAppService
    {
        private readonly EmployeeUnitManager _employeeUnitManager;
        private readonly IRepository<EmployeeUnit> _employeeUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit,long> _addressUnitRepository;

        public EmployeeUnitAppService(EmployeeUnitManager employeeUnitManager,
            IRepository<EmployeeUnit> employeeUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAddressUnitAppService addressAppService, IRepository<AddressUnit,long> addressRepository )
        {
            _employeeUnitManager = employeeUnitManager;
            _employeeUnitRepository = employeeUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
            _addressUnitRepository = addressRepository;
        }
        [UnitOfWork]
        public async Task DeleteEmployeeUnit(IdInput input)
        {
            await _employeeUnitManager.DeleteAsync(input.Id);
            GetAddressUnitInput dto=new GetAddressUnitInput();
            dto.TypeofObjectId=TypeofObject.Emp;
            dto.ObjectId = input.Id;
            await _addressAppService.DeleteAddressUnit(dto);
        }

        [UnitOfWork]
        public async Task<EmployeeUnitDto> CreateEmployeeUnit(CreateEmployeeUnitInput input)
        {
            var employeeUnit = new EmployeeUnit(lastname: input.LastName, firstname: input.FirstName,
                ssntaxid: input.SSNTaxId, employeeregion: input.EmployeeRegion, federaltaxid: input.FederalTaxId,
                is1099: input.Is1099, isw9Onfile: input.IsW9OnFile,
                isindependantcontractor: input.IsIndependantContractor, iscorporation: input.IsCorporation,
                isproducer: input.IsProducer, isdirector: input.IsDirector, isdirphoto: input.IsDirPhoto,
                issetdesigner: input.IsSetDesigner,
                iseditor: input.IsEditor, isartdirector: input.IsArtDirector, isactive: input.IsActive,
                isapproved: input.IsApproved, organizationunitid: input.OrganizationUnitId);
            await _employeeUnitManager.CreateAsync(employeeUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

           
            input.InputAddress.ObjectId = employeeUnit.Id;
            await _addressAppService.CreateAddressUnit(input.InputAddress);
            await CurrentUnitOfWork.SaveChangesAsync();
            return employeeUnit.MapTo<EmployeeUnitDto>();
        }

       

        public async Task<ListResultOutput<EmployeeUnitDto>> GetEmployeeUnits()
        {
            var query =
                from er in _employeeUnitRepository.GetAll()
                join ar in _addressUnitRepository.GetAll() on er.Id equals  ar.ObjectId
                select new {er, Address=ar };
            var items = await query.ToListAsync();

            return new ListResultOutput<EmployeeUnitDto>(
                items.Select(item =>
                {  
                    var dto = item.er.MapTo<EmployeeUnitDto>();
                    dto.Address=new Collection<AddressUnitDto>();
                    dto.Address.Add(item.Address.MapTo<AddressUnitDto>());
                    return dto;
                }).ToList());
        }

        public async Task<EmployeeUnitDto> UpdateEmployeeUnit(UpdateEmployeeUnitInput input)
        {
            var employeeUnit = await _employeeUnitRepository.GetAsync(input.EmployeeId);

            await _addressAppService.UpdateAddressUnit(input.InputAddress);
            await CurrentUnitOfWork.SaveChangesAsync();

            #region Setting the values to be updated

            employeeUnit.LastName = input.LastName;
            employeeUnit.FirstName = input.FirstName;
            employeeUnit.EmployeeRegion = input.EmployeeRegion;
            employeeUnit.FederalTaxId = input.FederalTaxId;
            employeeUnit.Is1099 = input.Is1099;
            employeeUnit.IsActive = input.IsActive;
            employeeUnit.IsApproved = input.IsApproved;
            employeeUnit.IsArtDirector = input.IsArtDirector;
            employeeUnit.IsCorporation = input.IsCorporation;
            employeeUnit.IsDirPhoto = input.IsDirPhoto;
            employeeUnit.IsDirector = input.IsDirector;
            employeeUnit.IsEditor = input.IsEditor;
            employeeUnit.IsIndependantContractor = input.IsIndependantContractor;
            employeeUnit.IsProducer = input.IsProducer;
            employeeUnit.SSNTaxId = input.SSNTaxId;
            employeeUnit.OrganizationUnitId = input.OrganizationUnitId;
            employeeUnit.SSNTaxId = input.SSNTaxId;
            employeeUnit.IsSetDesigner = input.IsSetDesigner;

            #endregion

            await _employeeUnitManager.UpdateAsync(employeeUnit);

            await CurrentUnitOfWork.SaveChangesAsync();
           


            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of employee is Added*/
            };

            return employeeUnit.MapTo<EmployeeUnitDto>();
        }
    }
}
