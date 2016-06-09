using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class JournalEntryDocumentDetailUnitManager : DomainService
    {
        protected IRepository<JournalEntryDocumentDetailUnit,long> JournalEntryDocumentDetailUnitRepository { get; }
        public JournalEntryDocumentDetailUnitManager(IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitrepository)
        {
            JournalEntryDocumentDetailUnitRepository = journalEntryDocumentDetailUnitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(JournalEntryDocumentDetailUnit input)
        {

          return  await JournalEntryDocumentDetailUnitRepository.InsertAndGetIdAsync(input);
        }


        public virtual async Task UpdateAsync(JournalEntryDocumentDetailUnit input)
        {
            await JournalEntryDocumentDetailUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await JournalEntryDocumentDetailUnitRepository.DeleteAsync(input.Id);
        }

    }
}
