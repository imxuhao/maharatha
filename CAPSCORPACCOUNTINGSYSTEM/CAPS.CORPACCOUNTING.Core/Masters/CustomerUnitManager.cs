using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class CustomerUnitManager : DomainService
    {
        protected IRepository<CustomerUnit> CustomerUnitRepository { get;  }

        public CustomerUnitManager(IRepository<CustomerUnit> customerunitrepository)
        {
            CustomerUnitRepository = customerunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(CustomerUnit customerunit)
        {
            await ValidateCustomerUnitAsync(customerunit);
            await CustomerUnitRepository.InsertAsync(customerunit);
        }

        public virtual async Task UpdateAsync(CustomerUnit customerunit)
        {
            await ValidateCustomerUnitAsync(customerunit);
            await CustomerUnitRepository.UpdateAsync(customerunit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CustomerUnitRepository.DeleteAsync(id);
        }
        protected virtual async Task ValidateCustomerUnitAsync(CustomerUnit customerunit)
        {
            //Validating if Duplicate Customer exists
            if (CustomerUnitRepository != null)
            {
                var customer = (await CustomerUnitRepository.GetAllListAsync(p => p.LastName == customerunit.LastName && p.OrganizationUnitId == customerunit.OrganizationUnitId));

                if (customerunit.Id == 0)
                {
                    if (customer.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", customerunit.LastName));
                    }
                }
                else
                {
                    if (customer.FirstOrDefault(p => p.Id != customerunit.Id && p.LastName == customerunit.LastName) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", customerunit.LastName));
                    }
                }
            }
        }
    }

}
