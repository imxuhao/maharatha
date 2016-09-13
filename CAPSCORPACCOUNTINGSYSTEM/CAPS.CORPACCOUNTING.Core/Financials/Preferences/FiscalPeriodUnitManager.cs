using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.UI;

namespace CAPS.CORPACCOUNTING.Financials.Preferences
{
    public class FiscalPeriodUnitManager : DomainService
    {
        protected IRepository<FiscalPeriodUnit> _fiscalPeriodUnitRepository { get; }

        public FiscalPeriodUnitManager(IRepository<FiscalPeriodUnit> fiscalPeriodUnitRepository)
        {
            _fiscalPeriodUnitRepository = fiscalPeriodUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(FiscalPeriodUnit input)
        {
            await ValidatePaymentRangeAsync(input);
            await _fiscalPeriodUnitRepository.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(FiscalPeriodUnit input)
        {
            await ValidatePaymentRangeAsync(input);
            await _fiscalPeriodUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await _fiscalPeriodUnitRepository.DeleteAsync(input.Id);
        }

        protected virtual async Task ValidatePaymentRangeAsync(FiscalPeriodUnit input)
        {
            if (input.PeriodStartDate > input.PeriodEndDate)
                throw new UserFriendlyException(L("FiscalPeriodStartDate should not be greaterthan FiscalPeriodEndDate"));

            if (_fiscalPeriodUnitRepository != null)
            {
               var payrange= await _fiscalPeriodUnitRepository.GetAll().Where(
                        u => (((u.PeriodStartDate <= input.PeriodStartDate &&
                               u.PeriodEndDate >= input.PeriodStartDate)
                              ||
                              (u.PeriodStartDate <= input.PeriodEndDate &&
                               u.PeriodEndDate >= input.PeriodEndDate)
                              ||
                              (u.PeriodStartDate >= input.PeriodStartDate &&
                               u.PeriodEndDate <= input.PeriodEndDate))
                             && u.FiscalYearId == input.FiscalYearId)).ToListAsync();
                if (input.Id == 0)
                {
                    if (payrange.Count > 0)
                    {
                        throw new UserFriendlyException(L("FiscalPeriod should not be overlap"));
                    }
                }
                else
                {
                    if (payrange.Select(p => p.Id != input.Id
                            && p.PeriodStartDate == input.PeriodStartDate
                            && p.PeriodEndDate == input.PeriodEndDate).Count() != 0)
                    {
                        throw new UserFriendlyException(L("FiscalPeriod should not be overlap"));
                    }
                }
                
               
            }
        }
    }
}
