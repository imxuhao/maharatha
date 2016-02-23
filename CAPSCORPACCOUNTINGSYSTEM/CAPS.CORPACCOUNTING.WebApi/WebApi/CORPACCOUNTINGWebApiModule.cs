using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Swashbuckle.Application;
using System.Linq;
using System.IO;
using System;

namespace CAPS.CORPACCOUNTING.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(CORPACCOUNTINGApplicationModule))]
    public class CORPACCOUNTINGWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Automatically creates Web API controllers for all application services of the application
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(CORPACCOUNTINGApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            ConfigureSwaggerUi();
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SUMIT WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                                             
                   // c.IncludeXmlComments(@"C:\Source Code\Cha-Ching\XMLComments\Application Module\CAPS.CORPACCOUNTING.Application.XML");
                })
                .EnableSwaggerUi();
            
        }
    }
}
