using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using Abp.Application.Services.Dto;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Financials.Preferences
{
    public class FiscalYearUnitManager : DomainService
    {
        protected IRepository<FiscalYearUnit> _fiscalYearUnitRepository { get; }

        public FiscalYearUnitManager(IRepository<FiscalYearUnit> fiscalYearUnitRepository)
        {
            _fiscalYearUnitRepository = fiscalYearUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(FiscalYearUnit input)
        {
            await Validate(input);
            await _fiscalYearUnitRepository.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(FiscalYearUnit input)
        {
           await Validate(input);
            await _fiscalYearUnitRepository.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await _fiscalYearUnitRepository.DeleteAsync(input.Id);
        }

        private async Task Validate(FiscalYearUnit input)
        {
            if (_fiscalYearUnitRepository != null)
            {
                var fiscalYear = (await _fiscalYearUnitRepository.GetAllListAsync(p => p.OrganizationUnitId ==
                                                                         input.OrganizationUnitId && p.YearStartDate.Year == input.YearStartDate.Year));
                if (fiscalYear.Count > 0)
                {

                    if (input.Id == 0)
                    {

                        throw new UserFriendlyException(L("FiscalYear already exist"));

                    }
                    else
                    if (fiscalYear.FirstOrDefault(p => p.Id != input.Id && p.YearStartDate.Year == input.YearStartDate.Year) != null)
                    {
                        throw new UserFriendlyException(L("FiscalYear already exist"));
                    }

                }

            }
        }
    }
}
