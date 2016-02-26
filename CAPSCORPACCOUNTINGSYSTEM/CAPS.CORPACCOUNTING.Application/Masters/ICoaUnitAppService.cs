using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface ICoaUnitAppService : IApplicationService
    {
        Task<CoaUnitDto> CreateCoaUnit(CreateCoaUnitInput input);

        /// <summary>
        /// This will return the list of COA based on Company ID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CoaUnitDto>> GetCoaUnits(GetCoaInput input);

        Task<CoaUnitDto> UpdateCoaUnit(UpdateCoaUnitInput input);
        Task DeleteCoaUnit(IdInput input);

        Task<CoaUnitDto> GetCoaUnitById(IdInput input);

    }
}