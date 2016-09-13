using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Security;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Editions.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Organization;
using System.IO;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Storage;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    public class TenantAppService : CORPACCOUNTINGAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly IRepository<OrganizationExtended, long> _organizationRepository;
        private readonly IRepository<TenantExtendedUnit> _tenantExtendedUnitRepository;
        private readonly IRepository<AddressUnit, long> _addressRepository;
        private readonly IAddressUnitAppService _addressAppService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAppFolders _appFolders;
        private readonly TenantExtendedUnitManager _tenantExtendedManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly IRepository<BinaryObject,Guid> _binaryObjectRepository;
        private readonly IRepository<ConnectionStringUnit> _connectionStringRepository;
        private readonly IRepository<VendorUnit> _vendorUnitRepository;
        private readonly IRepository<User,long> _userUnitRepository;
        private readonly IRepository<Role> _roleUnitRepository;
        private readonly IRepository<CustomerUnit> _customerUnitRepository;
        private readonly IRepository<EmployeeUnit> _employeeUnitRepository;
        private readonly IRepository<CoaUnit> _coaUnitRepository;


        public TenantAppService(
            TenantManager tenantManager, 
            IRepository<OrganizationExtended, long> organizationRepository, IRepository<TenantExtendedUnit> tenantExtendedUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, IAppFolders appFolders, IRepository<AddressUnit, long> addressRepository, TenantExtendedUnitManager tenantExtendedManager,
            IAddressUnitAppService addressAppService, IBinaryObjectManager binaryObjectManager, IRepository<BinaryObject, Guid> binaryObjectRepository, IRepository<ConnectionStringUnit> connectionStringRepository,
            IRepository<VendorUnit> vendorUnitRepository, IRepository<User, long> userUnitRepository, IRepository<Role> roleUnitRepository, IRepository<CustomerUnit> customerUnitRepository, 
            IRepository<EmployeeUnit> employeeUnitRepository, IRepository<CoaUnit> coaUnitRepository)
        {
            _tenantManager = tenantManager;
            _organizationRepository = organizationRepository;
            _tenantExtendedUnitRepository = tenantExtendedUnitRepository; ;
            _unitOfWorkManager = unitOfWorkManager;
            _appFolders = appFolders;
            _addressRepository = addressRepository;
            _tenantExtendedManager = tenantExtendedManager;
            _addressAppService = addressAppService;
            _binaryObjectManager = binaryObjectManager;
            _binaryObjectRepository = binaryObjectRepository;
            _connectionStringRepository = connectionStringRepository;
            _vendorUnitRepository = vendorUnitRepository;
            _userUnitRepository = userUnitRepository;
            _roleUnitRepository = roleUnitRepository;
            _customerUnitRepository = customerUnitRepository;
            _employeeUnitRepository = employeeUnitRepository;
            _coaUnitRepository = coaUnitRepository;
        }


        public async Task<PagedResultOutput<TenantListDto>> GetTenants(GetTenantsInput input)
        {
            var query = TenantManager.Tenants
                .Include(t => t.Edition)
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    t =>
                        t.Name.Contains(input.Filter) ||
                        t.TenancyName.Contains(input.Filter)
                );

            var tenantCount = await query.CountAsync();
            var tenants = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultOutput<TenantListDto>(
                tenantCount,
                tenants.MapTo<List<TenantListDto>>()
                );
        }

        /// <summary>
        /// SumitMethod to get TenantList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Tenants)]
        public async Task<PagedResultOutput<TenantListDto>> GetTenantUnits(SearchInputDto input)
        {
            var query = from tenant in TenantManager.Tenants
                        join org in _organizationRepository.GetAll() on tenant.OrganizationUnitId equals org.Id
                        join connection in _connectionStringRepository.GetAll() on org.ConnectionStringId equals connection.Id
                        select new { tenant, OrganizationName = org.DisplayName ,ConnectionName=connection.ConnectionString};

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }

            var tenantCount = await query.CountAsync();
            var tenants = await query.OrderBy(Helper.GetSort("tenant.Name ASC", input.Sorting)).PageBy(input).ToListAsync();

            return new PagedResultOutput<TenantListDto>(tenantCount, tenants.Select(item =>
            {
                var dto = item.tenant.MapTo<TenantListDto>();
                dto.OrganizationName = item.OrganizationName;
                var connectionString = new SqlConnectionStringBuilder(SimpleStringCipher.Instance.Decrypt(item.ConnectionName));
                dto.ServerName = connectionString.DataSource;
                dto.DatabaseName= connectionString.InitialCatalog;
                return dto;
            }).ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Create)]
        [UnitOfWork(IsDisabled = true)]
        public async Task CreateTenant(CreateTenantInput input)
        {
            await _tenantManager.CreateWithAdminUserAsync(input.TenancyName,
                input.Name,
                input.AdminPassword,
                input.AdminEmailAddress,
                input.ConnectionString,
                input.IsActive,
                input.EditionId,
                input.ShouldChangePasswordOnNextLogin,
                input.SendActivationEmail
                );
        }

        /// <summary>
        /// This is Sumit Method To create the Tenants
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Tenants_Create)]
        [UnitOfWork(IsDisabled = true)]
        public async Task CreateTenantUnit(CreateTenantInputUnit input)
        {
            await _tenantManager.CreateWithAdminUserAsync(input.TenancyName,
                input.Name,
                input.AdminPassword,
                input.AdminEmailAddress,
                input.IsActive,
                input.EditionId,
                input.ShouldChangePasswordOnNextLogin,
                input.SendActivationEmail,
                input.OrganizationUnitId, input.SourceTenantId, input.ModuleList);

        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<TenantEditDto> GetTenantForEdit(EntityRequestInput input)
        {
            var tenantEditDto = (await TenantManager.GetByIdAsync(input.Id)).MapTo<TenantEditDto>();
            tenantEditDto.ConnectionString = SimpleStringCipher.Instance.Decrypt(tenantEditDto.ConnectionString);
            return tenantEditDto;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task UpdateTenant(TenantEditDto input)
        {
            input.ConnectionString = SimpleStringCipher.Instance.Encrypt(input.ConnectionString);
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            input.MapTo(tenant);
            CheckErrors(await TenantManager.UpdateAsync(tenant));
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Delete)]
        public async Task DeleteTenant(EntityRequestInput input)
        {
            if (await ValidateTenant(input.Id))
            {
                var tenant = await TenantManager.GetByIdAsync(input.Id);
                CheckErrors(await TenantManager.DeleteAsync(tenant));
            }
            else
            {
                throw new UserFriendlyException(L("DeleteTenantValidationMessage"));

            }
            
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task<GetTenantFeaturesForEditOutput> GetTenantFeaturesForEdit(EntityRequestInput input)
        {
            var features = FeatureManager.GetAll();
            var featureValues = await TenantManager.GetFeatureValuesAsync(input.Id);

            return new GetTenantFeaturesForEditOutput
            {
                Features = features.MapTo<List<FlatFeatureDto>>().OrderBy(f => f.DisplayName).ToList(),
                FeatureValues = featureValues.Select(fv => new NameValueDto(fv)).ToList()
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task UpdateTenantFeatures(UpdateTenantFeaturesInput input)
        {
            await TenantManager.SetFeatureValuesAsync(input.Id, input.FeatureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_ChangeFeatures)]
        public async Task ResetTenantSpecificFeatures(EntityRequestInput input)
        {
            await TenantManager.ResetAllFeaturesAsync(input.Id);
        }
        public async Task<List<TenantListOutputDto>> GetTenantListByOrganizationId(IdInput<long> input)
        {
            var tenantList = await (from tenant in TenantManager.Tenants
                                    where tenant.OrganizationUnitId == input.Id
                                    select new TenantListOutputDto { TenantName = tenant.TenancyName, TenantId = tenant.Id }).ToListAsync();

            return tenantList;
        }


        /// <summary>
        /// Updating Sumit settings with comapnyAddress,Logo,Transmitter Fields
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [AbpAuthorize(AppPermissions.Pages_Administration_Tenant_Settings)]
        public async Task<CompanyImageOutputDto> UpdateCompanyUnit(TenantExtendedUnitInput input)
        {
           
            int tenantid = AbpSession.GetTenantId();
            byte[] byteArray = null;
            //using (_unitOfWorkManager.Current.SetTenantId(null))
            //{
            //    var tenant = await TenantManager.GetByIdAsync(tenantid);
            //    tenant.Name = input.TenantName;
            //    CheckErrors(await TenantManager.UpdateAsync(tenant));
            //}


            CompanyImageOutputDto companyLogo = new CompanyImageOutputDto();
            using (_unitOfWorkManager.Current.SetTenantId(tenantid))
            {
                if (input.TenantExtendedId > 0)
                {

                    if (!ReferenceEquals(input.ComapanyLogo, null))
                        byteArray = await UpdateCompanyLogo(input);
                    var tenant = await _tenantExtendedUnitRepository.GetAsync(input.TenantExtendedId);
                    Mapper.Map(input, tenant);

                    await _tenantExtendedManager.UpdateAsync(tenant);
                    companyLogo.TenantExtendedId = tenant.Id;
                    // update address Information
                    if (!ReferenceEquals(input.Address, null))
                    {
                        if (input.Address.AddressId != 0)
                        {
                            input.Address.TypeofObjectId = TypeofObject.OrganizationUnit;
                            await _addressAppService.UpdateAddressUnit(input.Address);
                            companyLogo.AddressId = input.Address.AddressId;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(input.Address.Line1) || !string.IsNullOrEmpty(input.Address.Line2 )||
                                !string.IsNullOrEmpty(input.Address.Line3) || !string.IsNullOrEmpty(input.Address.Line4 )||
                                !string.IsNullOrEmpty(input.Address.State) || !string.IsNullOrEmpty(input.Address.Country) ||
                                !string.IsNullOrEmpty(input.Address.Email) || !string.IsNullOrEmpty(input.Address.Phone1 )||
                                !string.IsNullOrEmpty(input.Address.Website))
                            {
                                input.Address.TypeofObjectId = TypeofObject.OrganizationUnit;
                                input.Address.ObjectId = input.TenantExtendedId;
                               // AutoMapper.Mapper.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                                //var config = new MapperConfiguration(cfg => {
                                //    cfg.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                                //});

                                var addr = await _addressAppService.CreateAddressUnit(
                                         AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(input.Address));
                                companyLogo.AddressId = addr.AddressId;
                            }
                        }
                    }

                }
                else
                {
                    if (!ReferenceEquals(input.ComapanyLogo, null))
                        byteArray = await UpdateCompanyLogo(input);
                    var tenantExtended = input.MapTo<TenantExtendedUnit>();
                    int id = await _tenantExtendedManager.CreateAsync(tenantExtended);
                    companyLogo.TenantExtendedId = id;
                    //address Information
                    if (!ReferenceEquals(input.Address, null))
                    {

                        if (!string.IsNullOrEmpty(input.Address.Line1) || !string.IsNullOrEmpty(input.Address.Line2) ||
                         !string.IsNullOrEmpty(input.Address.Line4) || !string.IsNullOrEmpty(input.Address.Line4) ||
                         !string.IsNullOrEmpty(input.Address.State) || !string.IsNullOrEmpty(input.Address.Country) ||
                         !string.IsNullOrEmpty(input.Address.Email) || !string.IsNullOrEmpty(input.Address.Phone1) ||
                         !string.IsNullOrEmpty(input.Address.ContactNumber))
                        {
                            input.Address.TypeofObjectId = TypeofObject.OrganizationUnit;
                            input.Address.ObjectId = id;
                            // AutoMapper.Mapper.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                            var config = new MapperConfiguration(cfg => {
                                cfg.CreateMap<UpdateAddressUnitInput, CreateAddressUnitInput>();
                            });
                            var addr = await _addressAppService.CreateAddressUnit(
                                    AutoMapper.Mapper.Map<UpdateAddressUnitInput, CreateAddressUnitInput>(input.Address));
                            companyLogo.AddressId = addr.AddressId;
                        }
                    }

                }
                await CurrentUnitOfWork.SaveChangesAsync();
                if (byteArray != null)
                {
                    companyLogo.CompanyLogo = Convert.ToBase64String(byteArray);
                    companyLogo.CompanyLogoId = input.CompanyLogoId;
                }
                return companyLogo;
            }
        }


        /// <summary>
        /// Get  comapnyAddress,Logo,Transmitter Fields for Editing in TenantLevel
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_Tenant_Settings)]
        public async Task<ComapnyPreferenceDto> GetCompanySettingsForEdit()
        {
            string tenancyName;
            int tenantid = AbpSession.GetTenantId();
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                var currentTenant = await _tenantManager.Tenants.FirstOrDefaultAsync(p => p.Id == tenantid);
                tenancyName = currentTenant.Name;
            }
          
            using (_unitOfWorkManager.Current.SetTenantId(tenantid))
            {
                var extendedcompany = await (from company in _tenantExtendedUnitRepository.GetAll()
                                             join address in _addressRepository.GetAll().Where(u => u.TypeofObjectId == TypeofObject.OrganizationUnit) on
                                                 company.Id equals address.ObjectId into addresss
                                             from address in addresss.DefaultIfEmpty()
                                             select new { company, address }).FirstOrDefaultAsync();

                if (!ReferenceEquals(extendedcompany, null))
                {
                    var dto = extendedcompany.company.MapTo<ComapnyPreferenceDto>();
                    dto.TenantExtendedId = extendedcompany.company.Id;
                    dto.CompanyName = tenancyName;
                    if (!ReferenceEquals(extendedcompany.address, null))
                    {
                        dto.Address = extendedcompany.address.MapTo<AddressUnitDto>();
                        dto.Address.AddressId = extendedcompany.address.Id;
                    }
                    if (extendedcompany.company.CompanyLogoId != null)
                    {
                        var file = await _binaryObjectManager.GetOrNullAsync(extendedcompany.company.CompanyLogoId.Value);
                        dto.CompanyLogo = Convert.ToBase64String(file.Bytes);
                    }
                    return dto;
                }
                ComapnyPreferenceDto comapnyPreferenceDto = new ComapnyPreferenceDto {CompanyName = tenancyName};
                return comapnyPreferenceDto;
            }
        }

        private async Task<byte[]> UpdateCompanyLogo(TenantExtendedUnitInput input)
        {
            var tempProfilePicturePath = Path.Combine(_appFolders.TempFileDownloadFolder, input.ComapanyLogo.FileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tempProfilePicturePath, FileMode.Open))
            {
                using (var bmpImage = new Bitmap(fsTempProfilePicture))
                {
                    var width = input.ComapanyLogo.Width == 0 ? bmpImage.Width : input.ComapanyLogo.Width;
                    var height = input.ComapanyLogo.Height == 0 ? bmpImage.Height : input.ComapanyLogo.Height;
                    var bmCrop = bmpImage.Clone(new Rectangle(input.ComapanyLogo.X, input.ComapanyLogo.Y, width, height), bmpImage.PixelFormat);

                    using (var stream = new MemoryStream())
                    {
                        bmCrop.Save(stream, bmpImage.RawFormat);
                        stream.Close();
                        byteArray = stream.ToArray();
                    }
                }
            }

            if (byteArray.LongLength > 1024000) //1000 KB
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit"));
            }


            if (input.CompanyLogoId.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(input.CompanyLogoId.Value);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, byteArray);
            await _binaryObjectManager.SaveAsync(storedFile);
            input.CompanyLogoId = storedFile.Id;
            return byteArray;
        }
        /// <summary>
        /// Get CompanyLogo
        /// </summary>
        /// <returns></returns>
        public async Task<CompanyImageOutputDto> GetCompanyLogo()
        {
            var tenant =await (from tenantextended in _tenantExtendedUnitRepository.GetAll()
                join binaryobj in _binaryObjectRepository.GetAll() on tenantextended.CompanyLogoId equals binaryobj.Id
                select binaryobj).FirstOrDefaultAsync();
            CompanyImageOutputDto companylogo = new CompanyImageOutputDto
            {
                CompanyLogo = !ReferenceEquals(tenant,null)? Convert.ToBase64String(tenant.Bytes):null
            };
            return companylogo;
            
        }

        /// <summary>
        /// Sumit Method to Update TenantUnit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task UpdateTenantUnit(TenantEditDto input)
        {
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            input.ConnectionString = tenant.ConnectionString;
            input.MapTo(tenant);
            CheckErrors(await TenantManager.UpdateAsync(tenant));
        }

        /// <summary>
        /// validating the tenant for Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> ValidateTenant(int id)
        {
            using (_unitOfWorkManager.Current.SetTenantId(id))
            {
                if (await _vendorUnitRepository.GetAll().AnyAsync() || await _userUnitRepository.GetAll().AnyAsync(p => p.UserName != User.AdminUserName)
               || await _roleUnitRepository.GetAll().AnyAsync(p => p.Name != StaticRoleNames.Tenants.Admin || p.Name != StaticRoleNames.Tenants.User)
               || await _coaUnitRepository.GetAll().AnyAsync() || await _customerUnitRepository.GetAll().AnyAsync() 
               || await _employeeUnitRepository.GetAll().AnyAsync())
                {
                    return false;
                }
            }
            return true;
        }

    }
}