using System.Web;
using System.Web.Optimization;

namespace PDFSignApplication
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/Content/cssfile").Include(
                      "~/Content/Css/jquery-ui.min.css",
                      "~/Content/Css/bootstrap.min.css"));


            bundles.Add(new StyleBundle("~/Content/css/Sign/File").Include(
                      "~/Content/Css/Sign/cssloading.css",
                      "~/Content/Css/Sign/midonline.css"));

            bundles.Add(new ScriptBundle("~/Content/scriptsfile").Include(
                      "~/Content/Scripts/polyfill.min.js",
                      "~/Content/Scripts/jquery.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/Content/scriptsfile2").Include(
                        "~/Content/Scripts/jquery-migrate.min.js",
                      "~/Content/Scripts/bootstrap.min.js"));


        }
    }
}
