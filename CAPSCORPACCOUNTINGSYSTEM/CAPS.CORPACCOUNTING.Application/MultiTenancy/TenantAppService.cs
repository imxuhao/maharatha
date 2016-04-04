using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Editions.Dto;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Helpers;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    [AbpAuthorize(AppPermissions.Pages_Tenants)]
    public class TenantAppService : CORPACCOUNTINGAppServiceBase, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly IRepository<User,long> _userRepository;
        private readonly IRepository<Tenant> _tenantRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public TenantAppService(
            TenantManager tenantManager, IRepository<User, long> userRepository, IUnitOfWorkManager unitOfWorkManager, IRepository<Tenant> tenantRepository)
        {
            _tenantManager = tenantManager;
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task<PagedResultOutput<TenantListDto>> GetTenants(SearchInputDto input)
        {
            string userName = "admin";
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var query = from tenant in TenantManager.Tenants.Include(t => t.Edition)
                            join user in _userRepository.GetAll() on tenant.Id equals user.TenantId
                            into tentnuser
                            from users in tentnuser.DefaultIfEmpty()
                            select new { Tenant=tenant,User= users };

                if (!ReferenceEquals(input.Filters, null))
                {
                    SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                    if (!ReferenceEquals(mapSearchFilters, null))
                        query = Helper.CreateFilters(query, mapSearchFilters);
                }

                query = query.Where(item => item.User.Name== userName);

                var tenantCount = await query.CountAsync();                
                var tenants = await query.OrderBy(Helper.GetSort("Tenant.TenancyName ASC", input.Sorting)).PageBy(input).ToListAsync();
               

                return new PagedResultOutput<TenantListDto>(tenantCount, tenants.Select(item =>
                {
                    var dto = item.Tenant.MapTo<TenantListDto>();
                    dto.AdminEmailAddress = item.User.EmailAddress;
                    return dto;
                }).ToList());

               
            }
        }       

        [AbpAuthorize(AppPermissions.Pages_Tenants_Create)]
        [UnitOfWork(IsDisabled = true)]
        public async Task CreateTenant(CreateTenantInput input)
        {
            await _tenantManager.CreateWithAdminUserAsync(input.TenancyName,
                input.Name,
                input.AdminPassword,
                input.AdminEmailAddress,
                input.IsActive,
                input.EditionId,
                input.ShouldChangePasswordOnNextLogin,
                input.SendActivationEmail);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task<TenantEditDto> GetTenantForEdit(EntityRequestInput input)
        {
            return (await TenantManager.GetByIdAsync(input.Id)).MapTo<TenantEditDto>();
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Edit)]
        public async Task UpdateTenant(TenantEditDto input)
        {
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            input.MapTo(tenant);
            CheckErrors(await TenantManager.UpdateAsync(tenant));
        }

        [AbpAuthorize(AppPermissions.Pages_Tenants_Delete)]
        public async Task DeleteTenant(EntityRequestInput input)
        {
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            CheckErrors(await TenantManager.DeleteAsync(tenant));
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
    }
}