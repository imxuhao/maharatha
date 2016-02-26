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
        /// This is used to create the JobAccount.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobAccountUnitDto> CreateJobAccountUnit(CreateJobAccountUnitInput input);

        /// <summary>
        ///  This is used to update the JobAccount based on JobAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobAccountUnitDto> UpdateJobAccountUnit(UpdateJobAccountUnitInput input);

        /// <summary>
        /// This is used to delete the JobAccount based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobAccountUnit(IdInput input);

        /// <summary>
        /// This is used to get the list of all JobAccounts based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<JobAccountUnitDto>> GetJobAccountUnits(IdInput input);
    }
}
