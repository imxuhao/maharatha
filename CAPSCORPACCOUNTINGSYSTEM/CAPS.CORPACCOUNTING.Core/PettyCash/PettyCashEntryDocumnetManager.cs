using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using Abp.Domain.Repositories;
using Abp.Zero;
using CAPS.CORPACCOUNTING.Accounting;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    public class PettyCashEntryDocumnetManager : DomainService
    {

        protected IRepository<PettyCashEntryDocumentUnit, long> _pettyCashEntryDocumentUnitRepository { get; }
        public PettyCashEntryDocumnetManager(IRepository<PettyCashEntryDocumentUnit, long> pettycashentrydocumentunitrepository)
        {
            _pettyCashEntryDocumentUnitRepository = pettycashentrydocumentunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }
       
        public virtual async Task<long> CreateAsync(PettyCashEntryDocumentUnit input)
        {
            await ValidateJournalUnitAsync(input);
            return await _pettyCashEntryDocumentUnitRepository.InsertAndGetIdAsync(input);
        }

        public virtual async Task UpdateAsync(PettyCashEntryDocumentUnit input)
        {
            await ValidateJournalUnitAsync(input);
            await _pettyCashEntryDocumentUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await _pettyCashEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }

        protected virtual async Task ValidateJournalUnitAsync(PettyCashEntryDocumentUnit pettyCashUnit)
        {
            //Validating if Duplicate DocumentReference(INVOICE#) exists
            if (_pettyCashEntryDocumentUnitRepository != null)
            {

                var pcUnits = (await _pettyCashEntryDocumentUnitRepository.GetAllListAsync(
                        p => p.DocumentReference == pettyCashUnit.DocumentReference && p.OrganizationUnitId == pettyCashUnit.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.PettyCash));

                if (pettyCashUnit.Id == 0)
                {
                    if (pcUnits.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate PettyCash#", pettyCashUnit.DocumentReference));
                    }
                }
                else
                {
                    if (pcUnits.FirstOrDefault(p => p.Id != pettyCashUnit.Id && p.DocumentReference == pettyCashUnit.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate PettyCash#", pettyCashUnit.DocumentReference));
                    }
                }

            }
        }






       
    }
}
