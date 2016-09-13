using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;

namespace CAPS.CORPACCOUNTING.Masters
{

    public class VendorAliasUnitManager : DomainService
    {
        protected IRepository<VendorAliasUnit, int> VendorAliasUnitRepository { get; }
        public VendorAliasUnitManager(IRepository<VendorAliasUnit, int> vendoraliasunitrepository)
        {
            VendorAliasUnitRepository = vendoraliasunitrepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }



        /// <summary>
        /// Inserting VendorAliasName Entity 
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(VendorAliasUnit vendorAlias)
        {
            await ValidateVendorAliasUnitAsync(vendorAlias);
            await VendorAliasUnitRepository.InsertAsync(vendorAlias);
        }

        /// <summary>
        /// Updating VendorAliasName Details
        /// </summary>
        /// <param name="vendorAlias"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(VendorAliasUnit vendorAlias)
        {
            await ValidateVendorAliasUnitAsync(vendorAlias);
            await VendorAliasUnitRepository.UpdateAsync(vendorAlias);
        }

        /// <summary>
        /// Deleting VendorAliasName Entity 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await VendorAliasUnitRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Validating the VendorAliasName
        /// </summary>
        /// <param name="vendorUnit"></param>
        /// <returns></returns>
        protected virtual async Task ValidateVendorAliasUnitAsync(VendorAliasUnit vendorAlias)
        {
            //Validating if Duplicate VendorAliasName exists
            if (VendorAliasUnitRepository != null)
            {
                var vendorsAliasUnit = (await VendorAliasUnitRepository.GetAllListAsync(p => p.AliasName == vendorAlias.AliasName && p.VendorId== vendorAlias.VendorId));

                if (vendorAlias.Id == 0)
                {
                    if (vendorsAliasUnit.Count > 0)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", vendorAlias.AliasName));
                    }
                }
                else
                {
                    if (vendorsAliasUnit.FirstOrDefault(p => p.Id != vendorAlias.Id && p.AliasName == vendorAlias.AliasName && p.VendorId == vendorAlias.VendorId) != null)
                    {
                        throw new UserFriendlyException(L("Duplicate Name", vendorAlias.AliasName));
                    }
                }
            }
        }

    }

}
