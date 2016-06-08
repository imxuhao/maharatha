using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.UI;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Journals;

namespace CAPS.CORPACCOUNTING.Payables
{
    public class APHeaderTransactionsUnitManager : DomainService
    {
        protected IRepository<ApHeaderTransactions,long> ApHeaderTransactionsUnitRepository { get; }
        public APHeaderTransactionsUnitManager(IRepository<ApHeaderTransactions,long> apheadertransactionsunitrepository)
        {
            ApHeaderTransactionsUnitRepository = apheadertransactionsunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(ApHeaderTransactions input)
        {
           return await ApHeaderTransactionsUnitRepository.InsertAndGetIdAsync(input);
        }

        public virtual async Task UpdateAsync(ApHeaderTransactions input)
        {
            await ApHeaderTransactionsUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await ApHeaderTransactionsUnitRepository.DeleteAsync(input.Id);
        }

        protected virtual async Task ValidateJournalUnitAsync(ApHeaderTransactions invoiceunit)
        {
            //Validating if Duplicate DocumentReference(INVOICE#) exists
            if (ApHeaderTransactionsUnitRepository != null)
            {

                var invoices = (await ApHeaderTransactionsUnitRepository.
                    GetAllListAsync(
                        p => p.DocumentReference == invoiceunit.DocumentReference && p.OrganizationUnitId == invoiceunit.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.GeneralLedger));

                if (invoiceunit.Id == 0)
                {
                    if (invoices.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Invoice#", invoiceunit.DocumentReference));
                    }
                }
                else
                {
                    if (invoices.FirstOrDefault(p => p.Id != invoiceunit.Id && p.DocumentReference == invoiceunit.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Invoice#", invoiceunit.DocumentReference));
                    }
                }

            }
        }
    }
}
