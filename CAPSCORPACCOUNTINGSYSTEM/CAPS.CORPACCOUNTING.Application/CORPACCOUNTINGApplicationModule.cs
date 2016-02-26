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
    [DependsOn(typeof(CORPACCOUNTINGCoreModule), typeof(AbpAutoMapperModule))]
    public class CORPACCOUNTINGApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            ///TBD:This needs to be worked upon
           // DTOInterceptorRegistrar.Initialize(IocManager);

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Custom DTO auto-mappings
            CustomDtoMapper.CreateMappings();


        }
    }
}
