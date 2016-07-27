using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        Task<PagedResultOutput<TenantListDto>> GetTenants(GetTenantsInput input);

        Task CreateTenant(CreateTenantInput input);

        Task<TenantEditDto> GetTenantForEdit(EntityRequestInput input);

        Task UpdateTenant(TenantEditDto input);

        Task DeleteTenant(EntityRequestInput input);

        Task<GetTenantFeaturesForEditOutput> GetTenantFeaturesForEdit(EntityRequestInput input);

        Task UpdateTenantFeatures(UpdateTenantFeaturesInput input);

        Task ResetTenantSpecificFeatures(EntityRequestInput input);

        /// <summary>
        /// Get TenantList(CompanyList) By OrganizationId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<TenantListOutputDto>> GetTenantListByOrganizationId(IdInput<long> input);

        /// <summary>
        /// Get TenantList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<TenantListDto>> GetTenantUnits(SearchInputDto input);

        /// <summary>
        /// Create Tenant
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateTenantUnit(CreateTenantInputUnit input);


        Task<ComapnyPreferenceDto> GetCompanySettingsForEdit();
        Task<CompanyImageOutputDto> UpdateCompanyUnit(TenantExtendedUnitInput input);

        /// <summary>
        /// Get CompanyLogo
        /// </summary>
        /// <returns></returns>
        Task<CompanyImageOutputDto> GetCompanyLogo();


    }
}
