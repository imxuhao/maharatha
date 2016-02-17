using Abp.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.MultiTenancy;
using Abp.Organizations;
using System;

namespace CAPS.CORPACCOUNTING.Authorization.Roles
{
    /// <summary>
    /// Represents a role in the system.
    /// </summary>
    public class Role : AbpRole<Tenant, User>,IMayHaveOrganizationUnit

    {
        public Role()
        {
            
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }

        /// <summary>
        /// Adding Organization ID to the Role table to support 
        /// </summary>
        public long? OrganizationUnitId { get; set; }
       
    }
}
