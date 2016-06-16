using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
namespace CAPS.CORPACCOUNTING.AccountReceivable
{
   public class ArInvoiceEntryDocumentDetailUnitManager : DomainService
    {
        protected IRepository<ArInvoiceEntryDocumentDetailUnit, long> ArInvoiceEntryDocumentDetailUnitRepository { get; }
        public ArInvoiceEntryDocumentDetailUnitManager(IRepository<ArInvoiceEntryDocumentDetailUnit, long> arInvoiceEntryDocumentDetailUnitRepository)
        {
            ArInvoiceEntryDocumentDetailUnitRepository = arInvoiceEntryDocumentDetailUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(ArInvoiceEntryDocumentDetailUnit input)
        {

            return await ArInvoiceEntryDocumentDetailUnitRepository.InsertAndGetIdAsync(input);
        }


        public virtual async Task UpdateAsync(ArInvoiceEntryDocumentDetailUnit input)
        {
            await ArInvoiceEntryDocumentDetailUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await ArInvoiceEntryDocumentDetailUnitRepository.DeleteAsync(input.Id);
        }
    }
}
