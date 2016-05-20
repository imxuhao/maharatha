using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Banking
{
    public  interface IBatchUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the BatchUnit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateBatchUnit(CreateBatchUnitInput input);

        /// <summary>
        ///  Update the BatchUnit based on BatchId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateBatchUnit(UpdateBatchUnitInput input);

        /// <summary>
        /// Delete the BatchUnit based on BatchId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteBatchUnit(IdInput input);

        /// <summary>
        /// Get the list of all Batch List
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<BatchUnitDto>> GetBatchUnits(SearchInputDto input);

        /// <summary>
        /// Get BatchUnit based on BatchId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BatchUnitDto> GetBatchUnitsById(IdInput input);



        /// <summary>
        /// Get BatchUnit List for AutoSearch
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetBatchList(AutoSearchInput input);
    }
}
