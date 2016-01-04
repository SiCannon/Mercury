using System.Web.Optimization;
using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;

namespace Memphis.Website.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/angular-lib").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-resource.js",
                        "~/Scripts/angular-sanitize.js",
                        "~/Scripts/angular-ui-select/select.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/angular-app")
                        .Include("~/JsSc/Modules/sc.js")
                        .Include("~/JsMp/Modules/app.js")
                        .IncludeDirectory("~/JsSc", "*.js", true)
                        .IncludeDirectory("~/JsMp", "*.js", true));

            var lessBundle = new CustomStyleBundle("~/bundles/less");
            lessBundle.Orderer = new NullOrderer();
            lessBundle.Include("~/Content/bootstrap/bootstrap.less");
            lessBundle.Include("~/Content/font-awesome/font-awesome.less");
            lessBundle.IncludeDirectory("~/Content", "*.less", false);
            bundles.Add(lessBundle);

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/angular-ui-select/select.css"
                      ));
        }
    }
}