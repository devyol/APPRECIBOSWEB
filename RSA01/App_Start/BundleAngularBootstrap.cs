using System.Web;
using System.Web.Optimization;

namespace RSA01
{
    public class BundleAngularBootstrap
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                "~/Scripts/vendor/bootstrap.js"
                ));
            bundles.Add(new StyleBundle("~/content/bootstrap/css").Include(
                "~/Content/bootstrap.css"
                ));
        }
    }
}