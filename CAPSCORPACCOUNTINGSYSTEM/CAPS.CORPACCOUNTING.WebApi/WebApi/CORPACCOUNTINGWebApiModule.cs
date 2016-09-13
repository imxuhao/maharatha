using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using Abp.WebApi.OData;
using Abp.WebApi.OData.Configuration;
using Swashbuckle.Application;

namespace CAPS.CORPACCOUNTING.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(AbpWebApiODataModule), typeof(CORPACCOUNTINGApplicationModule))]
    public class CORPACCOUNTINGWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
            ConfigureOData();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Automatically creates Web API controllers for all application services of the application
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(CORPACCOUNTINGApplicationModule).Assembly, "app")
                .Build();

            //Automatically create Web API controllers for a particular service

            //DynamicApiControllerBuilder.For<IAccountUnitAppService>("app/accountUnit").Build();

            //Overriding the methods you don't want to expose as web API

            //DynamicApiControllerBuilder.For<IAccountUnitAppService>("app/accountUnit").ForMethod("UpdateAccountUnit").DontCreateAction().Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

           // ConfigureSwaggerUi(); //Remove this line to disable swagger UI.
        }

        private void ConfigureOData()
        {
            var builder = Configuration.Modules.AbpWebApiOData().ODataModelBuilder;

            //Configure your entities here... see documentation: http://www.aspnetboilerplate.com/Pages/Documents/OData-Integration
            //builder.EntitySet<YourEntity>("YourEntities");
        }

        private void ConfigureSwaggerUi()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var commentsFileName = "CAPS.CORPACCOUNTING.Application" + ".XML";
            var commentsFile = Path.Combine(baseDirectory, commentsFileName);
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SUMIT WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.IncludeXmlComments(commentsFile);
                    c.DocumentFilter<FilterRoutesDocumentFilter>();
                })
                .EnableSwaggerUi();
        }
    }
}
