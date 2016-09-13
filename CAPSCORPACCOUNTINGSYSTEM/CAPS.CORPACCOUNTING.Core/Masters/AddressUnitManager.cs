using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class AddressUnitManager : DomainService
    {
        protected IRepository<AddressUnit,long> AddressUnitRepository { get;  }

        /// <summary>
        /// AddressUnitManager Constructor to initializing the AddressUnit Repository
        /// </summary>
        /// <param name="addressUnitRepository"></param>
        public AddressUnitManager(IRepository<AddressUnit,long> addressUnitRepository)
        {
            AddressUnitRepository = addressUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting Address Details
        /// </summary>
        /// <param name="addressunit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(AddressUnit addressunit)
        {
            await AddressUnitRepository.InsertAsync(addressunit);
        }

        /// <summary>
        /// Updating Address Details
        /// </summary>
        /// <param name="addressunit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(AddressUnit addressunit)
        {
            await AddressUnitRepository.UpdateAsync(addressunit);
        }

        /// <summary>
        /// Deleting the Address
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await AddressUnitRepository.DeleteAsync(id);
        }
    }

}
