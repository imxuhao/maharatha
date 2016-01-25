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

        public AddressUnitManager(IRepository<AddressUnit,long> addressUnitRepository)
        {
            AddressUnitRepository = addressUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(AddressUnit addressunit)
        {
            await AddressUnitRepository.InsertAsync(addressunit);
        }

        public virtual async Task UpdateAsync(AddressUnit addressunit)
        {
            await AddressUnitRepository.UpdateAsync(addressunit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await AddressUnitRepository.DeleteAsync(id);
        }
    }

}
