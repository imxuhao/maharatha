using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on Job.
    /// </summary>
    public interface IDivisionUnitAppService :IApplicationService
    {
        /// <summary>
        ///  Create the Division.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<int>> CreateDivisionUnit(CreateJobUnitInput input);

        /// <summary>
        /// Update the Job Division on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<int>> UpdateDivisionUnit(UpdateJobUnitInput input);

        /// <summary>
        /// Delete the Division based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteDivisionUnit(IdInput input);

        /// <summary>
        ///Get the list of all Divisions(Jobs) and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JobUnitDto>> GetDivisionUnits(SearchInputDto input);

        

    }
}
