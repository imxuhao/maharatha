using System.Data.Entity.Migrations;
using CAPS.CORPACCOUNTING.Migrations.Seed;

namespace CAPS.CORPACCOUNTING.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CORPACCOUNTING.EntityFramework.CORPACCOUNTINGDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CORPACCOUNTING";
        }

        protected override void Seed(CORPACCOUNTING.EntityFramework.CORPACCOUNTINGDbContext context)
        {
            new InitialDbBuilder(context).Create();
        }
    }
}
