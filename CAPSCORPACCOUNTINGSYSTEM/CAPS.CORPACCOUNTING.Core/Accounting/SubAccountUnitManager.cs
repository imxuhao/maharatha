using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using System.Threading.Tasks;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Accounting
{
  public class SubAccountUnitManager : DomainService
    {
        protected IRepository<SubAccountUnit, long> SubAccountUnitRepository { get; }

        public SubAccountUnitManager(IRepository<SubAccountUnit, long> subAccountUnitRepository)
        {
            SubAccountUnitRepository = subAccountUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(SubAccountUnit subAccountUnit)
        {
            await ValidateSubAccountUnitAsync(subAccountUnit);
            await SubAccountUnitRepository.InsertAsync(subAccountUnit);
        }

        public virtual async Task UpdateAsync(SubAccountUnit subAccountUnit)
        {
            await ValidateSubAccountUnitAsync(subAccountUnit);
            await SubAccountUnitRepository.UpdateAsync(subAccountUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await SubAccountUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

        /// <summary>
        /// Validating Sub Account 
        /// </summary>
        /// <param name="subAccountUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateSubAccountUnitAsync(SubAccountUnit subAccountUnit)
        {
            //Validating if Duplicate Sub Account exists
            if (SubAccountUnitRepository != null)
            {
                var subaccountunit = (await SubAccountUnitRepository.GetAllListAsync(p => p.SubAccountNumber == subAccountUnit.SubAccountNumber && p.OrganizationUnitId == subAccountUnit.OrganizationUnitId && p.TenantId == subAccountUnit.TenantId));

                if (subAccountUnit.Id == 0)
                {
                    if (subaccountunit.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Sub Account", subAccountUnit.Caption));
                    }
                }
                else
                {
                    if (subaccountunit.FirstOrDefault(p => p.Id != subAccountUnit.Id && p.SubAccountNumber == subAccountUnit.SubAccountNumber) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Sub Account", subAccountUnit.Caption));
                    }
                }
            }
        }
    }
}
