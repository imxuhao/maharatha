using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;
using CAPS.CORPACCOUNTING.Masters.CustomRepository;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class AccountUnitManager : DomainService
    {
        private readonly ICustomAccountRepository _customAccountRepository;
        public AccountUnitManager(IRepository<AccountUnit, long> accountunitrepository, IRepository<CoaUnit> coaUnitRepository, ICustomAccountRepository customAccountRepository)
        {
            _customAccountRepository = customAccountRepository;
            AccountUnitRepository = accountunitrepository;
            CoaUnitRepository = coaUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        protected IRepository<AccountUnit, long> AccountUnitRepository { get; }
        protected IRepository<CoaUnit> CoaUnitRepository { get; }

        [UnitOfWork]
        public virtual async Task<long> CreateAsync(AccountUnit accountUnit)
        {
            accountUnit.Code = await GetNextChildCodeAsync(accountUnit.ParentId, accountUnit.ChartOfAccountId);
            await ValidateAccountUnitAsync(accountUnit);
            return await AccountUnitRepository.InsertAndGetIdAsync(accountUnit);
        }


        public virtual async Task CreateAccountListAsync(AccountUnit accountUnit)
        {
            accountUnit.Code = await GetNextChildCodeAsync(accountUnit.ParentId, accountUnit.ChartOfAccountId);
            await ValidateAccountUnitAsync(accountUnit);
            await AccountUnitRepository.InsertAsync(accountUnit);
        }

        public virtual async Task UpdateAsync(AccountUnit accountUnit)
        {
            await ValidateAccountUnitAsync(accountUnit);
            await AccountUnitRepository.UpdateAsync(accountUnit);
        }

        public virtual async Task<string> GetNextChildCodeAsync(long? parentId, int coaId)
        {
            var lastChild = await GetLastChildOrNullAsync(parentId, coaId);
            if (lastChild != null) return AccountUnit.CalculateNextCode(lastChild.Code);
            var parentCode = parentId != null ? GetCode(parentId.Value) : null;
            return AccountUnit.AppendCode(parentCode, AccountUnit.CreateCode(1));
        }

        public virtual async Task<AccountUnit> GetLastChildOrNullAsync(long? parentId, int coaId)
        {
            var children =
                await
                    AccountUnitRepository.GetAllListAsync(ou => ou.ParentId == parentId && ou.ChartOfAccountId == coaId);
            return children.OrderBy(c => c.Code).LastOrDefault();
        }

        public virtual string GetCode(long id)
        {
            return AccountUnitRepository.Get(id).Code;
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(long id)
        {
            var children = await FindChildrenAsync(id, true);

            foreach (var child in children)
            {
                await AccountUnitRepository.DeleteAsync(child);
            }

            await AccountUnitRepository.DeleteAsync(id);
        }

        [UnitOfWork]
        public virtual async Task MoveAsync(long id, long? parentId, int coaId)
        {
            var accountUnit = await AccountUnitRepository.GetAsync(id);
            if (accountUnit.ParentId == parentId)
            {
                return;
            }

            //Should find children before Code change
            var children = await FindChildrenAsync(id, true);

            //Store old code of OU
            var oldCode = accountUnit.Code;

            //Move OU
            accountUnit.Code = await GetNextChildCodeAsync(parentId, coaId);
            accountUnit.ParentId = parentId;

            await ValidateAccountUnitAsync(accountUnit);

            //Update Children Codes
            foreach (var child in children)
            {
                child.Code = AccountUnit.AppendCode(accountUnit.Code, AccountUnit.GetRelativeCode(child.Code, oldCode));
            }
        }

        public async Task<List<AccountUnit>> FindChildrenAsync(long? parentId, bool recursive = false)
        {
            if (recursive)
            {
                if (!parentId.HasValue)
                {
                    return await AccountUnitRepository.GetAllListAsync();
                }

                var code = GetCode(parentId.Value);
                return
                    await
                        AccountUnitRepository.GetAllListAsync(ou => ou.Code.StartsWith(code) && ou.Id != parentId.Value);
            }
            return await AccountUnitRepository.GetAllListAsync(ou => ou.ParentId == parentId);
        }

        public virtual async Task ValidateAccountUnitAsync(AccountUnit accountUnit)
        {
            var coaUnit = await CoaUnitRepository.FirstOrDefaultAsync(p => p.Id == accountUnit.ChartOfAccountId);
            //Validating Numeric AccountNumbers
            //In Coa IsNumberic is true, AccountNumber shold be Numeric
            Regex rgx = new Regex(@"^([0-9]+-)*[0-9]+$");//This expression will accpet only Numbers and "-" ex:11-11

            if (coaUnit.IsNumeric && !rgx.IsMatch(accountUnit.AccountNumber))
            {
                throw new UserFriendlyException(L("Account Number should be numeric.", accountUnit.Caption));
            }

            // Validating duplicate account number and description of account with the same COAId and decription
            var siblings = (await FindChildrenAsync(accountUnit.ParentId))
                .Where(ou => ou.Id != accountUnit.Id && ou.ChartOfAccountId == accountUnit.ChartOfAccountId)
                .ToList();

            if (siblings.Any(ou => ou.AccountNumber == accountUnit.AccountNumber))
            {
                throw new UserFriendlyException(L("Duplicate Account Number", accountUnit.Caption));
            }

            #region Validating if Parent and Child have the same COAID

            if (accountUnit.ParentId != null)
            {
                var parentaccountUnit =
                    await AccountUnitRepository.GetAsync(Convert.ToInt64(accountUnit.ParentId));
                if (parentaccountUnit.ChartOfAccountId != accountUnit.ChartOfAccountId)
                {
                    throw new UserFriendlyException(L("Invalid Chart of Account", accountUnit.Caption));
                }
            }
            #endregion
        }



    }
}