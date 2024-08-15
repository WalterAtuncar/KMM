using System.Web.Optimization;

namespace WebApplication.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region jqGrid
            bundles.Add(new ScriptBundle("~/bundles/jqGrid.js").Include(
                     "~/Content/plugins/jqGrid/js/grid.locale-en.js",
                     "~/Content/plugins/jqGrid/js/jquery.jqGrid.min.js"));

            bundles.Add(new StyleBundle("~/bundles/jqGrid.css").Include(
                      "~/Content/plugins/jqGrid/css/jquery-ui.min.css",
                      "~/Content/plugins/jqGrid/css/ui.jqgrid.css"));
            #endregion
        }
    }
}