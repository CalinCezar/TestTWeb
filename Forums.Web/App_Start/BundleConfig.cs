using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Forums.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            //Bootstrap 
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include("~/Scripts/bootstrap.min.js"));

            //Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include("~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            //Font Awesome icons style
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            //Toaster
            bundles.Add(new StyleBundle("~/bundles/toaster/css").Include("~/Vendors/toastr/toastr.min.css", new CssRewriteUrlTransform()));

            //DataTables
            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include("~/Vendors/datatables/datatables.min.css", new CssRewriteUrlTransform()));
            

        }
    }
}