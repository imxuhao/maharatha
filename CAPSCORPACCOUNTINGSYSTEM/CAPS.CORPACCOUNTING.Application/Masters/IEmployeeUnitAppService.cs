using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Employee.
    /// </summary>
    public interface IEmployeeUnitAppService : IApplicationService
    {
        /// <summary>
        ///  This is used to create the Employee.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeUnitDto> CreateEmployeeUnit(CreateEmployeeUnitInput input);

        /// <summary>
        /// This is used to get the list of all Employees and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<EmployeeUnitDto>> GetEmployeeUnits(GetEmployeeInput input);

        /// <summary>
        /// This is used to update the Employee based on EmployeeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeUnitDto> UpdateEmployeeUnit(UpdateEmployeeUnitInput input);

        /// <summary>
        /// This is used to delete the Employee based on EmployeeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteEmployeeUnit(IdInput input);

        /// <summary>
        /// This is used to get the Employee based on EmployeeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EmployeeUnitDto> GetEmployeeUnitsById(IdInput input);
    }
}