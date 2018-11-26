using System.Web;
using System.Web.Optimization;

namespace ToDoList
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/wwwroot/libs/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/wwwroot/libs/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/wwwroot/libs/bootstrap.js",
                      "~/wwwroot/libs/respond.js"));

            bundles.Add(new StyleBundle("~/styles/css").Include(
                      "~/wwwroot/css/bootstrap.css",
                      "~/wwwroot/css/site.css"));

            bundles.Add(new ScriptBundle("~/scripts/home").Include("~/wwwroot/scripts/home.js"));
            bundles.Add(new ScriptBundle("~/scripts/layout").Include("~/wwwroot/scripts/layout.js"));
            bundles.Add(new ScriptBundle("~/scripts/tasks").Include("~/wwwroot/scripts/tasks.js"));

            bundles.Add(new StyleBundle("~/styles/home").Include("~/wwwroot/css/home.css"));
            bundles.Add(new StyleBundle("~/styles/tasks").Include("~/wwwroot/css/tasks.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
