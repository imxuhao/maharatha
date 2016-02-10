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
            DeleteAddressUnitInput dto = new DeleteAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Emp,
                ObjectId = input.Id
            };
            await _addressAppService.DeleteAddressUnit(dto);
        }

        /// <summary>
        /// This method is for testing to Insert data in Employee  with 2 addresses.After UI development we need to remove this method.
        /// using this method we are calling CreateEmployeeUnit to insert Employee and Addresss Data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task InsertEmployeeData(CreateEmployeeUnitInput input)
        {
            CreateAddressUnitInput empAddr1 = new CreateAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Emp,
                AddressTypeId = TypeofAddress.PrimaryContact,
                IsPrimary = true,
                Line1 = "Address1Employee"
            };
            CreateAddressUnitInput empAddr2 = new CreateAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Emp,
                AddressTypeId = TypeofAddress.Home,
                Line1 = "Address2Employee"
            };

            input.InputAddresses = new List<CreateAddressUnitInput> { empAddr1, empAddr2};
            await CreateEmployeeUnit(input);
        }
        /// <summary>
        /// This method is  for testing to update Employee data with addresses.After UI development we need to remove this method.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [UnitOfWork]
        public async Task UpdatedEmployeeData(UpdateEmployeeUnitInput input)
        {

            UpdateAddressUnitInput employeeAddr1 = new UpdateAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Emp,
                AddressTypeId = TypeofAddress.PrimaryContact,
                Email = "test@gmail.com",
                AddressId = 1,
                ObjectId = input.EmployeeId,
                IsPrimary = true
            };

            UpdateAddressUnitInput employeeAddr2 = new UpdateAddressUnitInput
            {
                TypeofObjectId = TypeofObject.Emp,
                AddressTypeId = TypeofAddress.Business,
                Email = "test1@gmail.com",
                AddressId = 2,
                ObjectId = input.EmployeeId
            };

            input.InputAddresses = new List<UpdateAddressUnitInput> { employeeAddr1, employeeAddr2};
            await UpdateEmployeeUnit(input);
        }

        /// <summary>
        /// Creates Employee with Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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


            if (input.InputAddresses != null)
            {
                foreach (var address in input.InputAddresses)
                {
                    if (address.Line1 != null || address.Line2 != null || address.Line4 != null ||
                        address.Line4 != null || address.State != null ||
                        address.Country != null || address.Email != null || address.Phone1 != null || address.Website != null)
                    {
                        address.ObjectId = employeeUnit.Id;
                        await _addressAppService.CreateAddressUnit(address);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
            }
            return employeeUnit.MapTo<EmployeeUnitDto>();
        }



        /// <summary>
        /// To get the records for showing in the grid with filters and SortOrder
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultOutput<EmployeeUnitDto>> GetEmployeeUnits(GetEmployeeInput input)
        {
            input.SortOrder = input.SortOrder == "ASC" ? " ascending" : " descending";
            var query =
                from emp in _employeeUnitRepository.GetAll().OrderBy(input.SortColumn + input.SortOrder)
                        .Skip((input.PageNumber - 1) * input.NumberofColumnsperPage)
                        .Take(input.NumberofColumnsperPage)
                join addr in _addressUnitRepository.GetAll() on emp.Id equals addr.ObjectId
                         into temp
                from adrs in temp.Where(p => p.IsPrimary == true).DefaultIfEmpty()
                where (input.OrganizationUnitId == null || emp.OrganizationUnitId == input.OrganizationUnitId) &&
                        (input.LastName == null || emp.LastName.Contains(input.LastName)) &&
                        (input.FirstName == null || emp.FirstName.Contains(input.FirstName)) &&
                        (input.SSNTaxId == null || emp.SSNTaxId.Contains(input.SSNTaxId)) &&
                        (input.FedralTaxId == null || emp.FederalTaxId.Contains(input.FedralTaxId))
                select new {emp, Address= adrs };
            var items = await query.ToListAsync();

            return new ListResultOutput<EmployeeUnitDto>(
                items.Select(item =>
                {  
                    var dto = item.emp.MapTo<EmployeeUnitDto>();
                    dto.EmployeeId = item.emp.Id;
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
        /// Updates the employee with Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<EmployeeUnitDto> UpdateEmployeeUnit(UpdateEmployeeUnitInput input)
        {
            var employeeUnit = await _employeeUnitRepository.GetAsync(input.EmployeeId);
            
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
                        address.TypeofObjectId = TypeofObject.Emp;
                        address.ObjectId = input.EmployeeId;
                        await
                            _addressAppService.CreateAddressUnit(
                                AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(address));
                    }
                }
                await CurrentUnitOfWork.SaveChangesAsync();

            }


            _unitOfWorkManager.Current.Completed += (sender, args) =>
            {
                /*Do Something when the Chart of employee is Added*/
            };

            return employeeUnit.MapTo<EmployeeUnitDto>();
        }

        /// <summary>
        /// Get the EmployeeDeatails by EmployeeId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<EmployeeUnitDto> GetEmployeeUnitsById(IdInput input)
        {
            var empquery =
               from emp in _employeeUnitRepository.GetAll()
               where emp.Id == input.Id
               select new { emp };
            var empitems = await empquery.ToListAsync();

            var addressquery =
               from addr in _addressUnitRepository.GetAll()
               where addr.ObjectId == input.Id && addr.TypeofObjectId == TypeofObject.Emp
               select new { addr };

            var addressitems = await addressquery.ToListAsync();

            var result = empitems[0].emp.MapTo<EmployeeUnitDto>();
            result.EmployeeId = empitems[0].emp.Id;
            result.Addresses = new Collection<AddressUnitDto>();
            for (int i = 0; i < addressitems.Count; i++)
            {
                result.Addresses.Add(addressitems[i].addr.MapTo<AddressUnitDto>());
                result.Addresses[i].AddressId = addressitems[i].addr.Id;
            }

            return result;
        }
    }
}
