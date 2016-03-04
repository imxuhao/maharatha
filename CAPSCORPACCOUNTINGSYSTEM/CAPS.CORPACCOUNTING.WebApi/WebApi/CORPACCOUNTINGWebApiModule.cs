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
using Abp.WebApi.OData;
using Abp.WebApi.OData.Configuration;
using CAPS.CORPACCOUNTING.Masters;

#pragma warning disable 1587

namespace CAPS.CORPACCOUNTING.WebApi
{
    /// <summary>
    /// Web API layer of the application.
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule), typeof(CORPACCOUNTINGApplicationModule),typeof(AbpWebApiODataModule))]
    public class CORPACCOUNTINGWebApiModule : AbpModule
    {
        /// <summary>
        /// This is the first event called on application startup. 
        /// OData requires to declare entities which can be used as OData resources. We should do this in PreInitialize method of our module, as shown below:
        /// </summary>
        public override void PreInitialize()
        {
            var builder = Configuration.Modules.AbpWebApiOData().ODataModelBuilder;

            //Configure your entities here...
            builder.EntitySet<CoaUnit>("COA");
        }

        /// <summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ///Automatically creates Web API controllers for all application services of the application

            DynamicApiControllerBuilder.ForAll<IApplicationService>(typeof(CORPACCOUNTINGApplicationModule).Assembly, "app").Build();

            ///Automatically create Web API controllers for a particular service

            //DynamicApiControllerBuilder.For<IAccountUnitAppService>("app/accountUnit").Build();

            ///Overriding the methods you don't want to expose as web API

            //DynamicApiControllerBuilder.For<IAccountUnitAppService>("app/accountUnit").ForMethod("UpdateAccountUnit").DontCreateAction().Build();


            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

            ConfigureSwaggerUi();
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
