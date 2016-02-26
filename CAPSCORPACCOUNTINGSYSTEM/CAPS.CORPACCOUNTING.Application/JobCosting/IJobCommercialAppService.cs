using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on JobDetail.
    /// </summary>
    public interface IJobCommercialAppService : IApplicationService
    {
        /// <summary>
        /// This is used to create the JobDetail. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobCommercialUnitDto> CreateJobDetailUnit(CreateJobCommercialInput input);

        /// <summary>
        /// This is used to update the JobDetail based on JobDetailId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobCommercialUnitDto> UpdateJobDetailUnit(UpdateJobCommercialnput input);

        /// <summary>
        /// This is used to delete the JobBudget based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobDetailUnit(IdInput input);

        /// <summary>
        /// This is used to get the JobBudget based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobCommercialUnitDto> GetJobDetailsByJobId(IdInput input);
    }
}
