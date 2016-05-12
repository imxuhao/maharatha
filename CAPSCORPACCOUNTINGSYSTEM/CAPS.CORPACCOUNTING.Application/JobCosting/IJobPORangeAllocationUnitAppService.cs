using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public interface IJobPORangeAllocationUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the JobPORangeAllocation.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobPORangeAllocationUnitDto> CreateJobPORangeAllocationUnit(CreateJobPORangeAllocationInput input);

        /// <summary>
        /// Update the JobPORangeAllocation based on JobLocationId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobPORangeAllocationUnitDto> UpdateJobPORangeAllocationUnit(UpdateJobPORangeAllocationInput input);

        /// <summary>
        /// Get the list of all JobPORangeAllocation based on PORangeAllocationId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<JobPORangeAllocationUnitDto>> GetJobPORangeAllocationByJobId(GetJobInput input);

        /// <summary>
        /// Delete the JobPORangeAllocation based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteJobPORangeAllocationUnit(IdInput input);

    }
}
