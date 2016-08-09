using Abp.Application.Services;
using CAPS.CORPACCOUNTING.Security.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security
{

    /// <summary>
    /// 
    /// </summary>
    public interface IUserSecuritySettingsAppService : IApplicationService
    {

        #region Account/Lines
        /// <summary>
        /// Create or Update Accounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateorUpdateAccountAccessList(UserSecuritySettingsInputUnit input);

        /// <summary>
        /// Create or Update Lines
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateorUpdateLineAccessList(UserSecuritySettingsInputUnit input);
        /// <summary>
        /// Get Accounts/Lines AccessList By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AccountAccessListUnitDto>> GetAccountAccessList(GetUserSecuritySettingsInputUnit input);


        /// <summary>
        /// Get Account/Lines List which is not in AccountAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<AccountAccessListUnitDto>> GetAccountList(GetUserSecuritySettingsInputUnit input);

        #endregion

        #region Projects/Divisions
        /// <summary>
        /// Create or Update Projects
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateorUpdateProjectAccessList(UserSecuritySettingsInputUnit input);

        /// <summary>
        /// Create or Update Divisions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateorUpdateDivisionAccessList(UserSecuritySettingsInputUnit input);


        /// <summary>
        /// Get Project/Division AccessList By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ProjectAccessListUnitDto>> GetProjectAccessList(GetUserSecuritySettingsInputUnit input);

        /// <summary>
        /// Get Project/Division List which is not in ProjectAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ProjectAccessListUnitDto>> GetProjectList(GetUserSecuritySettingsInputUnit input);
        #endregion

        #region Credit Card

        /// <summary>
        /// Create or Update Credit Cards
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateorUpdateCreditCardAccessList(UserSecuritySettingsInputUnit input);

        /// <summary>
        /// Get Credit Card Access List By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CreditAccessListUnitDto>> GetCreditCardAccessList(GetUserSecuritySettingsInputUnit input);

        /// <summary>
        /// Get Project/Division List which is not in ProjectAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CreditAccessListUnitDto>> GetCreditCardList(GetUserSecuritySettingsInputUnit input);

        #endregion

        #region Bank Account
        /// <summary>
        /// Create or Update Bank Accounts
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateorUpdateBankAccountAccessList(UserSecuritySettingsInputUnit input);


        /// <summary>
        /// Get Bank Accounts Access List By UserId
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<BankAccountAccessListUnitDto>> GetBankAccountAccessList(GetUserSecuritySettingsInputUnit input);

        /// <summary>
        /// Get Bank Accounts List which is not in BankAccountsAccessList
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<BankAccountAccessListUnitDto>> GetBankAccountList(GetUserSecuritySettingsInputUnit input);
        #endregion
    }
}
