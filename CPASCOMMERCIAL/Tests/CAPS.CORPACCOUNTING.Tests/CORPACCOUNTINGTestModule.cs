using Abp.Modules;
using Abp.Zero.Configuration;

namespace CAPS.CORPACCOUNTING.Tests
{
    [DependsOn(
        typeof(CORPACCOUNTINGApplicationModule),
        typeof(CORPACCOUNTINGDataModule))]
    public class CORPACCOUNTINGTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Use database as language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();
        }
    }
}
