using System.Web;
using System.Web.Optimization;

namespace CakeApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Content/admin/select2/select2.min.js",
            //          "~/Content/admin/js/bootstrap.min.js",
            //          "~/Content/admin/js/jquery.min.js",
            //          "~/Content/admin/js/jquery-migrate.min.js",
            //          "~/Content/admin/datatables/media/js/jquery.dataTables.js",
            //          "~/Content/admin/datatables/plugins/bootstrap/dataTables.bootstrap.js",
            //          "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/Content/main/js").Include(
                     
                     "~/Content/admin/select2/select2.min.js",
                     "~/Content/admin/js/bootstrap.min.js",
                     "~/Content/admin/js/jquery.dataTables.min.js",
                     //"~/Content/admin/datatables/media/js/jquery.dataTables.js",

                     "~/Content/admin/datatables/plugins/bootstrap/dataTables.bootstrap.js",
                     "~/Scripts/respond.js",
                     "~/Content/admin/js/fileinput.js",
                      "~/Content/admin/js/jquery.bsAlerts.min.js",
                      "~/Content/admin/js/jquery.form.js"));

            bundles.Add(new StyleBundle("~/Content/main/css").Include(
                      "~/Content/site.css",
                      "~/Content/admin/css/bootstrap.css",
                      "~/Content/admin/css/bootstrap-theme.css",
                      "~/Content/admin/font-awesome/css/font-awesome.css",
                      "~/Content/admin/select2/select2.css",
                      "~/Content/admin/datatables/plugins/bootstrap/dataTables.bootstrap.css",
                      "~/Content/admin/css/fileinput.css"
                      ));


            
           

            
        }
    }
}