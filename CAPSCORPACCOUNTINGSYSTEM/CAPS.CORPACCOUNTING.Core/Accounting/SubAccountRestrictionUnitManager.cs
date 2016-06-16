using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using System.Threading.Tasks;
using Abp.UI;
using System.Linq;
using System.Linq.Expressions;
using Abp.Extensions;
using LinqKit;
using CAPS.CORPACCOUNTING.Security;

namespace CAPS.CORPACCOUNTING.Accounting
{
    public class SubAccountRestrictionUnitManager : DomainService
    {
      

        protected IRepository<SubAccountRestrictionUnit, long> SubAccountRestrictionUnit { get; }

        public SubAccountRestrictionUnitManager(IRepository<SubAccountRestrictionUnit, long> subAccountRestrictionUnit)
        {

            SubAccountRestrictionUnit = subAccountRestrictionUnit;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(SubAccountRestrictionUnit input)
        {
            
            await SubAccountRestrictionUnit.InsertAsync(input);
        }

        public virtual async Task UpdateAsync(SubAccountRestrictionUnit input)
        {
          
            await SubAccountRestrictionUnit.UpdateAsync(input);
        }

        public virtual async Task DeleteAsync(IdInput<long> input)
        {
            await SubAccountRestrictionUnit.DeleteAsync(p => p.Id == input.Id);
        }

        
    }
}
