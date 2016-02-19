using System.Web.Optimization;

namespace BandageLanding
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }

        public static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/chola/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui-custom").Include(
                        "~/Scripts/jquery-ui-1.11.4.custom/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                    "~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/chola/modernizr.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/global").Include("~/Scripts/frissonlanding-global.js"));

            bundles.Add(new ScriptBundle("~/bundles/calendo-theme")
                    .Include(
                    "~/Scripts/calendo-theme/js/libs/jquery.bxslider.min.js",
                    "~/Scripts/calendo-theme/js/libs/jquery.fitvids.js",
                    "~/Scripts/calendo-theme/js/libs/flexslider-min.js",
                    "~/Scripts/calendo-theme/js/libs/jquery.magnific-popup.min.js",
                    "~/Scripts/calendo-theme/js/libs/scrollable.js",
                    "~/Scripts/calendo-theme/js/scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/chola/bootstrap.min.js",
                "~/Scripts/chola/jPushMenu.js",
                "~/Scripts/chola/v2p.js",
                "~/Scripts/chola/jquery.fancybox.js",
                "~/Scripts/chola/classie.js",
                "~/Scripts/chola/main.js",
                "~/Scripts/chola/masonry.js",
                "~/Scripts/chola/isotope.min.js",
                "~/Scripts/chola/jquery.tubular.1.0.js",
                "~/Scripts/chola/jquery.themepunch.tools.min.js",
                "~/Scripts/chola/jquery.themepunch.revolution.js",
                "~/Scripts/chola/settings.js",
                "~/Scripts/chola/scripts.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/cleditor").Include("~/Scripts/CLEditor1_4_5/jquery.cleditor.js"));
        }

        public static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/home/bundle")
                .IncludeDirectory("~/Content/home", "*.css"));

            bundles.Add(new StyleBundle("~/Content/calendo-theme")
                    .IncludeDirectory("~/Scripts/calendo-theme/css", "*.css"));

            bundles.Add(new StyleBundle("~/Content/Survey/bundle").Include("~/Content/survey/survey.css"));

            bundles.Add(new StyleBundle("~/Content/chola/bundle").Include(
                "~/Content/chola/animate.css",
                "~/Content/chola/jquery.fancybox.css",
                "~/Content/chola/bootstrap.min.css",
                "~/Content/chola/base.css",
                "~/Content/chola/style.css"
                ));

            bundles.Add(new StyleBundle("~/Content/cleditor/bundle").Include("~/Scripts/CLEditor1_4_5/jquery.cleditor.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui-custom/bundle")
                .Include(
                "~/Scripts/jquery-ui-1.11.4.custom/jquery-ui.css",
                "~/Scripts/jquery-ui-1.11.4.custom/jquery-ui.theme.css",
                "~/Scripts/jquery-ui-1.11.4.custom/jquery-ui.structure.css"));
        }
    }
}
