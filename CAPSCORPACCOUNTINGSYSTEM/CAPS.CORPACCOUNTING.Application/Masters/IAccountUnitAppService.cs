using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;
using Castle.Core;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Uploads.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// This service will provide all CRUD operations on Account.
    /// </summary>
   
    public interface IAccountUnitAppService:IApplicationService
    {
        /// <summary>
        /// Create the Account.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
      
        Task<IdOutputDto<long>> CreateAccountUnit(CreateAccountUnitInput input);

        /// <summary>
        /// Update the Account based on AccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> UpdateAccountUnit(UpdateAccountUnitInput input);

        /// <summary>
        /// Get the list of all Accounts based on CoaId and also provided with Sorting,Paging and Searching functionality.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<AccountUnitDto>> GetAccountUnitsByCoaId(GetAccountInput input);

        /// <summary>
        /// Delete the Account based on AccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteAccountUnit(IdInput<long> input);

        /// <summary>
        /// Get the Account based on AccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AccountUnitDto> GetAccountUnitsById(IdInput input);

        /// <summary>
        /// Get TypeofConsolidation
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetTypeofConsolidationList();

        /// <summary>
        /// Get TypeOfAccount List
        /// </summary>
        /// <returns></returns>
          Task<List<NameValueDto>> GetTypeOfAccountList();

        /// <summary>
        /// Get TypeOfCurrency
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetTypeOfCurrencyList();

        /// <summary>
        /// Get LinkAccount List By CoaId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetLinkAccountListByCoaId(AutoSearchInput input);

        /// <summary>
        /// Get RollupAccountsList
        /// </summary>
        /// <returns></returns>
        Task<List<AccountCacheItem>> GetRollupAccountsList(AutoSearchInput input);
        /// <summary>
        /// Get TypeofCurrencyRate
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetTypeOfCurrencyRateList();

        /// <summary>
        /// Get Account by Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AccountUnitDto> GetAccountById(IdInput<long> input);

        /// <summary>
        /// BulkAccounts Upload
        /// </summary>
        /// <param name="accountList"></param>
        /// <returns></returns>
        Task<ErrorMessageswithAccountDto> BulkAccountUploads(CreateAccountListInput accountList);
    }
    
}
