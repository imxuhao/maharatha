using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Helpers;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Journals.dto;
using CAPS.CORPACCOUNTING.Journals.Dto;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class JournalEntryDocDetailAppService : CORPACCOUNTINGServiceBase, IJournalEntryDocDetailAppService
    {
        private readonly JournalEntryDocumentDetailUnitManager _journalEntryDocumentDetailUnitManager;
        private readonly IRepository<JournalEntryDocumentDetailUnit, long> _journalEntryDocumentDetailUnitRepository;
        private IdOutputDto<long> _response = null;

        public JournalEntryDocDetailAppService(JournalEntryDocumentDetailUnitManager journalEntryDocumentDetailUnitManager,
            IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitRepository)

        {
            _journalEntryDocumentDetailUnitManager = journalEntryDocumentDetailUnitManager;
            _journalEntryDocumentDetailUnitRepository = journalEntryDocumentDetailUnitRepository;
        }


        public async Task CreateJournalEntryDocDetailUnit(List<CreateJournalEntryDocDetailInputUnit> input)
        {
            foreach (var journaldocDetails in input)
            {
                var journalEntryDocDetailUnit = journaldocDetails.MapTo<JournalEntryDocumentDetailUnit>();
                await _journalEntryDocumentDetailUnitManager.CreateAsync(journalEntryDocDetailUnit);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteJournalEntryDocDetailUnit(IdInput<long> input)
        {
            await _journalEntryDocumentDetailUnitManager.DeleteAsync(input);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResultOutput<JournalEntryDocDetailUnitDto>> GetJournalEntryDocDetailsByAccountingDocId(GetTransactionList input)
        {
            var query = from journals in _journalEntryDocumentDetailUnitRepository.GetAll()
                        select new { JournalDetails = journals };
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
           // query = query.Where(p => p.JournalDetails.AccountingDocumentId == input.AccountingDocumentId);
            query = query.WhereIf(!ReferenceEquals(input.OrganizationUnitId, null), p => p.JournalDetails.OrganizationUnitId == input.OrganizationUnitId);

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("JournalDetails.ItemMemo ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();

            return new PagedResultOutput<JournalEntryDocDetailUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.JournalDetails.MapTo<JournalEntryDocDetailUnitDto>();
                dto.AccountId = item.JournalDetails.Id;
                return dto;
            }).ToList());
        }

        public async Task UpdateJournalEntryDocumentUnit(List<UpdateJournalEntryDocDetailInputUnit> input)
        {
            foreach (var journaldocDetails in input)
            {
                if (journaldocDetails.AccountingItemId > 0)
                {
                    var journalEntryDocDetailUnit = journaldocDetails.MapTo<JournalEntryDocumentDetailUnit>();
                    journalEntryDocDetailUnit.Id = journaldocDetails.AccountingItemId;
                    await _journalEntryDocumentDetailUnitManager.UpdateAsync(journalEntryDocDetailUnit);

                    await CurrentUnitOfWork.SaveChangesAsync();
                }
                else if (journaldocDetails.AccountingItemId < 0)
                {
                    IdInput<long> idInput = new IdInput<long>() { Id = (journaldocDetails.AccountingItemId * (-1)) };
                    await _journalEntryDocumentDetailUnitManager.DeleteAsync(idInput);
                }
                else
                {
                    var journalEntryDocDetailUnit = journaldocDetails.MapTo<JournalEntryDocumentDetailUnit>();
                    await _journalEntryDocumentDetailUnitManager.CreateAsync(journalEntryDocDetailUnit);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }

            }
        }


    }
}
