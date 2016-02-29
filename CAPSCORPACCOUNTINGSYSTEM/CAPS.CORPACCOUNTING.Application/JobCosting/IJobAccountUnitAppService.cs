using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on JobAccount.
    /// </summary>
    public interface IJobAccountUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the JobAccount.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobAccountUnitDto> CreateJobAccountUnit(CreateJobAccountUnitInput input);

        /// <summary>
        ///  Update the JobAccount based on JobAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobAccountUnitDto> UpdateJobAccountUnit(UpdateJobAccountUnitInput input);

        /// <summary>
        /// Delete the JobAccount based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobAccountUnit(IdInput input);

        /// <summary>
        /// Get the list of all JobAccounts based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<JobAccountUnitDto>> GetJobAccountUnits(IdInput input);
    }
}
