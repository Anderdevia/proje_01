using System.Web;
using System.Web.Optimization;

namespace MVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryShopval").Include(
                           "~/Scripts/bootstrap.bundle.min-431.js",
                           // "~/Scripts/bootstrap.bundle.min.js",
                           "~/Scripts/jquery-3.3.1.slim.min.js",
                        "~/Scripts/jquery-1.11.0.min.js",
                        "~/Scripts/jquery-migrate-1.2.1.min.js",
                        "~/Scripts/templatemo.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryRegistro").Include(
                      "~/Scripts/bootstrap.bundle.min-431.js",
                      "~/Scripts/jquery-3.3.1.slim.min.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));


        }
    }
}
