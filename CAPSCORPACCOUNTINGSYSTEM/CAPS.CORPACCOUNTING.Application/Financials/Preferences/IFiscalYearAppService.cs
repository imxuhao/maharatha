using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;


namespace CAPS.CORPACCOUNTING.Financials.Preferences.Dto
{
    /// <summary>
    /// This service will provide all CRUD operations on FiscalYear.
    /// </summary>
    public interface IFiscalYearAppService : IApplicationService
    {
        /// <summary>
        /// Create the FiscalYear.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateFiscalYearUnit(CreateFiscalYearUnitInput input);

        /// <summary>
        /// Get the list of all FiscalYear and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<FiscalYearUnitDto>> GetFiscalYearUnits(SearchInputDto input);

        /// <summary>
        /// Update the FiscalYear based on FiscalYearId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateFiscalYearUnit(UpdateFiscalYearUnitInput input);

        /// <summary>
        /// Delete the FiscalYear based on FiscalYearId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteFiscalYearUnit(IdInput input);

        /// <summary>
        /// Get the FiscalYear by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<FiscalYearUnitDto> GetFiscalYearById(IdInput input);

        /// <summary>
        /// Get the FiscalPeriods by FiscalYearId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<FiscalPeriodUnitDto>> GetFiscalPeriodUnits(GetFiscalPeriodDto input);

        /// <summary>
        ///  Delete FiscalPeriod
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteFiscalPeriodUnit(IdInput input);
    }
}