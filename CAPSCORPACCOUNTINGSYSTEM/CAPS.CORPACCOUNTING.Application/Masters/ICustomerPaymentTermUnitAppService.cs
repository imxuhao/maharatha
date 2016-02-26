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
        /// This is used to create the CustomerPaymentTerm.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerPaymentTermUnitDto> CreateCustomerPaymentTermUnit(CreateCustomerPaymentTermUnitInput input);

        /// <summary>
        /// This is used to get the list of all CustomerPaymentTerms and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CustomerPaymentTermUnitDto>> GetCustomerPaymentTermUnits(GetCustomerPaymentTermInput input);

        /// <summary>
        /// This is used to update the CustomerPaymentTerm based on CustomerPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerPaymentTermUnitDto> UpdateCustomerPaymentTermUnit(UpdateCustomerPaymentTermUnitInput input);

        /// <summary>
        /// This is used to delete the CustomerPaymentTerm based on CustomerPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCustomerPaymentTermUnit(IdInput input);

        /// <summary>
        /// This is used to get the CustomerPaymentTerm based on CustomerPaymentTermId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerPaymentTermUnitDto> GetCustomerPayTermUnitsById(IdInput input);
    }
}