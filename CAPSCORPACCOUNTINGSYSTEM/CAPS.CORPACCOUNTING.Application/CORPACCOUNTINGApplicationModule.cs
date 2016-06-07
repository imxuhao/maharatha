using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Interceptors;

namespace CAPS.CORPACCOUNTING
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(typeof(CORPACCOUNTINGCoreModule))]
    public class CORPACCOUNTINGApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
            //Adding an interceptor to measure the time taken for each method to execute in Applition
            MeasureDurationInterceptorRegistrar.Initialize(IocManager.IocContainer.Kernel);
        }
   
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Custom DTO auto-mappings
            CustomDtoMapper.CreateMappings();
        }
    }
}
