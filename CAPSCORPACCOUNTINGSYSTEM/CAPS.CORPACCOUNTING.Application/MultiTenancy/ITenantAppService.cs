using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        Task<PagedResultOutput<TenantListDto>> GetTenants(SearchInputDto input);      

        Task CreateTenant(CreateTenantInput input);

        Task<TenantEditDto> GetTenantForEdit(EntityRequestInput input);

        Task UpdateTenant(TenantEditDto input);

        Task DeleteTenant(EntityRequestInput input);

        Task<GetTenantFeaturesForEditOutput> GetTenantFeaturesForEdit(EntityRequestInput input);

        Task UpdateTenantFeatures(UpdateTenantFeaturesInput input);

        Task ResetTenantSpecificFeatures(EntityRequestInput input);
    }
}
