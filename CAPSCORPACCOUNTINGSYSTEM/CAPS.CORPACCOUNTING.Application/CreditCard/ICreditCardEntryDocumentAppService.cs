using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.CreditCard.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.CreditCard
{
    /// <summary>
    /// 
    /// </summary>
   public interface ICreditCardEntryDocumentAppService : IApplicationService
    {
        /// <summary>
        /// Create CreditCard Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IdOutputDto<long>> CreateCreditCardEntryDocumentUnit(CreditCardEntryDocumentInputUnit input);

        /// <summary>
        /// Update CreditCard Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateCreditCardEntryDocumentUnit(CreditCardEntryDocumentInputUnit input);

        /// <summary>
        /// Delete CreditCard Entry Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCreditCardEntryDocumentUnit(IdInput input);

        /// <summary>
        /// Delete CreditCard Detail Document.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteCreditCardDetailUnit(IdInput input);

        /// <summary>
        /// Get CreditCard Entry Statements List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CreditCardStatementDto>> GetCreditCardStatementsUnits(SearchInputDto input);


        /// <summary>
        /// Get CreditCard Details by AccountingDocumentId List.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> GetCreditCardDetailsByDocumentId(GetTransactionList input);

        /// <summary>
        /// Get CreditCard History.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> GetCreditCardHistory(SearchInputDto input);



    }
}
