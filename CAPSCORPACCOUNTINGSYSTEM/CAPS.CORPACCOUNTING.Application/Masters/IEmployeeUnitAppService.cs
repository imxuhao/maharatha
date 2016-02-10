using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IEmployeeUnitAppService : IApplicationService
    {
        Task<EmployeeUnitDto> CreateEmployeeUnit(CreateEmployeeUnitInput input);

        Task<ListResultOutput<EmployeeUnitDto>> GetEmployeeUnits(GetEmployeeInput input);

        Task<EmployeeUnitDto> UpdateEmployeeUnit(UpdateEmployeeUnitInput input);
        Task DeleteEmployeeUnit(IdInput input);

        Task InsertEmployeeData(CreateEmployeeUnitInput input);

        Task UpdatedEmployeeData(UpdateEmployeeUnitInput input);

        Task<EmployeeUnitDto> GetEmployeeUnitsById(IdInput input);
    }
}