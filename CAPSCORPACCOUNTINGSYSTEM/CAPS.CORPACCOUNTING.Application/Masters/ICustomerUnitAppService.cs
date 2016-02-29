using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Customer.
    /// </summary>
    public interface ICustomerUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the Customer.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerUnitDto> CreateCustomerUnit(CreateCustomerUnitInput input);

        /// <summary>
        /// Get the list of all Customers and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CustomerUnitDto>> GetCustomerUnits(GetCustomerInput input);

        /// <summary>
        /// Update the Customer based on CustomerId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerUnitDto> UpdateCustomerUnit(UpdateCustomerUnitInput input);

        /// <summary>
        /// Delete the Customer based on CustomerId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCustomerUnit(IdInput input);

        /// <summary>
        /// Get the Customer based on CustomerId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CustomerUnitDto> GetCustomerUnitsById(IdInput input);
    }
}