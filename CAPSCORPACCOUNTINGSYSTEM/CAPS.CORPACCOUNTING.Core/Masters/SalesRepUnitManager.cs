using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class SalesRepUnitManager : DomainService
    {
        protected IRepository<SalesRepUnit> SalesRepUnitRepository { get;  }

        public SalesRepUnitManager(IRepository<SalesRepUnit> salesrepunitrepository)
        {
            SalesRepUnitRepository = salesrepunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(SalesRepUnit salesrepunit)
        {
            await ValidateSalesRepUnitAsync(salesrepunit);
            await SalesRepUnitRepository.InsertAsync(salesrepunit);
        }

        public virtual async Task UpdateAsync(SalesRepUnit salesrepunit)
        {
            await ValidateSalesRepUnitAsync(salesrepunit);
            await SalesRepUnitRepository.UpdateAsync(salesrepunit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await SalesRepUnitRepository.DeleteAsync(id);
        }
        protected virtual async Task ValidateSalesRepUnitAsync(SalesRepUnit salesRepUnit)
        {
            //Validating if Duplicate SalesRep exists
            if (SalesRepUnitRepository != null)
            {
                var salesRep = (await SalesRepUnitRepository.GetAllListAsync(p => p.LastName == salesRepUnit.LastName && p.OrganizationUnitId == salesRepUnit.OrganizationUnitId));

                if (salesRepUnit.Id == 0)
                {
                    if (salesRep.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", salesRepUnit.LastName));
                    }
                }
                else
                {
                    if (salesRep.FirstOrDefault(p => p.Id != salesRepUnit.Id && p.LastName == salesRepUnit.LastName) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", salesRepUnit.LastName));
                    }
                }
            }
        }
    }

}
