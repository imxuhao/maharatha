using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Collections.Generic;

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
        Task<PagedResultOutput<CustomerUnitDto>> GetCustomerUnits(SearchInputDto input);

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

        /// <summary>
        /// Get the CustomersList of Organization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetCustomerList(AutoSearchInput input);

        /// <summary>
        /// Get Payment Method List
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofPaymentMethodList();

        /// <summary>
        /// Get Customer Payment Terms
        /// </summary>
        /// <returns></returns>
          Task<List<NameValueDto>> GetCustomerPaymentTermsList(AutoSearchInput input);

        /// <summary>
        /// Get SalesRep as list
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetSalesRepList(AutoSearchInput input);
    }
}