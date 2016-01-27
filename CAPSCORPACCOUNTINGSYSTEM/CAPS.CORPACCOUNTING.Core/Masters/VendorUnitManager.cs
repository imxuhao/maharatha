using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class VendorUnitManager : DomainService
    {
        protected IRepository<VendorUnit> VendorUnitRepository { get;  }

        public VendorUnitManager(IRepository<VendorUnit> vendorunitrepository)
        {
            VendorUnitRepository = vendorunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(VendorUnit vendorPaymentTermUnit)
        {
            await ValidateVendorPaymentTermUnitAsync(vendorPaymentTermUnit);
            await VendorUnitRepository.InsertAsync(vendorPaymentTermUnit);
        }

        public virtual async Task UpdateAsync(VendorUnit vendorPaymentTermUnit)
        {
            await ValidateVendorPaymentTermUnitAsync(vendorPaymentTermUnit);
            await VendorUnitRepository.UpdateAsync(vendorPaymentTermUnit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await VendorUnitRepository.DeleteAsync(id);
        }
        protected virtual async Task ValidateVendorPaymentTermUnitAsync(VendorUnit vendorUnit)
        {
            //Validating if Duplicate VendorName exists
            if (VendorUnitRepository != null)
            {
                var vendorsUnit = (await VendorUnitRepository.GetAllListAsync(p => p.LastName == vendorUnit.LastName && p.OrganizationUnitId == vendorUnit.OrganizationUnitId));

                if (vendorUnit.Id == 0)
                {
                    if (vendorsUnit.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", vendorUnit.LastName));
                    }
                }
                else
                {
                    if (vendorsUnit.FirstOrDefault(p => p.Id != vendorUnit.Id && p.LastName == vendorUnit.LastName) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", vendorUnit.LastName));
                    }
                }
            }
        }
    }

}
