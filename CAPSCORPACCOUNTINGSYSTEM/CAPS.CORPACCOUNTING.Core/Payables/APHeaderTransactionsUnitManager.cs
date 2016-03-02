using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Payables
{
    public class APHeaderTransactionsUnitManager : DomainService
    {
        protected IRepository<ApHeaderTransactions> ApHeaderTransactionsUnitRepository { get; }
        public APHeaderTransactionsUnitManager(IRepository<ApHeaderTransactions> apheadertransactionsunitrepository)
        {
            ApHeaderTransactionsUnitRepository = apheadertransactionsunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(ApHeaderTransactions accountUnit)
        {           
            await ApHeaderTransactionsUnitRepository.InsertAsync(accountUnit);
        }

    }
}
