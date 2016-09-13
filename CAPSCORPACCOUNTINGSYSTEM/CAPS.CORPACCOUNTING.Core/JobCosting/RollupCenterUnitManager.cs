using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    public class RollupCenterUnitManager : DomainService
    {
        protected IRepository<RollupCenterUnit> RollupCenterRepository { get;  }

        public RollupCenterUnitManager(IRepository<RollupCenterUnit> rollupcenterRepository)
        {
            RollupCenterRepository = rollupcenterRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting RollupCenterUnit Entity 
        /// </summary>
        /// <param name="rollupCenterUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(RollupCenterUnit rollupCenterUnit)
        {
            await ValidateRollupCenterUnitAsync(rollupCenterUnit);
            await RollupCenterRepository.InsertAsync(rollupCenterUnit);
        }

        /// <summary>
        /// Updating RollupCenterUnit Details
        /// </summary>
        /// <param name="rollupCenterUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(RollupCenterUnit rollupCenterUnit)
        {
            await ValidateRollupCenterUnitAsync(rollupCenterUnit);
            await RollupCenterRepository.UpdateAsync(rollupCenterUnit);
        }

        /// <summary>
        /// Deleting RollupCenterUnit Entity 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await RollupCenterRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Validating the RollupCenterUnit
        /// </summary>
        /// <param name="rollupCenterUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateRollupCenterUnitAsync(RollupCenterUnit rollupCenterUnit)
        {
            //Validating if Duplicate Caption exists
            if (RollupCenterRepository != null)
            {
                var rollupCenter = (await RollupCenterRepository.GetAllListAsync(p => p.Caption == rollupCenterUnit.Caption && p.OrganizationUnitId == rollupCenterUnit.OrganizationUnitId));

                if (rollupCenterUnit.Id == 0)
                {
                    if (rollupCenter.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Caption", rollupCenterUnit.Caption));
                    }
                }
                else
                {
                    if (rollupCenter.FirstOrDefault(p => p.Id != rollupCenterUnit.Id && p.Caption == rollupCenterUnit.Caption) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Caption", rollupCenterUnit.Caption));
                    }
                }
            }
        }
    }

}
