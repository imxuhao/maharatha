using System.Data.Entity;
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

        public virtual async Task<int> CreateAsync(FiscalYearUnit input)
        {
            await Validate(input);
          return  await _fiscalYearUnitRepository.InsertAndGetIdAsync(input);
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
                var fiscalYear = await _fiscalYearUnitRepository.GetAll().Where(
                       u => (((u.YearStartDate <= input.YearStartDate && u.YearEndDate >= input.YearStartDate)||
                             (u.YearStartDate <= input.YearEndDate && u.YearEndDate >= input.YearEndDate) ||
                             (u.YearStartDate >= input.YearStartDate && u.YearEndDate <= input.YearEndDate))
                            && u.OrganizationUnitId == input.OrganizationUnitId)).ToListAsync();
                if (fiscalYear.Count > 0)
                {

                    if (input.Id == 0)
                    {

                        throw new UserFriendlyException(L("FiscalYear already exist"));

                    }
                    else
                    if (fiscalYear.FirstOrDefault(p => p.Id != input.Id  && (p.YearStartDate >= input.YearStartDate && p.YearStartDate <= input.YearEndDate || p.YearEndDate <= input.YearEndDate && p.YearStartDate >= input.YearEndDate)) != null)
                    {
                        throw new UserFriendlyException(L("FiscalYear already exist"));
                    }

                }

            }
        }
    }
}


