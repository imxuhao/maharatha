using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on JobLocation.
    /// </summary>
    public interface IJobLocationAppService : IApplicationService
    {
        /// <summary>
        /// This is used to create the JobLocation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobLocationUnitDto> CreateJobLocationUnit(CreateJobLocationInput input);

        /// <summary>
        /// This is used to update the JobLocation based on JobLocationId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobLocationUnitDto> UpdateJobLocationUnit(UpdateJobLocationInput input);

        /// <summary>
        /// This is used to get the list of all JobLocations based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<JobLocationUnitDto>> GetJobLocationUnitsByJobId(IdInput input);

        /// <summary>
        /// This is used to delete the JobLocation based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobLocationUnit(IdInput input);

    }
}
