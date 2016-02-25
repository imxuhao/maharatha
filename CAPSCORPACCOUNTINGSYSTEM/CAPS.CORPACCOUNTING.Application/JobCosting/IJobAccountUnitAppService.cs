using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobAccountUnitAppService : IApplicationService
    {
        Task<JobAccountUnitDto> CreateJobAccountUnit(CreateJobAccountUnitInput input);

        Task<JobAccountUnitDto> UpdateJobAccountUnit(UpdateJobAccountUnitInput input);
        Task DeleteJobAccountUnit(IdInput input);
        Task<ListResultOutput<JobAccountUnitDto>> GetJobAccountUnits(IdInput input);
    }
}
