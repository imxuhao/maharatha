using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
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
            await ValidatePaymentRangeAsync(input);
            await BankAccountPaymentRangeUnitRepository.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(BankAccountPaymentRangeUnit input)
        {
            await ValidatePaymentRangeAsync(input);
            await BankAccountPaymentRangeUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await BankAccountPaymentRangeUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

        protected virtual async Task ValidatePaymentRangeAsync(BankAccountPaymentRangeUnit input)
        {
            if (BankAccountPaymentRangeUnitRepository != null)
            {
                var paymentRangeUnit = await BankAccountPaymentRangeUnitRepository.GetAll()
                    .Where(
                        u =>(((u.StartingPaymentNumber <= input.StartingPaymentNumber &&
                               u.EndingPaymentNumber >= input.StartingPaymentNumber)
                              ||
                              (u.StartingPaymentNumber <= input.EndingPaymentNumber &&
                               u.EndingPaymentNumber >= input.EndingPaymentNumber)
                              ||
                              (u.StartingPaymentNumber >= input.StartingPaymentNumber &&
                               u.EndingPaymentNumber <= input.EndingPaymentNumber))
                             && u.BankAccountId == input.BankAccountId )).ToListAsync();
                if (input.Id == 0)
                {
                    if (paymentRangeUnit.Count > 0)
                    {
                        throw new UserFriendlyException(L("StartingCheck# and EndingCheck# should not be overlap"));
                    }
                }
                else
                {
                    if (paymentRangeUnit.Select(p => p.Id != input.Id
                            && p.StartingPaymentNumber == input.StartingPaymentNumber
                            && p.EndingPaymentNumber == input.EndingPaymentNumber).Count() != 0)
                    {
                        throw new UserFriendlyException(L("StartingCheck# and EndingCheck# should not be overlap"));
                    }
                }
            }
        }
    }
}
