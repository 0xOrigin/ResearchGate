using System.Web;
using System.Web.Optimization;

namespace Research_Gate
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
                            "~/Scripts/chosen.jquery.min.js", 
                            "~/Scripts/chosen.proto.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/chosen.css",
                      "~/Content/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/Content/site.css",
                      "~/Content/search.css",
                      "~/Content/paper.css",
                      "~/Content/home.css",
                      "~/Content/edit-profile.css",
                      "~/Content/upload-paper.css",
                      "~/Content/profile.css",
                      "~/Content/register.css",
                      "~/Content/login.css"));
        }
    }
}
