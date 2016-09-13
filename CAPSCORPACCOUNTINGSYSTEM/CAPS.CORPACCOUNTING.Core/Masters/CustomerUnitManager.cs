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


        /// <summary>
        /// Inserting Customer Details in CustomerTable
        /// </summary>
        /// <param name="customerunit"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(CustomerUnit customerunit)
        {
            await ValidateCustomerUnitAsync(customerunit);
            await CustomerUnitRepository.InsertAsync(customerunit);
        }

        /// <summary>
        ///  Updating Customer Details in CustomerTable
        /// </summary>
        /// <param name="customerunit"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(CustomerUnit customerunit)
        {
            await ValidateCustomerUnitAsync(customerunit);
            await CustomerUnitRepository.UpdateAsync(customerunit);
        }

        /// <summary>
        /// Deleting the Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CustomerUnitRepository.DeleteAsync(id);
        }
        /// <summary>
        /// Validating the Customer
        /// </summary>
        /// <param name="customerunit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateCustomerUnitAsync(CustomerUnit customerunit)
        {
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
