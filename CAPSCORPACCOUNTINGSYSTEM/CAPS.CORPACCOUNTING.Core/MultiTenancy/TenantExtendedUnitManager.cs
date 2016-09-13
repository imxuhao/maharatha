using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    public class TenantExtendedUnitManager : DomainService
    {
        protected IRepository<TenantExtendedUnit> _tenantExtendedUnitRepository { get; private set; }

        /// <summary>
        /// CoaUnitManager Constructor to initializing the CoaUnit Repository
        /// </summary>
        /// <param name="tenantExtendedUnitRepository"></param>
        public TenantExtendedUnitManager(IRepository<TenantExtendedUnit> tenantExtendedUnitRepository)
        {
            _tenantExtendedUnitRepository = tenantExtendedUnitRepository;


            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting TenantExtendedUnit Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<int> CreateAsync(TenantExtendedUnit input)
        {
            try
            {
                return await _tenantExtendedUnitRepository.InsertAndGetIdAsync(input);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updating TenantExtendedUnit Details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TenantExtendedUnit input)
        {
            await _tenantExtendedUnitRepository.UpdateAsync(input);
        }

        /// <summary>
        /// Deleting TenantExtended
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await _tenantExtendedUnitRepository.DeleteAsync(id);
        }

    }
}