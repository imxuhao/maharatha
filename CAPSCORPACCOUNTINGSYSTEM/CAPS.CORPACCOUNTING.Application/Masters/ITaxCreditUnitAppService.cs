using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on TaxCredit.
    /// </summary>
    public interface ITaxCreditUnitAppService : IApplicationService
    {
        /// <summary>
        ///  Create the TaxCredit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateTaxCreditUnit(CreateTaxCreditUnitInput input);

        /// <summary>
        /// Get the list of all TaxCredit.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultOutput<TaxCreditUnitDto>> GetTaxCreditUnits(SearchInputDto input);

        /// <summary>
        /// Update the TaxCredit based on TaxCreditId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateTaxCreditUnit(UpdateTaxCreditUnitInput input);

        /// <summary>
        /// Delete the TaxCredit based on TaxCreditId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteTaxCreditUnit(IdInput input);
    }
}
