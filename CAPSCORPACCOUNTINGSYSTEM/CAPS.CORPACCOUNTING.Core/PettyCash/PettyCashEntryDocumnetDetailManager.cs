using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    public  class PettyCashEntryDocumnetDetailManager : DomainService
    {
        protected IRepository<PettyCashEntryDocumentDetailUnit, long> _pcEntryDocumnetDetailRepository { get; }
        public PettyCashEntryDocumnetDetailManager(IRepository<PettyCashEntryDocumentDetailUnit, long> pcentrydocumnetdetailrepository)
        {
            _pcEntryDocumnetDetailRepository = pcentrydocumnetdetailrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }


        public virtual async Task CreateAsync(PettyCashEntryDocumentDetailUnit input)
        {
            await _pcEntryDocumnetDetailRepository.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(PettyCashEntryDocumentDetailUnit input)
        {
            await _pcEntryDocumnetDetailRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await _pcEntryDocumnetDetailRepository.DeleteAsync(input.Id);
        }
    }
}
