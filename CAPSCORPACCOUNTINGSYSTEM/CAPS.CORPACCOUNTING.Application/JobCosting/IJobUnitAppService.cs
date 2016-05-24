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
        Task DeleteJobUnit(IdInputExtensionDto input);

        /// <summary>
        /// Get the list of all Jobs and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<JobCommercialUnitDto>> GetJobUnits(SearchInputDto input);

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

        /// <summary>
        /// Get JobAccounts by CoaId and JobId
        /// </summary>
        /// <returns></returns>
        Task<List<JobAccountUnitDto>> GetLineListByProjectCoa(GetJobAccountInputDto input);

        /// <summary>
        /// Get TaxRecovery
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetTaxRecovery();

        /// <summary>
        /// Get TaxCreditList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetTaxCreditList(AutoSearchInput input);
        /// <summary>
        /// Get Customers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetCustomersList(AutoSearchInput input);
    }
}
