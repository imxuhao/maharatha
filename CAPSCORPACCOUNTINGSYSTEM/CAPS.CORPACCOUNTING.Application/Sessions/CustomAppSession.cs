using System.Linq;
using System.Security.Claims;
using System.Threading;
using Abp.Dependency;

namespace CAPS.CORPACCOUNTING.Sessions
{
    public class CustomAppSession:ITransientDependency
    {
        public string OrganizationId   
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

                var organizationClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "Application_UserOrgID");
                if (string.IsNullOrEmpty(organizationClaim?.Value))
                {
                    return null;
                }

                return organizationClaim.Value;
            }
        }

        public string TenantId
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

                var tenantClaim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == "http://www.aspnetboilerplate.com/identity/claims/tenantId");
                if (string.IsNullOrEmpty(tenantClaim?.Value))
                {
                    return null;
                }

                return tenantClaim.Value;
            }
        }

    }
}
