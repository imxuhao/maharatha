using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero;
using Abp.Organizations;
using System.Linq;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Authorization.Users;
using Abp.Authorization.Users;
using System.Data.Entity;

namespace CAPS.CORPACCOUNTING.Organization
{
    public class OrganizationExtendedUnitManager : OrganizationUnitManager
    {

        protected IRepository<OrganizationExtended, long> OrganizationExtendedUnitRepository { get; private set; }
        protected IRepository<UserOrganizationUnit,long> UserOrganizationUnitRepository { get; set; }
        public OrganizationExtendedUnitManager(IRepository<OrganizationExtended, long> organizationExtendedUnitRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository 
            ) : base(organizationUnitRepository)
        {
            OrganizationExtendedUnitRepository = organizationExtendedUnitRepository;
            UserOrganizationUnitRepository = userOrganizationUnitRepository;
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

        public async Task<List<OrganizationExtended>> GetExtendedOrganizationUnitsAsync(User user,EntityClassification entityClassificationId)
        {
            var query =await (from uou in UserOrganizationUnitRepository.GetAll()
                        join ou in OrganizationExtendedUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        where uou.UserId == user.Id && ou.EntityClassificationId== entityClassificationId
                        select ou).ToListAsync();
            return query;
        }
    }
}
