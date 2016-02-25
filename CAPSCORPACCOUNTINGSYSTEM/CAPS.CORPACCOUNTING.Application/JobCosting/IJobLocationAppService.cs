using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobLocationAppService : IApplicationService
    {
        Task<JobLocationUnitDto> CreateJobLocationUnit(CreateJobLocationInput input);

        Task<JobLocationUnitDto> UpdateJobLocationUnit(UpdateJobLocationInput input);
        Task<ListResultOutput<JobLocationUnitDto>> GetJobLocationUnitsByJobId(IdInput input);
        Task DeleteJobLocationUnit(IdInput input);

    }
}
