using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Payables.Dto;

namespace CAPS.CORPACCOUNTING.Payables
{
    /// <summary>
    /// This service will provide all CRUD operations on Vendor.
    /// </summary>
    public interface IAPHeaderTransactionsAppService : IApplicationService
    {
        /// <summary>
        /// Create the Vendor.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAPHeaderTransactionUnit(CreateAPHeaderTransactionsInputUnit input);
    }
}
