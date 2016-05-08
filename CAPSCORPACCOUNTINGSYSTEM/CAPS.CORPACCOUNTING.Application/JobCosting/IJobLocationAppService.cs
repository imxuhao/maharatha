using System.Collections.Generic;
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
        /// Create the JobLocation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobLocationUnitDto> CreateJobLocationUnit(CreateJobLocationInput input);

        /// <summary>
        /// Update the JobLocation based on JobLocationId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobLocationUnitDto> UpdateJobLocationUnit(UpdateJobLocationInput input);

        /// <summary>
        /// Get the list of all JobLocations based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<JobLocationUnitDto>> GetJobLocationUnitsByJobId(IdInput input);

        /// <summary>
        /// Delete the JobLocation based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobLocationUnit(IdInput input);

    }
}
