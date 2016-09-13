using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using Abp.UI;
using CAPS.CORPACCOUNTING.Accounting;

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
        public virtual async Task<long> CreateAsync(PayrollEntryDocumentUnit input)
        {
            await ValidatePayrollUnitAsync(input);
            return  await PayrollEntryDocumentUnitRepository.InsertOrUpdateAndGetIdAsync(input);
        }

        public virtual async Task UpdateAsync(PayrollEntryDocumentUnit input)
        {
            await ValidatePayrollUnitAsync(input);
            await PayrollEntryDocumentUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await PayrollEntryDocumentUnitRepository.DeleteAsync(input.Id);
        }

        protected virtual async Task ValidatePayrollUnitAsync(PayrollEntryDocumentUnit invoiceunit)
        {
            //Validating if Duplicate DocumentReference(PayrollInvoice#) exists
            if (PayrollEntryDocumentUnitRepository != null)
            {

                var invoices = (await PayrollEntryDocumentUnitRepository.GetAllListAsync(
                        p => p.DocumentReference == invoiceunit.DocumentReference && p.OrganizationUnitId == invoiceunit.OrganizationUnitId
                        && p.TypeOfAccountingDocumentId == TypeOfAccountingDocument.Payroll));

                if (invoiceunit.Id == 0)
                {
                    if (invoices.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate PayrollInvoice#", invoiceunit.DocumentReference));
                    }
                }
                else
                {
                    if (invoices.FirstOrDefault(p => p.Id != invoiceunit.Id && p.DocumentReference == invoiceunit.DocumentReference) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate PayrollInvoice#", invoiceunit.DocumentReference));
                    }
                }

            }
        }
    }
}
