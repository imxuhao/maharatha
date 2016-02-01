using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class CoaUnitManager : DomainService
    {
        protected IRepository<CoaUnit> CoaUnitRepository { get; private set; }

        /// <summary>
        /// CoaUnitManager Constructor to initializing the CoaUnit Repository
        /// </summary>
        /// <param name="coaunitrepository"></param>
        public CoaUnitManager(IRepository<CoaUnit> coaunitrepository)
        {
            CoaUnitRepository = coaunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        /// <summary>
        /// Inserting Coaunit Details
        /// </summary>
        /// <param name="coaUnit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(CoaUnit coaUnit)
        {
            await ValidateCoaUnitAsync(coaUnit);
            await CoaUnitRepository.InsertAsync(coaUnit);
        }

        /// <summary>
        /// Updating CoaUnit Details
        /// </summary>
        /// <param name="coaUnit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(CoaUnit coaUnit)
        {
            await ValidateCoaUnitAsync(coaUnit);
            await CoaUnitRepository.UpdateAsync(coaUnit);
        }

        /// <summary>
        /// Deleting CoaUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CoaUnitRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Validating CoaUnit 
        /// </summary>
        /// <param name="coaUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateCoaUnitAsync(CoaUnit coaUnit)
        {
            //Validating if Duplicate COA exists
            if (CoaUnitRepository != null)
            {
                var coaunit = (await CoaUnitRepository.GetAllListAsync(p => p.Caption == coaUnit.Caption && p.OrganizationUnitId== coaUnit.OrganizationUnitId && p.TenantId ==coaUnit.TenantId ));

                if (coaUnit.Id == 0)
                {
                    if (coaunit.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Chart of Account", coaUnit.Caption));
                    }
                }
                else
                {
                    if (coaunit.FirstOrDefault(p => p.Id != coaUnit.Id && p.Caption == coaUnit.Caption) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Chart of Account", coaUnit.Caption));
                    }
                }
            }
        }
    }
}