using System.Data.Entity.Migrations;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using CAPS.CORPACCOUNTING.Migrations.Seed;
using CAPS.CORPACCOUNTING.Migrations.Seed.Host;
using CAPS.CORPACCOUNTING.Migrations.Seed.Tenants;

namespace CAPS.CORPACCOUNTING.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.CORPACCOUNTINGDbContext>, ISupportSeedMode
    {
        public SeedMode SeedMode { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CORPACCOUNTING";
            SeedMode = SeedMode.Host;
        }

        protected override void Seed(EntityFramework.CORPACCOUNTINGDbContext context)
        {
            context.DisableAllFilters();

            if (SeedMode == SeedMode.Host)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else if(SeedMode == SeedMode.Tenant)
            {
                //You can add seed for tenant databases...
            }

            context.SaveChanges();
        }
    }
}
