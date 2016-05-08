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
        /// Create the JobDetail. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobCommercialUnitDto> CreateJobDetailUnit(CreateJobCommercialInput input);

        /// <summary>
        /// Update the JobDetail based on JobDetailId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobCommercialUnitDto> UpdateJobDetailUnit(UpdateJobCommercialnput input);

        /// <summary>
        /// Delete the JobBudget based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobDetailUnit(IdInput input);
       
    }
}
