using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;


namespace CAPS.CORPACCOUNTING.Financials.Preferences.Dto
{
    /// <summary>
    /// This service will provide all CRUD operations on COA.
    /// </summary>
    public interface IFiscalPeriodAppService : IApplicationService
    {
        /// <summary>
        /// Create the FiscalPeriod.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateFiscalPeriodUnit(CreateFiscalPeriodUnitInput input);

        /// <summary>
        /// Get the list of all FiscalPeriod and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<FiscalPeriodUnitDto>> GetFiscalPeriodUnits(SearchInputDto input);

        /// <summary>
        /// Update the FiscalPeriod based on FiscalPeriodId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFiscalPeriodUnit(UpdateFiscalPeriodUnitInput input);

        /// <summary>
        /// Delete the FiscalPeriod based on FiscalPeriodId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteFiscalPeriodUnit(IdInput input);
       

    }
}