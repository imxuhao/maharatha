using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on CustomerPaymentTerm.
    /// </summary>
    public interface ICustomerPaymentTermUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the CustomerPaymentTerm.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerPaymentTermUnitDto> CreateCustomerPaymentTermUnit(CreateCustomerPaymentTermUnitInput input);

        /// <summary>
        /// Get the list of all CustomerPaymentTerms and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CustomerPaymentTermUnitDto>> GetCustomerPaymentTermUnits(GetCustomerPaymentTermInput input);

        /// <summary>
        /// Update the CustomerPaymentTerm based on CustomerPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerPaymentTermUnitDto> UpdateCustomerPaymentTermUnit(UpdateCustomerPaymentTermUnitInput input);

        /// <summary>
        /// Delete the CustomerPaymentTerm based on CustomerPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCustomerPaymentTermUnit(IdInput input);

        /// <summary>
        /// Get the CustomerPaymentTerm based on CustomerPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerPaymentTermUnitDto> GetCustomerPayTermUnitsById(IdInput input);
    }
}