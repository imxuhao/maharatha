using Abp.MultiTenancy;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Authorization.Users;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    /// <summary>
    /// Represents a Tenant in the system.
    /// A tenant is a isolated customer for the application
    /// which has it's own users, roles and other application entities.
    /// </summary>
    public class Tenant : AbpTenant<User>,IMayHaveOrganizationUnit
    {
        //Can add application specific tenant properties here
        public virtual int? LajitId { get; set; }
        protected Tenant()
        {

        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {

        }

        public long? OrganizationUnitId { get; set; }
    }
}