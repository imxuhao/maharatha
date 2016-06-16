using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.UI;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
   public class ArInvoiceEntryDocumentUnitManager : DomainService
    {

        public IRepository<ArInvoiceEntryDocumentUnit, long> ArInvoiceEntryDocumentUnitRepository { get; }
        public ArInvoiceEntryDocumentUnitManager(IRepository<ArInvoiceEntryDocumentUnit, long> arInvoiceEntryDocumentUnitRepository)
        {
            ArInvoiceEntryDocumentUnitRepository = arInvoiceEntryDocumentUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(ArInvoiceEntryDocumentUnit arInvoiceEntry)
        {
            await ArInvoiceEntryDocumentUnitAsync(arInvoiceEntry);
            return await ArInvoiceEntryDocumentUnitRepository.InsertAndGetIdAsync(arInvoiceEntry);
        }


        public virtual async Task UpdateAsync(ArInvoiceEntryDocumentUnit arInvoiceEntry)
        {
            await ArInvoiceEntryDocumentUnitAsync(arInvoiceEntry);
            await ArInvoiceEntryDocumentUnitRepository.UpdateAsync(arInvoiceEntry);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await ArInvoiceEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }
        protected virtual async Task ArInvoiceEntryDocumentUnitAsync(ArInvoiceEntryDocumentUnit arInvoiceEntry)
        {
            //Validating if Duplicate DocumentReference(AR Invoice#) exists
            if (ArInvoiceEntryDocumentUnitRepository != null)
            {

                var arInvoices = (await ArInvoiceEntryDocumentUnitRepository.
                    GetAllListAsync(p => p.DocumentReference == arInvoiceEntry.DocumentReference && p.OrganizationUnitId == arInvoiceEntry.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.AccountsReceivable));

                if (arInvoiceEntry.Id == 0)
                {
                    if (arInvoices.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate AR Invoice#", arInvoiceEntry.DocumentReference));
                    }
                }
                else
                {
                    if (arInvoices.FirstOrDefault(p => p.Id != arInvoiceEntry.Id && p.DocumentReference == arInvoiceEntry.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate AR Invoice#", arInvoiceEntry.DocumentReference));
                    }
                }

            }
        }
    }
}

