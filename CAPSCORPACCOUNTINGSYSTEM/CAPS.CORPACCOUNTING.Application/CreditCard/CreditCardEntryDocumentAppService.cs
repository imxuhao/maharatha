using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.CreditCard.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Journals.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.ChargeEntry;

namespace CAPS.CORPACCOUNTING.CreditCard
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditCardEntryDocumentAppService : CORPACCOUNTINGServiceBase, ICreditCardEntryDocumentAppService
    {
        //private readonly JournalEntryDocumentUnitManager _journalEntryDocumentUnitManager;
        private readonly IRepository<ChargeEntryDocumentUnit, long> _chargeEntryDocumentUnitRepository;

        private readonly IRepository<ChargeEntryDocumentDetailUnit, long> _chargeEntryDocumentDetailUnitRepository;
        //private readonly JournalEntryDocumentDetailUnitManager _journalEntryDocumentDetailUnitManager;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="chargeentrydocumentunitrepository"></param>
        /// <param name="chargeentrydocumentdetailunitrepository"></param>
        public CreditCardEntryDocumentAppService(IRepository<ChargeEntryDocumentUnit, long> chargeentrydocumentunitrepository,
            IRepository<ChargeEntryDocumentDetailUnit, long> chargeentrydocumentdetailunitrepository
            )
        {
            _chargeEntryDocumentUnitRepository = chargeentrydocumentunitrepository;
            _chargeEntryDocumentDetailUnitRepository = chargeentrydocumentdetailunitrepository;
        }


        Task<IdOutputDto<long>> ICreditCardEntryDocumentAppService.CreateCreditCardEntryDocumentUnit(CreditCardEntryDocumentInputUnit input)
        {
            throw new NotImplementedException();
        }

        Task ICreditCardEntryDocumentAppService.DeleteCreditCardDetailUnit(IdInput<long> input)
        {
            throw new NotImplementedException();
        }

        Task ICreditCardEntryDocumentAppService.DeleteCreditCardEntryDocumentUnit(IdInput input)
        {
            throw new NotImplementedException();
        }

        Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> ICreditCardEntryDocumentAppService.GetCreditCardDetailsByDocumentId(GetTransactionList input)
        {
            throw new NotImplementedException();
        }

        Task<PagedResultOutput<CreditCardEntryDocumentUnitDto>> ICreditCardEntryDocumentAppService.GetCreditCardEntryDocumentUnits(SearchInputDto input)
        {
            throw new NotImplementedException();
        }

        Task ICreditCardEntryDocumentAppService.UpdateCreditCardEntryDocumentUnit(CreditCardEntryDocumentInputUnit input)
        {
            throw new NotImplementedException();
        }
    }
}
