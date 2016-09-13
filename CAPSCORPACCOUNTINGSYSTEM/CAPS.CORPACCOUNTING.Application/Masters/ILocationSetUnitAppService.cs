using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on LocationSet.
    /// </summary>
    public interface ILocationSetUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the LocationSet.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LocationSetUnitDto> CreateLocationSetUnit(CreateLocationSetUnitInput input);

        /// <summary>
        /// Update the LocationSet based on LocationSetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LocationSetUnitDto> UpdateLocationSetUnit(UpdateLocationSetUnitInput input);

        /// <summary>
        /// Delete the LocationSet based on LocationSetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteLocationSetUnit(IdInput input);

        /// <summary>
        /// Get the LocationSet based on LocationSetId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<LocationSetUnitDto> GetLocationSetUnitsById(IdInput input);
    
    }
}
