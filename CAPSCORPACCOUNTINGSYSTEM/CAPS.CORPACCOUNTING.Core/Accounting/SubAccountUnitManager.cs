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
    public class SubAccountUnitManager : DomainService
    {
        protected IRepository<SubAccountUnit, long> SubAccountUnitRepository { get; }

        protected IRepository<SubEntityAccessRestrictionUnit, long> SubEntityAccessRestrictionUnit { get; }

        public SubAccountUnitManager(IRepository<SubAccountUnit, long> subAccountUnitRepository, IRepository<SubEntityAccessRestrictionUnit, long> subentityAccessRepository)
        {
            SubAccountUnitRepository = subAccountUnitRepository;
            SubEntityAccessRestrictionUnit = subentityAccessRepository;
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

        public Expression<Func<SubAccountUnit, bool>> AccessPredicateByAccount(int accountId)
        {
            // return prod => !prod.Discontinued && prod.Purchases.Where(purch => purch.Date > DateTime.Now.AddDays(-30)).Count() >= minPurchases;
            var predicate = PredicateBuilder.False<SubAccountUnit>();

            if (SubEntityAccessRestrictionUnit.GetAll().Any(p => p.AccountId == accountId && p.UserId == null && p.SubAccountId != null))
            {
                var subAccountListInts = SubEntityAccessRestrictionUnit.GetAll().Where(p => p.AccountId == accountId && p.UserId == null && p.SubAccountId != null).ToList();
                // int[] subAccountListInts = new int[] { 1, 2, 3, 4 };

                predicate = subAccountListInts.Select(p => p.SubAccountId).Select(l =>
                {
                    if (l != null) return (long) l;
                    return 0;
                }).Aggregate(predicate, (current, subAccount) => current.Or(p => p.Id == subAccount));

                return predicate;
            }
            return p => p.IsProjectSubAccount;
        }
    }
}
