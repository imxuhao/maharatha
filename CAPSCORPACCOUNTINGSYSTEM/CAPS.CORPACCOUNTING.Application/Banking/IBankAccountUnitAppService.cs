using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Helpers.CacheItems;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Banking
{
    public interface IBankAccountUnitAppService : IApplicationService
    {

        /// <summary>
        /// Create the BankAccount.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateBankAccountUnit(CreateBankAccountUnitInput input);

        /// <summary>
        ///  Update the BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateBankAccountUnit(UpdateBankAccountUnitInput input);

        /// <summary>
        /// Delete the BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteBankAccountUnit(IdInput<long> input);

        /// <summary>
        /// Get the list of all BankAccounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<BankAccountUnitDto>> GetBankAccountUnits(SearchInputDto input);

        /// <summary>
        /// Get BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BankAccountUnitDto> GetBankAccountUnitsById(IdInput<long> input);

        /// <summary>
        /// Get BankAccountTypeList
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetBankAccountTypeList();

        /// <summary>
        /// Get AccountType as Bank of all Corporate AccountList
        /// </summary>
        /// <returns></returns>
        Task<List<AccountCacheItem>> GetCorporateAccountList(AutoSearchInput input);

        /// <summary>
        /// Get CheckStoockList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetCheckStockList(AutoSearchInput input);

        /// <summary>
        /// Get BankAccountPaymentRangeList By BankAccountId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<BankAccountPaymentRangeDto>> GetBankAccountPaymentRangeByBankAccountId(GetBankAccoutPaymentRangeDto input);

        /// <summary>
        /// Delete BankAccountPaymentRange By Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteBankAccountPaymentRange(IdInput input);

        /// <summary>
        /// Get UploadMethodList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetUploadMethodList(AutoSearchInput input);
        /// <summary>
        /// Get PositivePayList 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<NameValueDto>> GetPositivePayList(AutoSearchInput input);

    }
}
