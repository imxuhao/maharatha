using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Organization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.Linq;
using AutoMapper;
using System.Data.Entity;
using Abp.Domain.Uow;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    public class TenantCloneAppService : CORPACCOUNTINGAppServiceBase
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<TypeOfCurrencyUnit, short> _typeOfCurrencyUnit;
        private readonly IRepository<TypeOfAccountUnit> _typeOfAccountUnit;
        private readonly IRepository<RegionUnit> _regionUnit;
        private readonly IRepository<CountryUnit> _countryUnit;
        private readonly IRepository<VendorUnit> _vendorUnit;
        private readonly IRepository<User, long> _userUnit;
        private readonly IRepository<Role> _roleUnit;
        private readonly IRepository<CoaUnit> _coaUnit;
        private readonly IRepository<EmployeeUnit> _employeeUnit;
        private readonly IRepository<CustomerUnit> _customerUnit;
        private readonly IRepository<ConnectionStringUnit> _connectionStringRepository;
        private readonly IRepository<OrganizationExtended, long> _organizationRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="typeOfCurrencyUnit"></param>
        /// <param name="typeOfAccountUnit"></param>
        /// <param name="regionUnit"></param>
        /// <param name="countryUnit"></param>
        /// <param name="vendorUnit"></param>
        /// <param name="userUnit"></param>
        /// <param name="roleUnit"></param>
        /// <param name="coaUnit"></param>
        /// <param name="employeeUnit"></param>
        /// <param name="customerUnit"></param>
        /// <param name="connectionStringRepository"></param>
        /// <param name="organizationRepository"></param>
        public TenantCloneAppService(
            IUnitOfWorkManager unitOfWorkManager,
       IRepository<TypeOfCurrencyUnit, short> typeOfCurrencyUnit,
       IRepository<TypeOfAccountUnit> typeOfAccountUnit,
       IRepository<RegionUnit> regionUnit,
       IRepository<CountryUnit> countryUnit, IRepository<VendorUnit> vendorUnit, IRepository<User, long> userUnit, IRepository<Role> roleUnit,
       IRepository<CoaUnit> coaUnit, IRepository<EmployeeUnit> employeeUnit, IRepository<CustomerUnit> customerUnit, IRepository<ConnectionStringUnit> connectionStringRepository, IRepository<OrganizationExtended, long> organizationRepository)

        {
            _unitOfWorkManager = unitOfWorkManager;
            _typeOfCurrencyUnit = typeOfCurrencyUnit;
            _typeOfAccountUnit = typeOfAccountUnit;
            _regionUnit = regionUnit;
            _countryUnit = countryUnit;
            _vendorUnit = vendorUnit;
            _userUnit = userUnit;
            _roleUnit = roleUnit;
            _coaUnit = coaUnit;
            _employeeUnit = employeeUnit;
            _customerUnit = customerUnit;
            _connectionStringRepository = connectionStringRepository;
            _organizationRepository = organizationRepository;
        }


        /// <summary>
        /// Sumit Method to add Users when map the user to Tenant
        /// Adding the newly created User in some other Tenanats of the same Organization
        /// </summary>
        /// <param name="newTenantId"></param>
        /// <param name="sourceTenantId"></param>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public async Task CloneTenantData(int newTenantId, int? sourceTenantId, List<string> entityList)
        {
            if (!ReferenceEquals(entityList, null))
            {
                foreach (string entityName in entityList)
                {
                    switch (entityName)
                    {
                        case "Vendors":
                            {
                                List<VendorUnit> vendorList = null;
                                //Get vendor data from Tenanat of Source
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _vendorUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        vendorList = await _vendorUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                //Inserting Vendor Data in DestinationTenant
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    Mapper.CreateMap<VendorUnit, VendorUnit>()
                                        .ForMember(u => u.Id, ap => ap.Ignore());
                                    var vendorUnit = new VendorUnit();
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        foreach (var vendor in vendorList)
                                        {
                                            vendor.MapTo(vendorUnit);
                                            vendorUnit.TenantId = newTenantId;
                                            vendorUnit.CreatorUserId = null;
                                            vendorUnit.CreatorUserId = null;
                                            await _vendorUnit.InsertAsync(vendorUnit);
                                        }
                                    }
                                }
                                break;
                            }
                        case "Users":
                            {
                                List<User> userList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _userUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        userList = await _userUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    Mapper.CreateMap<User, User>()
                                      .ForMember(u => u.Id, ap => ap.Ignore());
                                    var userUnit = new User();
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        foreach (var user in userList)
                                        {
                                            if (user.Name != "admin")
                                            {
                                                user.MapTo(userUnit);
                                                userUnit.TenantId = newTenantId;
                                                userUnit.CreatorUser = null;
                                                userUnit.CreatorUserId = null;
                                                userUnit.LastModifierUser = null;
                                                userUnit.LastModifierUserId = null;
                                                await _userUnit.InsertAsync(userUnit);
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case "Roles":
                            {
                                List<Role> rollList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _roleUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        rollList = await _roleUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<Role, Role>()
                                  .ForMember(u => u.Id, ap => ap.Ignore());
                                        var roleUnit = new Role();

                                        foreach (var role in rollList)
                                        {
                                            if (role.Name != "Admin" && role.Name != "User")
                                            {

                                                role.MapTo(roleUnit);
                                                roleUnit.TenantId = newTenantId;
                                                roleUnit.CreatorUser = null;
                                                roleUnit.CreatorUserId = null;
                                                roleUnit.LastModifierUserId = null;
                                                roleUnit.LastModifierUser = null;
                                                await _roleUnit.InsertAsync(roleUnit);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "ChartofAccounts":
                            {
                                List<CoaUnit> coaList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _coaUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        coaList = await _coaUnit.GetAll().Where(u => u.TenantId == sourceTenantId && u.IsCorporate).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<CoaUnit, CoaUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var coaUnit = new CoaUnit();
                                        foreach (var coa in coaList)
                                        {

                                            coa.MapTo(coaUnit);
                                            coa.TenantId = newTenantId;
                                            coa.CreatorUserId = null;
                                            await _coaUnit.InsertAsync(coaUnit);
                                        }
                                    }
                                }
                                break;
                            }

                        case "ProjectChartofAccounts":
                            {
                                List<CoaUnit> coaList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _coaUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        coaList = await _coaUnit.GetAll().Where(u => u.TenantId == sourceTenantId && !u.IsCorporate).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<CoaUnit, CoaUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var coaUnit = new CoaUnit();
                                        foreach (var coa in coaList)
                                        {
                                            coa.MapTo(coaUnit);
                                            coaUnit.TenantId = newTenantId;
                                            coaUnit.CreatorUserId = null;
                                            await _coaUnit.InsertAsync(coaUnit);
                                        }
                                    }
                                }
                                break;
                            }

                        case "Employees":
                            {
                                List<EmployeeUnit> empList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _employeeUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        empList = await _employeeUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                    {
                                        Mapper.CreateMap<EmployeeUnit, EmployeeUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var employeeUnit = new EmployeeUnit();
                                        if (!ReferenceEquals(entityList, null))
                                        {
                                            foreach (var emp in empList)
                                            {
                                                emp.MapTo(employeeUnit);
                                                employeeUnit.TenantId = newTenantId;
                                                employeeUnit.CreatorUserId = null;
                                                await _employeeUnit.InsertAsync(employeeUnit);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        case "Customers":
                            {
                                List<CustomerUnit> customerList = null;
                                using (_unitOfWorkManager.Current.SetTenantId(sourceTenantId))
                                {
                                    if (await _customerUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
                                    {
                                        customerList = await _customerUnit.GetAll().Where(u => u.TenantId == sourceTenantId).ToListAsync();
                                    }
                                }
                                using (_unitOfWorkManager.Current.SetTenantId(newTenantId))
                                {
                                    if (!ReferenceEquals(entityList, null))
                                    {
                                        Mapper.CreateMap<CustomerUnit, CustomerUnit>().ForMember(u => u.Id, ap => ap.Ignore());
                                        var customerUnit = new CustomerUnit();
                                        foreach (var customer in customerList)
                                        {
                                            customer.MapTo(customerUnit);
                                            customerUnit.TenantId = newTenantId;
                                            customerUnit.CreatorUserId = null;
                                            await _customerUnit.InsertAsync(customerUnit);
                                        }
                                    }
                                }
                                break;
                            }
                    }

                }
            }
        }


        /// <summary>
        /// Chaching Method
        /// Tenant based seeding
        /// </summary>
        /// <param name="newTenantId"></param>
        /// <returns></returns>
        public async Task CustomTenantSeeding(int newTenantId)
        {
            //Currency Seeding
            if (await _typeOfCurrencyUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var currencyList = await _typeOfCurrencyUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                currencyList.ForEach(u => u.TenantId = newTenantId);
                foreach (var currency in currencyList)
                {
                    await _typeOfCurrencyUnit.InsertAsync(currency);
                }
            }

            //Account Seeding
            if (await _typeOfAccountUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var typeOfAccountList = await _typeOfAccountUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                typeOfAccountList.ForEach(u => u.TenantId = newTenantId);
                foreach (var typeOfAccount in typeOfAccountList)
                {
                    await _typeOfAccountUnit.InsertAsync(typeOfAccount);
                }
            }

            //Region Seeding
            if (await _regionUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var regionList = await _regionUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                regionList.ForEach(u => u.TenantId = newTenantId);
                foreach (var region in regionList)
                {
                    await _regionUnit.InsertAsync(region);
                }
            }

            //Country Seeding
            if (await _countryUnit.CountAsync(u => u.TenantId == newTenantId) == 0)
            {
                var countryList = await _countryUnit.GetAll().Where(u => u.TenantId == 1).ToListAsync();

                countryList.ForEach(u => u.TenantId = newTenantId);
                foreach (var country in countryList)
                {
                    await _countryUnit.InsertAsync(country);
                }
            }
        }
    }
}
