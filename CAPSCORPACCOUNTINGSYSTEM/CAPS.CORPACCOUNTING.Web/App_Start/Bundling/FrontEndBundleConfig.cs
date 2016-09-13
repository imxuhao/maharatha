using System.Web.Optimization;

namespace CAPS.CORPACCOUNTING.Web.Bundling
{
    public static class FrontEndBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //LIBRARIES

            AddFrontendCssLibs(bundles, false);
            AddFrontendCssLibs(bundles, true);

            bundles.Add(
                new ScriptBundle("~/Bundles/Frontend/libs/js")
                    .Include(
                        ScriptPaths.Json2,
                        ScriptPaths.JQuery,
                        ScriptPaths.JQuery_Migrate,
                        ScriptPaths.Bootstrap,
                        ScriptPaths.Bootstrap_Hover_Dropdown,
                        ScriptPaths.JQuery_Slimscroll,
                        ScriptPaths.JQuery_BlockUi,
                        ScriptPaths.JQuery_Cookie,
                        ScriptPaths.SpinJs,
                        ScriptPaths.SpinJs_JQuery,
                        ScriptPaths.SweetAlert,
                        ScriptPaths.Toastr,
                        ScriptPaths.MomentJs,
                        ScriptPaths.MomentTimezoneJs,
                        ScriptPaths.Abp,
                        ScriptPaths.Abp_JQuery,
                        ScriptPaths.Abp_Toastr,
                        ScriptPaths.Abp_BlockUi,
                        ScriptPaths.Abp_SpinJs,
                        ScriptPaths.Abp_SweetAlert,
                        ScriptPaths.Abp_Moment
                    ).ForceOrdered()
                );
            //ABP Dependencies Libraries

            bundles.Add(new ScriptBundle("~/Bundles/ABP/libs/Extjs").Include(
                ScriptPaths.Json2,
                ScriptPaths.JQuery,
                ScriptPaths.SweetAlert,
                ScriptPaths.Toastr,
                ScriptPaths.MomentJs,
                ScriptPaths.MomentTimezoneJs,
                ScriptPaths.Abp,
                ScriptPaths.Abp_JQuery,
                ScriptPaths.Abp_Toastr,
                ScriptPaths.Abp_BlockUi,
                ScriptPaths.Abp_SpinJs,
                ScriptPaths.Abp_SweetAlert,
                ScriptPaths.Abp_Moment
                ).ForceOrdered());
            AddCommonCssForExtjs(bundles);
           
            //METRONIC

            AddFrontendCssMetronic(bundles, false);
            AddFrontendCssMetronic(bundles, true);

            bundles.Add(
                new ScriptBundle("~/Bundles/Frontend/metronic/js")
                    .Include(
                        "~/metronic/assets/frontend/layout/scripts/back-to-top.js",
                        "~/metronic/assets/frontend/layout/scripts/layout.js"
                    ).ForceOrdered()
                );
            //Minify Bundles regardless of web.config setting
            BundleTable.EnableOptimizations = true;
        }

        private static void AddFrontendCssLibs(BundleCollection bundles, bool isRTL)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/Frontend/libs/css" + (isRTL ? "RTL" : ""))
                    .Include(StylePaths.Simple_Line_Icons, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.FontAwesome, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.FamFamFamFlags, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(isRTL ? StylePaths.BootstrapRTL : StylePaths.Bootstrap, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.SweetAlert)
                    .Include(StylePaths.Toastr)
                    .ForceOrdered()
                );
        }

        private static void AddFrontendCssMetronic(BundleCollection bundles, bool isRTL)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/Frontend/metronic/css" + (isRTL ? "RTL" : ""))
                    .Include("~/metronic/assets/global/css/components" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/frontend/layout/css/style" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/frontend/pages/css/style-revolution-slider.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/frontend/layout/css/style-responsive" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/frontend/layout/css/themes/red" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .ForceOrdered()
                );
        }

        private static void AddCommonCssForExtjs(BundleCollection bundles)
        {
            //*****Important Note: When adding any image/external dependency in any of the css file make sure to add CssRewriteUrlWithVirtualDirectoryTransform() for given css path in below bundle.
            bundles.Add(
                new StyleBundle("~/Bundles/ABP/css/Extjs").Include(StylePaths.FamFamFlags, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.LineIcons, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.MultiSearch)
                    .Include(StylePaths.ChachingGlobal)
                    .Include(StylePaths.Sweet_Alert)
                    .Include(StylePaths.ABPToast)
                    .ForceOrdered());
        }
    }
}