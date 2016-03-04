using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Application.Services.Dto;

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

        public virtual async Task UpdateAsync(ApHeaderTransactions accountUnit)
        {
            await ApHeaderTransactionsUnitRepository.UpdateAsync(accountUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await ApHeaderTransactionsUnitRepository.DeleteAsync(input.Id);
        }
    }
}
