using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
namespace CAPS.CORPACCOUNTING.Payroll
{
    public class PayrollEntryDocumentUnitManager : DomainService
    {
        protected IRepository<PayrollEntryDocumentUnit,long> PayrollEntryDocumentUnitRepository { get; }
        public PayrollEntryDocumentUnitManager(IRepository<PayrollEntryDocumentUnit,long> payrollEntryDocumentUnitrepository)
        {
            PayrollEntryDocumentUnitRepository = payrollEntryDocumentUnitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(PayrollEntryDocumentUnit accountUnit)
        {
            await PayrollEntryDocumentUnitRepository.InsertAsync(accountUnit);
        }

        public virtual async Task UpdateAsync(PayrollEntryDocumentUnit accountUnit)
        {
            await PayrollEntryDocumentUnitRepository.UpdateAsync(accountUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await PayrollEntryDocumentUnitRepository.DeleteAsync(p => p.BatchId == input.Id);
        }
    }
}
