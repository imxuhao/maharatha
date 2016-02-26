using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on SalesRepresentative.
    /// </summary>
    public interface ISalesRepUnitAppService : IApplicationService
    {
        /// <summary>
        /// This is used to create the SalesRepresentative.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SalesRepUnitDto> CreateSalesRepUnit(CreateSalesRepUnitInput input);

        /// <summary>
        /// This is used to get the list of all SalesRepresentatives and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<SalesRepUnitDto>> GetSalesRepUnits(GetSalesRepInput input);

        /// <summary>
        /// This is used to update the SalesRepresentative based on SalesRepId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SalesRepUnitDto> UpdateSalesRepUnit(UpdateSalesRepUnitInput input);

        /// <summary>
        /// This is used to delete the SalesRepresentative based on SalesRepId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteSalesRepUnit(IdInput input);

        /// <summary>
        /// This is used to get the SalesRepresentative based on SalesRepId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SalesRepUnitDto> GetSalesRepUnitsById(IdInput input);
    }
}