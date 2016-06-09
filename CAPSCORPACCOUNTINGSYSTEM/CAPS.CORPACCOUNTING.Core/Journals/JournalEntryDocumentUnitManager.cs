using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Linq;
using Abp.UI;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class JournalEntryDocumentUnitManager : DomainService
    {
        protected IRepository<JournalEntryDocumentUnit, long> JournalEntryDocumentUnitRepository { get; }
        public JournalEntryDocumentUnitManager(IRepository<JournalEntryDocumentUnit, long> journalEntryDocumentUnitrepository)
        {
            JournalEntryDocumentUnitRepository = journalEntryDocumentUnitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(JournalEntryDocumentUnit accountUnit)
        {
            await ValidateJournalUnitAsync(accountUnit);
            return await JournalEntryDocumentUnitRepository.InsertAndGetIdAsync(accountUnit);
        }


        public virtual async Task UpdateAsync(JournalEntryDocumentUnit accountUnit)
        {
            await ValidateJournalUnitAsync(accountUnit);
            await JournalEntryDocumentUnitRepository.UpdateAsync(accountUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await JournalEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }
        protected virtual async Task ValidateJournalUnitAsync(JournalEntryDocumentUnit journalunit)
        {
            //Validating if Duplicate DocumentReference(Journal#) exists
            if (JournalEntryDocumentUnitRepository != null)
            {

                var journals = (await JournalEntryDocumentUnitRepository.
                    GetAllListAsync(
                        p => p.DocumentReference == journalunit.DocumentReference && p.OrganizationUnitId == journalunit.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId==TypeOfAccountingDocument.GeneralLedger));

                if (journalunit.Id == 0)
                {
                    if (journals.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Journal#", journalunit.DocumentReference));
                    }
                }
                else
                {
                    if (journals.FirstOrDefault(p => p.Id != journalunit.Id && p.DocumentReference == journalunit.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Journal#", journalunit.DocumentReference));
                    }
                }

            }
        }
    }
}
