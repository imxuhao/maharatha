using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Zero;
using System;

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

            //await ValidateCOAUnitAsync(coaUnit);
            await CoaUnitRepository.InsertAsync(coaUnit);
        }

        public virtual async Task UpdateAsync(CoaUnit coaUnit)
        {
            await ValidateCOAUnitAsync(coaUnit);
            await CoaUnitRepository.UpdateAsync(coaUnit);
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(int id)
        {
            await CoaUnitRepository.DeleteAsync(id);
        }

        protected virtual async Task ValidateCOAUnitAsync(CoaUnit coaUnit)
        {
            //Validating if Duplicate COA exists 
            
                //var COAs = await CoaUnitRepository.GetAllListAsync(cu => cu.Caption == coaUnit.Caption && cu.OrganizationUnitId == coaUnit.OrganizationUnitId && cu.TenantId == coaUnit.TenantId);

                //if (COAs.Count > 0)
                //{
                //    throw new UserFriendlyException(L("Duplicate Chart of Account", coaUnit.Caption));
                //}
            
        }
    }
}
