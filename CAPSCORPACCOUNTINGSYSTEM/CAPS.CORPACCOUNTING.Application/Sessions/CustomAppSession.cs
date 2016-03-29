using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

    }
}
