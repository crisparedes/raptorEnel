using System.Web;
using System.Web.Optimization;

namespace RaptorENEL_V._1._0
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery-{version}.js",
                        "~/js/jquery.validate.js",
                        "~/js/jquery.validate.unobtrusive.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/bootstrap-datepicker.js"));

            /*bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/js/jquery.validate*"));*/

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            /*bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/js/modernizr-*"));*/

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/js/bootstrap.js",
                      "~/js/respond.js",
                      "~/js/perfect-scrollbar.min.js",
                      "~/js/jquery.validate.js",
                      "~/js/form-validation.js",
                      "~/js/scripts.js",
                      "~/Scripts/gridmvc.js"));

            bundles.Add(new StyleBundle("~/css/css").Include(
                      "~/css/bootstrap.css",
                      "~/css/site.css",
                      "~/css/justified-nav.css",
                      "~/css/Layout.css",
                      "~/css/font-awesome.css",
                      "~/css/perfect-scrollbar.css",
                      "~/css/htmlGrid.css",
                      "~/Content/Gridmvc.css",
                      "~/Content/toastr.css",
                      "~/Content/bootstrap-datepicker.css"));
        }
    }
}
