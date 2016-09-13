using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.UI;
using CAPS.CORPACCOUNTING.Accounting;
namespace CAPS.CORPACCOUNTING.ChargeEntry
{
   public class ChargeEntryDocumentUnitManager : DomainService
    {
        public IRepository<ChargeEntryDocumentUnit, long> ChargeEntryDocumentUnitRepository { get; }
        public ChargeEntryDocumentUnitManager(IRepository<ChargeEntryDocumentUnit, long> chargeEntryDocumentUnitRepository)
        {
            ChargeEntryDocumentUnitRepository = chargeEntryDocumentUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(ChargeEntryDocumentUnit creditCardEntry)
        {
            await ChargeInvoiceEntryDocumentUnitAsync(creditCardEntry);
            return await ChargeEntryDocumentUnitRepository.InsertAndGetIdAsync(creditCardEntry);
        }


        public virtual async Task UpdateAsync(ChargeEntryDocumentUnit creditCardEntry)
        {
            await ChargeInvoiceEntryDocumentUnitAsync(creditCardEntry);
            await ChargeEntryDocumentUnitRepository.UpdateAsync(creditCardEntry);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await ChargeEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }
        protected virtual async Task ChargeInvoiceEntryDocumentUnitAsync(ChargeEntryDocumentUnit creditCardEntry)
        {
            //Validating if Duplicate DocumentReference(AR Invoice#) exists
            if (ChargeEntryDocumentUnitRepository != null)
            {

                var chargeInvoices = (await ChargeEntryDocumentUnitRepository.
                    GetAllListAsync(p => p.DocumentReference == creditCardEntry.DocumentReference && p.OrganizationUnitId == creditCardEntry.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.CreditCard));

                if (creditCardEntry.Id == 0)
                {
                    if (chargeInvoices.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Invoice#", creditCardEntry.DocumentReference));
                    }
                }
                else
                {
                    if (chargeInvoices.FirstOrDefault(p => p.Id != creditCardEntry.Id && p.DocumentReference == creditCardEntry.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Invoice#", creditCardEntry.DocumentReference));
                    }
                }

            }
        }
    }
}
