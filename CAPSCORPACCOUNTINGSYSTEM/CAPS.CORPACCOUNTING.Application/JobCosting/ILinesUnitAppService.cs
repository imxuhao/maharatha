using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// This service will provide all CRUD operations on Account.
    /// </summary>
    public interface ILinesUnitAppService:IApplicationService
    {
      

        /// <summary>
        /// Create the New Line.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AccountUnitDto> CreateLineUnit(CreateAccountUnitInput input);

        /// <summary>
        /// Update the Line based on AccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AccountUnitDto> UpdateLineUnit(UpdateAccountUnitInput input);

      

        /// <summary>
        /// Delete the Account based on AccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteLineUnit(IdInput<long> input);

        /// <summary>
        /// Get Lines by ProjectCoaId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<AccountUnitDto>> GetLinesByCoaId(GetAccountInput input);
    }
}
