using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.UI;
using Abp.Zero;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Organization
{
    public class OrganizationExtendedUnitManager : OrganizationUnitManager
    {

        protected IRepository<OrganizationExtended, long> OrganizationExtendedUnitRepository { get; private set; }

        public OrganizationExtendedUnitManager(IRepository<OrganizationExtended, long> organizationExtendedUnitRepository, IRepository<OrganizationUnit, long> organizationUnitRepository) : base(organizationUnitRepository)
        {
            OrganizationExtendedUnitRepository = organizationExtendedUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        [UnitOfWork]
        public async Task CreateAsync(OrganizationExtended organizationExtendedUnit)
        {
            organizationExtendedUnit.Code = await GetNextChildCodeAsync(organizationExtendedUnit.ParentId);
            await ValidateOrganizationUnitAsync(organizationExtendedUnit);
            await OrganizationExtendedUnitRepository.InsertAsync(organizationExtendedUnit);
        }

        public async Task UpdateAsync(OrganizationExtended organizationExtendedUnit)
        {
            await ValidateOrganizationUnitAsync(organizationExtendedUnit);
            await OrganizationExtendedUnitRepository.UpdateAsync(organizationExtendedUnit);
        }
    }
}
