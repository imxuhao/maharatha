using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobCommercialAppService : IApplicationService
    {
        Task<JobCommercialUnitDto> CreateJobDetailUnit(CreateJobCommercialInput input);

        Task<JobCommercialUnitDto> UpdateJobDetailUnit(UpdateJobCommercialnput input);
        Task DeleteJobDetailUnit(IdInput input);
        Task<JobCommercialUnitDto> GetJobDetailsByJobId(IdInput input);
    }
}
