using Abp.Configuration.Startup;
using Abp.Runtime.Session;
using CAPS.CORPACCOUNTING.MultiTenancy;

namespace CAPS.CORPACCOUNTING.Runtime.Session
{
    /// <summary>
    /// Extends <see cref="IdentityFrameworkClaimsAbpSession"/>  (from Abp.Zero library).
    /// </summary>
    public class AspNetZeroAbpSession : IdentityFrameworkClaimsAbpSession
    {
        public ITenantIdAccessor TenantIdAccessor { get; set; }

        public override int? TenantId
        {
            get
            {
                if (base.TenantId != null)
                {
                    return base.TenantId;
                }

                //Try to find tenant from ITenantIdAccessor, if provided
                return TenantIdAccessor?.GetCurrentTenantIdOrNull(false); //set false to prevent circular usage.
            }
        }

        public AspNetZeroAbpSession(IMultiTenancyConfig multiTenancy) 
            : base(multiTenancy)
        {

        }
    }
}