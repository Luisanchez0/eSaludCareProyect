using System.Web;
using System.Web.Optimization;

namespace eSaludCareUsers
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

            bundles.Add(new Bundle("~/bundles/complemento").Include(
                         "~/Scripts/scripts.js",
            "~/Scripts/vendor/bootstrap/js/bootstrap.bundle.min.js",
                         "~/Scripts/vendor/php-email-form/validate.js",
                         "~/Scripts/vendor/aos/aos.js",
                         "~/Scripts/vendor/glightbox/js/glightbox.min.js",
                         "~/Scripts/vendor/purecounter/purecounter_vanilla.js",
                         "~/Scripts/vendor/imagesloaded/imagesloaded.pkgd.min.js",
                         "~/Scripts/vendor/isotope-layout/isotope.pkgd.min.js",
                         "~/Scripts/vendor/swiper/swiper-bundle.min.js"));


            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/main.css"));
        }
    }
}
