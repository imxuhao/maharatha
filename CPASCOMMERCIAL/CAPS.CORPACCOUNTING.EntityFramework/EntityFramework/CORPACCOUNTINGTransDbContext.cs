using System;
using System.Data.Entity;
using System.Threading;
using System.Security.Claims;
using System.Linq;
using Abp.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.EntityFramework
{
    public class CORPACCOUNTINGTransDbContext : AbpDbContext
    {
        /* Define an IDbSet for each entity of the application */


        public virtual IDbSet<CoaUnit> CoaUnit { get; set; }

        public  virtual  IDbSet<AccountUnit> AccountUnit { get; set; }

        public CORPACCOUNTINGTransDbContext()
        {
        }

        public CORPACCOUNTINGTransDbContext(string nameOrConnectionString) : base(GetDynamicConnectionString())
        {
        }

        #region Dynamic Connection Based on Tenenat ID

        public static string GetDynamicConnectionString()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            var claimsIdentity = claimsPrincipal?.Identity as ClaimsIdentity;

            //Getting the TenantID from Identity
            var connStringClaim =
                claimsIdentity?.Claims.FirstOrDefault(
                    c => c.Type == "http://www.aspnetboilerplate.com/identity/claims/tenantId");

            if (string.IsNullOrEmpty(connStringClaim?.Value))
            {
                return null;
            }

            return ConnectionStrings.GetConnectionString(Convert.ToInt32(connStringClaim.Value));

            // return @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;";
        }

        #endregion
    }
}