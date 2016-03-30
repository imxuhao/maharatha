using System.Threading.Tasks;
using Abp.Application.Services;
using CAPS.CORPACCOUNTING.JobCosting.Dto;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on ARBillingType.
    /// </summary>
    public interface IARBillingTypeUnitAppService : IApplicationService
    {
        /// <summary>
        /// Create the ARBillingType.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ARBillingTypeDto> CreateARBillingTypeUnit(CreateARBillingTypeUnitInput input);

        /// <summary>
        ///Update ARBillingType based on ARBillingTypeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ARBillingTypeDto> UpdateARBillingTypeUnit(UpdateARBillingTypeUnitInput input);

        /// <summary>
        /// Delete ARBillingType based on ARBillingTypeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteARBillingTypeUnit(IdInput input);

        /// <summary>
        /// Get the ARBillingType based on ARBillingTypeId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ARBillingTypeDto> GetARBillingTypeUnitById(IdInput input);
    }
}

