using GFCA.APT.WEB.Helpers;
using System.Web;
using System.Web.Optimization;

namespace GFCA.APT.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.css",
                     "~/Content/site.css"));

            Bundle csslteStyle = new StyleBundle("~/Content/cssLte");
            csslteStyle.Orderer = new AsIsBundleOrderer();
            csslteStyle
                .Include("~/Content/plugins/fontawesome-free/css/all.min.css")
                .Include("~/Content/plugins/ionicons/css/ionicons.min.css")
                .Include("~/Content/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css")
                .Include("~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css")
                .Include("~/Content/plugins/jqvmap/jqvmap.min.css")
                .Include("~/Content/dist/css/adminlte.min.css")
                .Include("~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css")
                .Include("~/Content/plugins/daterangepicker/daterangepicker.css")
                .Include("~/Content/plugins/summernote/summernote-bs4.min.css")
                //.Include("~/Content/bootstrap.css")
                .Include("~/Content/ej2/bootstrap4.css")
                .Include("~/Scripts/bs4-toast/dist/toast.min.css")
                .Include("~/Content/Site.css")
                ;
            bundles.Add(csslteStyle);

            /*
            https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback <!-- Google Font: Source Sans Pro -->
            ~/Content/plugins/fontawesome-free/css/all.min.css                                        <!-- Font Awesome -->
            https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css                       <!-- Ionicons -->
            ~/Content/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css         <!-- Tempusdominus Bootstrap 4 -->
            ~/Content/plugins/icheck-bootstrap/icheck-bootstrap.min.css                               <!-- iCheck -->
            ~/Content/plugins/jqvmap/jqvmap.min.css                                                   <!-- JQVMap -->
            ~/Content/dist/css/adminlte.min.css                                                       <!-- Theme style -->
            ~/Content/plugins/overlayScrollbars/css/OverlayScrollbars.min.css                         <!-- overlayScrollbars -->
            ~/Content/plugins/daterangepicker/daterangepicker.css                                     <!-- Daterange picker -->
            ~/Content/plugins/summernote/summernote-bs4.min.css                                       <!-- summernote -->
            */
            Bundle polyfillScript = new ScriptBundle("~/bundles/polyfill");
            polyfillScript.Orderer = new AsIsBundleOrderer();
            polyfillScript
                .Include("~/Scripts/es6-promise/dist/es6-promise.js")
                .Include("~/Scripts/es6-promise/dist/es6-promise.auto.js")
                ;
            bundles.Add(polyfillScript);

            Bundle ejScript = new ScriptBundle("~/bundles/ejScripts");
            ejScript.Orderer = new AsIsBundleOrderer();
            ejScript
                .Include("~/Scripts/jsrender.min.js")
                .Include("~/Scripts/ej/ej.web.all.min.js")
                .Include("~/Scripts/ej/ej.unobtrusive.min.js")
                ;
            bundles.Add(ejScript);

            Bundle jqueyLteScript = new ScriptBundle("~/bundles/jquerylte");
            jqueyLteScript.Orderer = new AsIsBundleOrderer();
            jqueyLteScript
                .Include("~/Content/plugins/jquery/jquery.min.js ")
                .Include("~/Content/plugins/jquery-ui/jquery-ui.min.js")
                ;
            bundles.Add(jqueyLteScript);
            /*
            /Content/plugins/jquery/jquery.min.js                   <!-- jQuery -->
            /Content/plugins/jquery-ui/jquery-ui.min.js             <!-- jQuery UI 1.11.4 -->
            <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
            <script>
                $.widget.bridge('uibutton', $.ui.button)
            </script>
            
            /Content/plugins/bootstrap/js/bootstrap.bundle.min.js   <!-- Bootstrap 4 -->
            /Content/plugins/chart.js/Chart.min.js                  <!-- ChartJS -->
            /Content/plugins/sparklines/sparkline.js                <!-- Sparkline -->
            /Content/plugins/jqvmap/jquery.vmap.min.js              <!-- JQVMap -->
            /Content/plugins/jqvmap/maps/jquery.vmap.usa.js
            /Content/plugins/jquery-knob/jquery.knob.min.js         <!-- jQuery Knob Chart -->
            /Content/plugins/moment/moment.min.js                   <!-- daterangepicker -->
            /Content/plugins/daterangepicker/daterangepicker.js
            /Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js  <!-- Tempusdominus Bootstrap 4 -->
            /Content/plugins/summernote/summernote-bs4.min.js       <!-- Summernote -->
            /Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js           <!-- overlayScrollbars -->
            /Content/dist/js/adminlte.js                            <!-- AdminLTE App -->
            */

            Bundle adminlteScript = new ScriptBundle("~/bundles/adminlte");
            adminlteScript.Orderer = new AsIsBundleOrderer();
            adminlteScript
                .Include("~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js")
                .Include("~/Content/plugins/chart.js/Chart.min.js")
                .Include("~/Content/plugins/sparklines/sparkline.js")
                .Include("~/Content/plugins/jqvmap/jquery.vmap.min.js")
                .Include("~/Content/plugins/jqvmap/maps/jquery.vmap.usa.js")
                .Include("~/Content/plugins/jquery-knob/jquery.knob.min.js")
                .Include("~/Content/plugins/moment/moment.min.js")
                .Include("~/Content/plugins/daterangepicker/daterangepicker.js")
                .Include("~/Content/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js")
                .Include("~/Content/plugins/summernote/summernote-bs4.min.js")
                .Include("~/Content/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js")
                .Include("~/Content/dist/js/adminlte.js")
                .Include("~/Scripts/jquery.bsAlerts.min.js")
                .Include("~/Scripts/bs4-toast/dist/toast.min.js")
                ;
            bundles.Add(adminlteScript);
            bundles.Add(new StyleBundle("~/bundles/ejstyles").Include(
                      "~/ejThemes/flat-saffron/ej.web.all.min.css"));

            
        }
    }
}
