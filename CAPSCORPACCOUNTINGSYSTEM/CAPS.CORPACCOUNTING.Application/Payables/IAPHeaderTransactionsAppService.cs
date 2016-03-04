using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Payables.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Payables
{
    /// <summary>
    /// Provide all CRUD operations on APHeaderTransactions.
    /// </summary>
    public interface IAPHeaderTransactionsAppService : IApplicationService
    {
        /// <summary>
        /// Create the APHeaderTransaction.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAPHeaderTransactionUnit(CreateAPHeaderTransactionsInputUnit input);

        /// <summary>
        /// Update the APHeaderTransaction
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAPHeaderTransactionUnit(UpdateAPHeaderTransactionsInputUnit input);

        /// <summary>
        /// Delete ApHeader Transactions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAPHeaderTransactionUnit(IdInput input);
    }
}
