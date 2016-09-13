using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Payables
{
    public class InvoiceEntryDocumentDetailUnitManager : DomainService
    {
        protected IRepository<InvoiceEntryDocumentDetailUnit,long> InvoiceEntryDocumentDetailUnitRepository { get; }
        public InvoiceEntryDocumentDetailUnitManager(IRepository<InvoiceEntryDocumentDetailUnit, long> invoiceentrydocumentdetailunit)
        {
            InvoiceEntryDocumentDetailUnitRepository = invoiceentrydocumentdetailunit;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(InvoiceEntryDocumentDetailUnit invoiceentrydocumentdetailunit)
        {
            await InvoiceEntryDocumentDetailUnitRepository.InsertAsync(invoiceentrydocumentdetailunit);
        }

        public virtual async Task UpdateAsync(InvoiceEntryDocumentDetailUnit invoiceentrydocumentdetailunit)
        {
            await InvoiceEntryDocumentDetailUnitRepository.UpdateAsync(invoiceentrydocumentdetailunit);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await InvoiceEntryDocumentDetailUnitRepository.DeleteAsync(input.Id);
        }
    }
}
