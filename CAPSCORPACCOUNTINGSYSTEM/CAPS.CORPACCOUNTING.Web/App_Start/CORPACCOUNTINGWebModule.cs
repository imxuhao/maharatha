using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.IO;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using CAPS.CORPACCOUNTING.Web.App.Startup;
using CAPS.CORPACCOUNTING.Web.Areas.Mpa.Startup;
using CAPS.CORPACCOUNTING.Web.Bundling;
using CAPS.CORPACCOUNTING.Web.Navigation;
using CAPS.CORPACCOUNTING.Web.Routing;
using CAPS.CORPACCOUNTING.WebApi;

namespace CAPS.CORPACCOUNTING.Web
{
    /// <summary>
    /// Web module of the application.
    /// This is the most top and entrance module that dependens on others.
    /// </summary>
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(CORPACCOUNTINGDataModule),
        typeof(CORPACCOUNTINGApplicationModule),
        typeof(CORPACCOUNTINGWebApiModule))]
    public class CORPACCOUNTINGWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Use database as language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<AppNavigationProvider>();
            Configuration.Navigation.Providers.Add<FrontEndNavigationProvider>();
            Configuration.Navigation.Providers.Add<MpaNavigationProvider>();
        }

        public override void Initialize()
        {
            //Dependency Injection
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
                );

            //Areas
            AreaRegistration.RegisterAllAreas();

            //Routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Bundling
            BundleTable.Bundles.IgnoreList.Clear();
            CommonBundleConfig.RegisterBundles(BundleTable.Bundles);
            AppBundleConfig.RegisterBundles(BundleTable.Bundles);
            FrontEndBundleConfig.RegisterBundles(BundleTable.Bundles);
            MpaBundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {
            var server = HttpContext.Current.Server;
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.SampleProfileImagesFolder = server.MapPath("~/Common/Images/SampleProfilePics");
            appFolders.TempFileDownloadFolder = server.MapPath("~/Temp/Downloads");

            try { DirectoryHelper.CreateIfNotExists(appFolders.TempFileDownloadFolder); } catch { }
        }
    }
}
