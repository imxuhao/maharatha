using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using CAPS.CORPACCOUNTING.Migrations.Seed;
using CAPS.CORPACCOUNTING.Migrations.Seed.Host;
using CAPS.CORPACCOUNTING.Migrations.Seed.Tenants;

namespace CAPS.CORPACCOUNTING.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.CORPACCOUNTINGDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CORPACCOUNTING";
            
        }

        protected override void Seed(EntityFramework.CORPACCOUNTINGDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
              new  InitialTenantDbBuilder(context, Tenant.Id).Create();
            }

            context.SaveChanges();
        }
    }
}
