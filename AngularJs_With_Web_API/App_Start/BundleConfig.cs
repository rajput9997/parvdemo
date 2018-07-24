using System.Web;
using System.Web.Optimization;

namespace AngularJs_With_Web_API
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/js/angular.js",
                "~/js/app.js",
                "~/js/userapp.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/css/bootstrap.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}