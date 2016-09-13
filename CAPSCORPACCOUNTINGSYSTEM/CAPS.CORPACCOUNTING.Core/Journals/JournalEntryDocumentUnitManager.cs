using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Linq;
using Abp.UI;
using CAPS.CORPACCOUNTING.Accounting;
using AutoMapper;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class JournalEntryDocumentUnitManager : DomainService
    {
        private readonly IRepository<JournalEntryDocumentUnit, long> _journalEntryDocumentUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<JournalEntryDocumentDetailUnit, long> _journalEntryDocumentDetailUnitRepository;

        public JournalEntryDocumentUnitManager(IRepository<JournalEntryDocumentUnit, long> journalEntryDocumentUnitrepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<JournalEntryDocumentDetailUnit, long> journalEntryDocumentDetailUnitRepository)
        {
            _journalEntryDocumentUnitRepository = journalEntryDocumentUnitrepository;
            _unitOfWorkManager = unitOfWorkManager;
            _journalEntryDocumentDetailUnitRepository = journalEntryDocumentDetailUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(JournalEntryDocumentUnit journalUnit)
        {

            await ValidateJournalUnitAsync(journalUnit);
            return await _journalEntryDocumentUnitRepository.InsertAndGetIdAsync(journalUnit);
        }

        [UnitOfWork]
        public virtual async Task<long> CreateRecurringAsync(long journalId,int tenantId)
        {
            _unitOfWorkManager.Current.SetTenantId(tenantId);
            {
                JournalEntryDocumentUnit recurJournalDocument = new JournalEntryDocumentUnit();
                var journalDocumentunit = _journalEntryDocumentUnitRepository.Get(journalId);

                //Mapper.CreateMap<JournalEntryDocumentUnit, JournalEntryDocumentUnit>()
                //    .ForMember(dto => dto.Id, options => options.Ignore());
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<JournalEntryDocumentUnit, JournalEntryDocumentUnit>().ForMember(dto => dto.Id, options => options.Ignore());
                });
                Mapper.Map(journalDocumentunit, recurJournalDocument);

                // Adding the Parent Reference
                recurJournalDocument.OriginalDocumentId = journalId;
                recurJournalDocument.RecurDocId = journalId;
                // Changing the Description
                recurJournalDocument.DocumentReference = recurJournalDocument.DocumentReference + "-" +
                                                           _journalEntryDocumentUnitRepository.GetAll()
                                                               .Count(p => p.OriginalDocumentId == journalId) + 1;

                // await ValidateJournalUnitAsync(newjournalDocumentunit);
                var accountingDocumentId = await _journalEntryDocumentUnitRepository.InsertAndGetIdAsync(recurJournalDocument);

                //Getting journal details by AccountingDocumentId
                var newJournalDetails = await _journalEntryDocumentDetailUnitRepository.GetAllListAsync(u => u.AccountingDocumentId == journalId);
                foreach (var journals in newJournalDetails)
                {
                    journals.AccountingDocumentId = accountingDocumentId;
                    await _journalEntryDocumentDetailUnitRepository.InsertAsync(journals);
                }
                return accountingDocumentId;
            }
        }

        public virtual async Task UpdateAsync(JournalEntryDocumentUnit journalUnit)
        {
            await ValidateJournalUnitAsync(journalUnit);
            await _journalEntryDocumentUnitRepository.UpdateAsync(journalUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await _journalEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }
        protected virtual async Task ValidateJournalUnitAsync(JournalEntryDocumentUnit journalunit)
        {
            //Validating if Duplicate DocumentReference(Journal#) exists
            if (_journalEntryDocumentUnitRepository != null)
            {

                var journals = (await _journalEntryDocumentUnitRepository.
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
