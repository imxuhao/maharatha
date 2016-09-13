using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Accounting
{
   public interface ISubAccountUnitAppService : IApplicationService
    {

        /// <summary>
        /// Create the SubAccount.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SubAccountUnitDto> CreateSubAccountUnit(CreateSubAccountUnitInput input);

        /// <summary>
        ///  Update the SubAccount based on SubAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SubAccountUnitDto> UpdateSubAccountUnit(UpdateSubAccountUnitInput input);

        /// <summary>
        /// Delete the SubAccount based on SubAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteSubAccountUnit(IdInput<long> input);

        /// <summary>
        /// Get the list of all SubAccount
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<SubAccountUnitDto>> GetSubAccountUnits(SearchInputDto input);

        /// <summary>
        /// Get SubAccount based on SubAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SubAccountUnitDto> GetSubAccountUnitsById(IdInput input);

        /// <summary>
        /// Get GetTypeofSubAccount List
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofSubAccountList();

       /// <summary>
       /// Get SubAccountRestrictions By SubAccountId
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>

       Task<List<SubAccountRestrictionUnitDto>> GetAccountRestrictionList(GetAccountRestrictionInput input);

        /// <summary>
        /// Get SubAccountRestrictions By SubAccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<SubAccountRestrictionUnitDto>> GetAccountList(GetAccountRestrictionInput input);

    
    }
}
