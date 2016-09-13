using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class AccountLinksManager : DomainService
    {
        protected IRepository<AccountLinks,long> _accountLinkRepository { get; private set; }

        /// <summary>
        /// CoaUnitManager Constructor to initializing the CoaUnit Repository
        /// </summary>
        /// <param name="accountLinkRepository"></param>
        public AccountLinksManager(IRepository<AccountLinks, long> accountLinkRepository)
        {
            _accountLinkRepository = accountLinkRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting AccountLinks Details
        /// </summary>
        /// <param name="accountLinkUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(AccountLinks accountLinkUnit)
        {
            await _accountLinkRepository.InsertAsync(accountLinkUnit);
        }

        /// <summary>
        /// Updating AccountLinks Details
        /// </summary>
        /// <param name="accountLinkUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(AccountLinks accountLinkUnit)
        {
            await _accountLinkRepository.UpdateAsync(accountLinkUnit);
        }

        /// <summary>
        /// Deleting CoaUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await _accountLinkRepository.DeleteAsync(id);
        }
        
    }
}