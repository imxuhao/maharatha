using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class CustomerPaymentTermUnitManager : DomainService
    {
        protected IRepository<CustomerPaymentTermUnit> CustomerPaymentTermUnitRepository { get;  }

        public CustomerPaymentTermUnitManager(IRepository<CustomerPaymentTermUnit> customerPaymentTermunitrepository)
        {
            CustomerPaymentTermUnitRepository = customerPaymentTermunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(CustomerPaymentTermUnit customerPaymentTermUnit)
        {
            await ValidateCustomerPaymentTermUnitAsync(customerPaymentTermUnit);
            await CustomerPaymentTermUnitRepository.InsertAsync(customerPaymentTermUnit);
        }

        public virtual async Task UpdateAsync(CustomerPaymentTermUnit customerPaymentTermUnit)
        {
            await ValidateCustomerPaymentTermUnitAsync(customerPaymentTermUnit);
            await CustomerPaymentTermUnitRepository.UpdateAsync(customerPaymentTermUnit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CustomerPaymentTermUnitRepository.DeleteAsync(id);
        }
        protected virtual async Task ValidateCustomerPaymentTermUnitAsync(CustomerPaymentTermUnit customePaymentUnit)
        {
            //Validating if Duplicate CustomerPaymentTem exists
            if (CustomerPaymentTermUnitRepository != null)
            {
                var customerPaymentTermunit = (await CustomerPaymentTermUnitRepository.GetAllListAsync(p => p.Description == customePaymentUnit.Description && p.OrganizationUnitId == customePaymentUnit.OrganizationUnitId));

                if (customePaymentUnit.Id == 0)
                {
                    if (customerPaymentTermunit.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate CustomerPaymentTem", customePaymentUnit.Description));
                    }
                }
                else
                {
                    if (customerPaymentTermunit.FirstOrDefault(p => p.Id != customePaymentUnit.Id && p.Description == customePaymentUnit.Description) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate CustomerPaymentTem", customePaymentUnit.Description));
                    }
                }
            }
        }
    }

}
