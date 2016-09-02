using System;
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
using Abp.Collections.Extensions;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Masters
{
    [AbpAuthorize] ///This is to ensure only logged in user has access to this module.
    public class EmployeeUnitAppService : CORPACCOUNTINGServiceBase, IEmployeeUnitAppService
    {
        private readonly EmployeeUnitManager _employeeUnitManager;
        private readonly IRepository<EmployeeUnit> _employeeUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IRepository<AddressUnit, long> _addressUnitRepository;
        private readonly ICacheManager _cacheManager;
        private readonly CustomAppSession _customAppSession;
        private readonly IEmployeeCache _empCache;

        public EmployeeUnitAppService(EmployeeUnitManager employeeUnitManager,
            IRepository<EmployeeUnit> employeeUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAddressUnitAppService addressAppService,
            IRepository<AddressUnit, long> addressRepository,
            CustomAppSession customAppSession, ICacheManager cacheManager, IEmployeeCache empCache)
        {
            _employeeUnitManager = employeeUnitManager;
            _employeeUnitRepository = employeeUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _addressAppService = addressAppService;
            _addressUnitRepository = addressRepository;
            _customAppSession = customAppSession;
            _cacheManager = cacheManager;
            _empCache = empCache;
        }
        /// <summary>
        /// Delete the Employee abd EmployeeAddresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
        /// Creates the Employee with Addresses
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<EmployeeUnitDto> CreateEmployeeUnit(CreateEmployeeUnitInput input)
        {
            var employeeUnit = input.MapTo<EmployeeUnit>();
            await _employeeUnitManager.CreateAsync(employeeUnit);
            await CurrentUnitOfWork.SaveChangesAsync();


            if (input.Addresses != null)
            {
                foreach (var address in input.Addresses)
                {
                    if (!string.IsNullOrEmpty(address.Line1) || !string.IsNullOrEmpty(address.Line2) ||
                        !string.IsNullOrEmpty(address.Line4) || !string.IsNullOrEmpty(address.Line4) ||
                        !string.IsNullOrEmpty(address.State) || !string.IsNullOrEmpty(address.Country) ||
                        !string.IsNullOrEmpty(address.Email) || !string.IsNullOrEmpty(address.Phone1) ||
                        !string.IsNullOrEmpty(address.Website))
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
        /// This method is for retrieve the records for showing in the grid with filters and SortOrder
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutput<EmployeeUnitDto>> GetEmployeeUnits(SearchInputDto input)
        {
            var query = CreateEmployeeQuery(input);
            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("Employee.LastName ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            var employeeListDtos = ConvertToEmployeeDtos(results);
            return new PagedResultOutput<EmployeeUnitDto>(resultCount, employeeListDtos);
        }

        /// <summary>
        /// Converting Employee to outputdto of a EmployeeList
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private List<EmployeeUnitDto> ConvertToEmployeeDtos(List<EmployeeAndAddressDto> results)
        {
            return results.Select(
                result =>
                {
                    var dto = result.Employee.MapTo<EmployeeUnitDto>();
                    dto.EmployeeId = result.Employee.Id;
                    dto.Addresses = new Collection<AddressUnitDto>();
                    if (result.Address != null)
                    {
                        dto.Addresses.Add(result.Address.MapTo<AddressUnitDto>());
                        dto.Addresses[0].AddressId = result.Address.Id;
                    }
                    return dto;
                }).ToList();
        }

        private IQueryable<EmployeeAndAddressDto> CreateEmployeeQuery(SearchInputDto input)
        {
            var query = from emp in _employeeUnitRepository.GetAll()
                        join addr in _addressUnitRepository.GetAll() on emp.Id equals addr.ObjectId
                                 into temp
                        from adrs in temp.Where(p => p.IsPrimary == true && p.TypeofObjectId == TypeofObject.Emp).DefaultIfEmpty()
                        select new EmployeeAndAddressDto { Employee = emp, Address = adrs };

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            return query;
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
            var empItem = await _employeeUnitRepository.GetAsync(input.Id);
            var addressitems = await _addressUnitRepository.GetAllListAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Emp);
            var result = empItem.MapTo<EmployeeUnitDto>();
            result.EmployeeId = empItem.Id;
            result.Addresses = new Collection<AddressUnitDto>();
            for (int i = 0; i < addressitems.Count; i++)
            {
                result.Addresses.Add(addressitems[i].MapTo<AddressUnitDto>());
                result.Addresses[i].AddressId = addressitems[i].Id;
            }
            return result;
        }

        public async Task<List<NameValueDto>> GetEmployeeList(SearchInputDto input)
        {
            var query = from emp in _employeeUnitRepository.GetAll()
                        select emp;
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            var employeeList = await query.Select(u => new NameValueDto { Name = u.LastName, Value = u.Id.ToString() }).ToListAsync();
            return employeeList;
        }

        public async Task<List<NameValueDto>> GetEmployeeList(AutoSearchInput input)
        {
            var cacheItem = await _empCache.GetEmployeeCacheItemAsync(
              CacheKeyStores.CalculateCacheKey(CacheKeyStores.EmployeeKey, Convert.ToInt32(_customAppSession.TenantId)));
            var res= cacheItem.ToList()
                    .WhereIf(input.Property == "isDirector", p => p.IsDirector)
                    .WhereIf(input.Property == "isProducer", p => p.IsProducer )
                    .WhereIf(input.Property == "isDirPhoto", p => p.IsArtDirector)
                    .WhereIf(input.Property == "isArtDirector", p => p.IsArtDirector)
                    .WhereIf(input.Property == "isSetDesigner", p => p.IsSetDesigner)
                    .WhereIf(!string.IsNullOrEmpty(input.Query), p => p.LastName.ToUpper().Contains(input.Query.ToUpper()) || p.FirstName.ToUpper().Contains(input.Query.ToUpper()))
                    .Select(u => new NameValueDto { Name = u.FirstName+" "+ u.LastName, Value = u.EmployeeId.ToString() }).ToList();
            return res;
        }
    }
}

