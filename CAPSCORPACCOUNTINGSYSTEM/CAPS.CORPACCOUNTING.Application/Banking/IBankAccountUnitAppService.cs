using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Threading.Tasks;
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
        Task DeleteBankAccountUnit(IdInput input);

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
        Task<BankAccountUnitDto> GetBankAccountUnitsById(IdInput input);

        /// <summary>
        /// Get BankAccountTypeList
        /// </summary>
        /// <returns></returns>
        List<NameValueDto> GetBankAccountTypeList();

        /// <summary>
        /// Get AccountType as Bank of all Corporate AccountList
        /// </summary>
        /// <returns></returns>
        Task<List<AutoFillDto>> GetCorporateAccountList(AutoSearchInput input);

        /// <summary>
        /// Get CheckStoockList
        /// </summary>
        /// <returns></returns>
        Task<List<NameValueDto>> GetCheckStoockList(AutoSearchInput input);

    }
}
