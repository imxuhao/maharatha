using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on Job.
    /// </summary>
    public interface IJobUnitAppService :IApplicationService
    {
        /// <summary>
        ///  Create the Job.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobUnitDto> CreateJobUnit(CreateJobUnitInput input);

        /// <summary>
        /// Update the Job based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobUnitDto> UpdateJobUnit(UpdateJobUnitInput input);

        /// <summary>
        /// Delete the Job based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobUnit(IdInput input);

        /// <summary>
        /// Get the list of all Jobs and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JobUnitDto>> GetJobUnits(GetJobInput input);

        /// <summary>
        /// Get the Job based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobUnitDto> GetJobUnitById(IdInput input);

     
    }
}
