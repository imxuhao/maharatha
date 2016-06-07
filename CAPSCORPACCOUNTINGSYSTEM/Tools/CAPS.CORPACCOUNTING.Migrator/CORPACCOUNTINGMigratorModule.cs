using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using CAPS.CORPACCOUNTING.EntityFramework;

namespace CAPS.CORPACCOUNTING.Migrator
{
    [DependsOn(typeof(CORPACCOUNTINGDataModule))]
    public class CORPACCOUNTINGMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<CORPACCOUNTINGDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}