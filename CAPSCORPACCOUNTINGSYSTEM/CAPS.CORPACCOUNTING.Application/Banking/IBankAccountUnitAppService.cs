using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Banking
{
    public interface IBankAccountUnitAppService : IApplicationService
    {

        /// <summary>
        /// Create the BankAccount.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BankAccountUnitDto> CreateBankAccountUnit(CreateBankAccountUnitInput input);

        /// <summary>
        ///  Update the BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BankAccountUnitDto> UpdateBankAccountUnit(UpdateBankAccountUnitInput input);

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
        Task<PagedResultOutput<BankAccountAndAddressDto>> GetBankAccountUnits(SearchInputDto input);

        /// <summary>
        /// Get BankAccount based on BankAccountId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BankAccountUnitDto> GetBankAccountUnitsById(IdInput input);

    }
}
