using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// 
    /// </summary>
   public interface ITerritoriesUnitAppService: IApplicationService
    {
        /// <summary>
        ///  Create the Territories.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateTerritoriesUnit(CreateTerritoriesUnitInput input);

        /// <summary>
        ///  Get the list of all Territories.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<TerritoriesUnitDto>> GetTerritoriesUnits(SearchInputDto input);

        /// <summary>
        /// Update the TerritoriesUnit based on TerritorieId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateTerritoriesUnit(UpdateTerritoriesUnitInput input);

        /// <summary>
        ///  Delete the Territories based on TerritorieId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteTerritoriesUnit(IdInput input);
    }
}
