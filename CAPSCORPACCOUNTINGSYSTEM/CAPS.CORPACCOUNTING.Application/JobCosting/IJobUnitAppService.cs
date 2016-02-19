using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobUnitAppService :IApplicationService
    {
        Task<JobUnitDto> CreateJobUnit(CreateJobUnitInput input);

        Task<JobUnitDto> UpdateJobUnit(UpdateJobUnitInput input);
        Task DeleteJobUnit(IdInput input);
        Task<PagedResultOutput<JobUnitDto>> GetJobUnits(GetJobInput input);
    }
}
