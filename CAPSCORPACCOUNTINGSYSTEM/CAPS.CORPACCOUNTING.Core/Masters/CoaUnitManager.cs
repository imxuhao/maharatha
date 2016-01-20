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

        public CoaUnitManager(IRepository<CoaUnit> coaunitrepository)
        {
            CoaUnitRepository = coaunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(CoaUnit coaUnit)
        {
            await ValidateCoaUnitAsync(coaUnit);
            await CoaUnitRepository.InsertAsync(coaUnit);
        }

        public virtual async Task UpdateAsync(CoaUnit coaUnit)
        {
            await ValidateCoaUnitAsync(coaUnit);
            await CoaUnitRepository.UpdateAsync(coaUnit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CoaUnitRepository.DeleteAsync(id);
        }

        protected virtual async Task ValidateCoaUnitAsync(CoaUnit coaUnit)
        {
            //Validating if Duplicate COA exists
            if (CoaUnitRepository != null)
            {
                var coaunit = (await CoaUnitRepository.GetAllListAsync(p => p.Caption == coaUnit.Caption && p.OrganizationUnitId== coaUnit.OrganizationUnitId));

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