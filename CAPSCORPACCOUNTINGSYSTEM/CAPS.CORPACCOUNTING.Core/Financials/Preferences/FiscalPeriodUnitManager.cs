using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
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
            await _fiscalPeriodUnitRepository.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(FiscalPeriodUnit input)
        {
            await _fiscalPeriodUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await _fiscalPeriodUnitRepository.DeleteAsync(input.Id);
        }
    }
}
