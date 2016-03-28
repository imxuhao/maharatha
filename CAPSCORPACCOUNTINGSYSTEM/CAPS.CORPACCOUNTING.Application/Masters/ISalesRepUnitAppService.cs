using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on SalesRepresentative.
    /// </summary>
    public interface ISalesRepUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the SalesRepresentative.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SalesRepUnitDto> CreateSalesRepUnit(CreateSalesRepUnitInput input);

        /// <summary>
        /// Get the list of all SalesRepresentatives and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<SalesRepUnitDto>> GetSalesRepUnits(SearchInputDto input);

        /// <summary>
        /// Update the SalesRepresentative based on SalesRepId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SalesRepUnitDto> UpdateSalesRepUnit(UpdateSalesRepUnitInput input);

        /// <summary>
        /// Delete the SalesRepresentative based on SalesRepId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteSalesRepUnit(IdInput input);

        /// <summary>
        /// Get the SalesRepresentative based on SalesRepId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SalesRepUnitDto> GetSalesRepUnitsById(IdInput input);
    }
}