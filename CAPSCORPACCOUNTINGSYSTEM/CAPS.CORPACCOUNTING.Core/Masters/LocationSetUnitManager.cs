using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class LocationSetUnitManager : DomainService
    {
        protected IRepository<LocationSetUnit> _locationSetUnitRepository { get; }

        /// <summary>
        ///  LocationSetUnitManager Constructor to initializing the LocationSet Repository
        /// </summary>
        /// <param name="locationSetUnitRepository"></param>
        public LocationSetUnitManager(IRepository<LocationSetUnit> locationSetUnitRepository)
        {
            _locationSetUnitRepository = locationSetUnitRepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }
        /// <summary>
        ///  creating LocationSet 
        /// </summary>
        /// <param name="locationSetUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(LocationSetUnit locationSetUnit)
        {          
            await _locationSetUnitRepository.InsertAsync(locationSetUnit);
        }

        /// <summary>
        ///  Updating LocationSet Details
        /// </summary>
        /// <param name="locationSetUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(LocationSetUnit locationSetUnit)
        {
            
            await _locationSetUnitRepository.UpdateAsync(locationSetUnit);
        }

        /// <summary>       
        /// Deleting LocationSet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await _locationSetUnitRepository.DeleteAsync(id);
        }
    }
}
