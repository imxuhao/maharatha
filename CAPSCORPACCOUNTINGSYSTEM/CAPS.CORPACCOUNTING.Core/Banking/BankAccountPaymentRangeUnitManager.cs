using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Banking
{
    public class BankAccountPaymentRangeUnitManager : DomainService
    {
        protected IRepository<BankAccountPaymentRangeUnit> BankAccountPaymentRangeUnitRepository { get; }

        public BankAccountPaymentRangeUnitManager(IRepository<BankAccountPaymentRangeUnit> bankAccountPaymentRangeUnitRepository)
        {
            BankAccountPaymentRangeUnitRepository = bankAccountPaymentRangeUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(BankAccountPaymentRangeUnit input)
        {
            await BankAccountPaymentRangeUnitRepository.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(BankAccountPaymentRangeUnit input)
        {
            await BankAccountPaymentRangeUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await BankAccountPaymentRangeUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }
    }
}
