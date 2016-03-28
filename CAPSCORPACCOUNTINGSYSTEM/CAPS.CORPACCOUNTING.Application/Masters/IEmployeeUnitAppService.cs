using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Employee.
    /// </summary>
    public interface IEmployeeUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the Employee.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeUnitDto> CreateEmployeeUnit(CreateEmployeeUnitInput input);

        /// <summary>
        /// Get the list of all Employees and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<EmployeeUnitDto>> GetEmployeeUnits(SearchInputDto input);

        /// <summary>
        /// Update the Employee based on EmployeeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeUnitDto> UpdateEmployeeUnit(UpdateEmployeeUnitInput input);

        /// <summary>
        /// Delete the Employee based on EmployeeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteEmployeeUnit(IdInput input);

        /// <summary>
        /// Get the Employee based on EmployeeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeUnitDto> GetEmployeeUnitsById(IdInput input);
    }
}