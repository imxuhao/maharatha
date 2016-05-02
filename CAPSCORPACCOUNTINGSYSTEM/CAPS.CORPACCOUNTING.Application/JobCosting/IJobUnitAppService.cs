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
        Task<PagedResultOutput<JobUnitDto>> GetJobUnits(SearchInputDto input);

        /// <summary>
        /// Get the Job based on JobId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JobUnitDto> GetJobUnitById(IdInput input);

        /// <summary>
        /// Get Organizations of Tenant except input Organization.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetOrganizationUnits(IdInput input);      

        /// <summary>
        /// Get DivisionsList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetDivisionList(AutoSearchInput input);

        /// <summary>
        /// Get ProjectStatusList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetProjectStatusList();

        /// <summary>
        /// Get BudgetSoftwareList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetBudgetSoftwareList();
        /// <summary>
        /// Get ProjectCoaList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetProjectCoaList(AutoSearchInput input);
        /// <summary>
        /// Get RollupAccountList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetRollupAccountList(AutoSearchInput input);

        /// <summary>
        /// Get AllFinancialRollupAccountList 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetGenericRollupAccountsList(AutoSearchInput input);
        /// <summary>
        /// Get ProjectTypeList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetProjectTypeList();

    }
}
