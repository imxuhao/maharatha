using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Payroll.Dto;

namespace CAPS.CORPACCOUNTING.Payroll
{
    /// <summary>
    /// Provide all CRUD operations on PayrollEntryDocument.
    /// </summary>
    public interface IPayrollEntryDocumentAppService : IApplicationService
    {
        /// <summary>
        /// Create the PayrollEntryDocument.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreatePayrollEntryDocumentUnit(PayrollEntryDocumentInputUnit input);

        /// <summary>
        /// Update the PayrollEntryDocument
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdatePayrollEntryDocumentUnit(PayrollEntryDocumentInputUnit input);

        /// <summary>
        /// Delete PayrollEntryDocument
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeletePayrollEntryDocumentUnit(IdInput<long> input);

        /// <summary>
        /// Get all PayrollEntryDocument with Paging and Sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<PayrollEntryDocumentUnitDto>> GetPayrollEntryDocumentUnits(SearchInputDto input);

        /// <summary>
        /// Get PayrollEntryDocumentDetails By AccountingDocumentID
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<PayrollEntryDocumentDetailUnitDto>> GetPayrollEntryDocumentDetailsByAccountingDocumentId(GetTransactionList input);
    }
}
