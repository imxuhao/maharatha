using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
namespace CAPS.CORPACCOUNTING.Payroll
{
    public class PayrollEntryDocumentDetailUnitManager : DomainService
    {
        protected IRepository<PayrollEntryDocumentDetailUnit,long> PayrollEntryDocumentUnitDetailRepository { get; }
        public PayrollEntryDocumentDetailUnitManager(IRepository<PayrollEntryDocumentDetailUnit, long> payrollEntryDocumentUnitDetailRepository)
        {
            PayrollEntryDocumentUnitDetailRepository = payrollEntryDocumentUnitDetailRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(PayrollEntryDocumentDetailUnit accountUnit)
        {
          return  await PayrollEntryDocumentUnitDetailRepository.InsertOrUpdateAndGetIdAsync(accountUnit);
        }

        public virtual async Task UpdateAsync(PayrollEntryDocumentDetailUnit accountUnit)
        {
            await PayrollEntryDocumentUnitDetailRepository.UpdateAsync(accountUnit);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await PayrollEntryDocumentUnitDetailRepository.DeleteAsync(input.Id);
        }
    }
}
