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
        private readonly IRepository<JournalEntryDocumentUnit, long> JournalEntryDocumentUnitRepository;
        public JournalEntryDocumentUnitManager(IRepository<JournalEntryDocumentUnit, long> journalEntryDocumentUnitrepository)
        {
            JournalEntryDocumentUnitRepository = journalEntryDocumentUnitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(JournalEntryDocumentUnit journalUnit)
        {

            await ValidateJournalUnitAsync(journalUnit);
            return await JournalEntryDocumentUnitRepository.InsertAndGetIdAsync(journalUnit);
        }


        [UnitOfWork]
        public virtual async Task<long> CreateRecurringAsync(long journalId)
        {
            var newjournalDocumentunit = JournalEntryDocumentUnitRepository.Get(journalId);

           // Adding the Parent Reference
            newjournalDocumentunit.OriginalDocumentId = journalId;

            // Changing the Description
            newjournalDocumentunit.DocumentReference= newjournalDocumentunit.DocumentReference+"-"+
            JournalEntryDocumentUnitRepository.GetAll().Count(p => p.OriginalDocumentId == journalId) + 1;

           // await ValidateJournalUnitAsync(newjournalDocumentunit);
            return await JournalEntryDocumentUnitRepository.InsertAndGetIdAsync(newjournalDocumentunit);
        }
        public virtual async Task UpdateAsync(JournalEntryDocumentUnit journalUnit)
        {
            await ValidateJournalUnitAsync(journalUnit);
            await JournalEntryDocumentUnitRepository.UpdateAsync(journalUnit);
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
                    GetAllListAsync(p => p.DocumentReference == journalunit.DocumentReference && p.OrganizationUnitId == journalunit.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.GeneralLedger));

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
