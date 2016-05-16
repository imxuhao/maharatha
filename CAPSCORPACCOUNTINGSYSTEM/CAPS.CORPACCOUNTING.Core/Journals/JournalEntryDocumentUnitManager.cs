using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class JournalEntryDocumentUnitManager : DomainService
    {
        protected IRepository<JournalEntryDocumentUnit,long> JournalEntryDocumentUnitRepository { get; }
        public JournalEntryDocumentUnitManager(IRepository<JournalEntryDocumentUnit,long> journalEntryDocumentUnitrepository)
        {
            JournalEntryDocumentUnitRepository = journalEntryDocumentUnitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(JournalEntryDocumentUnit accountUnit)
        {
          return  await JournalEntryDocumentUnitRepository.InsertAndGetIdAsync(accountUnit);
        }


        public virtual async Task UpdateAsync(JournalEntryDocumentUnit accountUnit)
        {
            await JournalEntryDocumentUnitRepository.UpdateAsync(accountUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await JournalEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }

    }
}
