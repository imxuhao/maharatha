using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobCommercialAppService : IApplicationService
    {
        Task<JobJobCommercialUnitDto> CreateJobDetailUnit(CreateJobCommercialInput input);

        Task<JobJobCommercialUnitDto> UpdateJobDetailUnit(UpdateJobCommercialnput input);
        Task DeleteJobDetailUnit(IdInput input);
    }
}
