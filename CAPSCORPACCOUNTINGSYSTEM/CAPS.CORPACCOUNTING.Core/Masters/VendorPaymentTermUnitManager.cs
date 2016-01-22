using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.UI;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class VendorPaymentTermUnitManager : DomainService
    {
        protected IRepository<VendorPaymentTermUnit> VendorPaymentTermUnitRepository { get;  }

        public VendorPaymentTermUnitManager(IRepository<VendorPaymentTermUnit> vendorpaymenttermunitrepository)
        {
            VendorPaymentTermUnitRepository = vendorpaymenttermunitrepository;

            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public virtual async Task CreateAsync(VendorPaymentTermUnit vendorPaymentTermUnit)
        {
            await ValidateVendorPaymentTermUnitAsync(vendorPaymentTermUnit);
            await VendorPaymentTermUnitRepository.InsertAsync(vendorPaymentTermUnit);
        }

        public virtual async Task UpdateAsync(VendorPaymentTermUnit vendorPaymentTermUnit)
        {
            await ValidateVendorPaymentTermUnitAsync(vendorPaymentTermUnit);
            await VendorPaymentTermUnitRepository.UpdateAsync(vendorPaymentTermUnit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await VendorPaymentTermUnitRepository.DeleteAsync(id);
        }
        protected virtual async Task ValidateVendorPaymentTermUnitAsync(VendorPaymentTermUnit vendorpaymentUnit)
        {
            //Validating if Duplicate VendorPaymentTem exists
            if (VendorPaymentTermUnitRepository != null)
            {
                var vendorPaymentTermunit = (await VendorPaymentTermUnitRepository.GetAllListAsync(p => p.Description == vendorpaymentUnit.Description && p.OrganizationUnitId == vendorpaymentUnit.OrganizationUnitId));

                if (vendorpaymentUnit.Id == 0)
                {
                    if (vendorPaymentTermunit.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate VendorPaymentTem", vendorpaymentUnit.Description));
                    }
                }
                else
                {
                    if (vendorPaymentTermunit.FirstOrDefault(p => p.Id != vendorpaymentUnit.Id && p.Description == vendorpaymentUnit.Description) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate VendorPaymentTem", vendorpaymentUnit.Description));
                    }
                }
            }
        }
    }

}
