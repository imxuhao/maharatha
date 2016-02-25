using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IEmployeeUnitAppService : IApplicationService
    {
        Task<EmployeeUnitDto> CreateEmployeeUnit(CreateEmployeeUnitInput input);
        Task<PagedResultOutput<EmployeeUnitDto>> GetEmployeeUnits(GetEmployeeInput input);
        Task<EmployeeUnitDto> UpdateEmployeeUnit(UpdateEmployeeUnitInput input);
        Task DeleteEmployeeUnit(IdInput input);
        Task<EmployeeUnitDto> GetEmployeeUnitsById(IdInput input);
    }
}