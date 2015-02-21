using System.Web;
using System.Web.Optimization;

namespace Homebank.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
				"~/Scripts/jquery-{version}.js",
				"~/Scripts/jquery-ui-{version}.js",
				"~/Scripts/bootstrap.js",
				"~/Scripts/Highcharts-4.0.1/js/highcharts.js",
				"~/Scripts/Highcharts-4.0.1/js/modules/no-data-to-display.js",
				"~/Scripts/select2.min.js"));

			bundles.Add(new StyleBundle("~/bundles/Content/themes/dark-hive/style").Include(
				"~/Content/themes/dark-hive/jquery-ui-{version}.custom.css", new CssRewriteUrlTransform()));

			bundles.Add(new StyleBundle("~/bundles/styles").Include(
				"~/Content/bootstrap.css",
				"~/Content/site.css",
				"~/Content/css/select2.css"));

			bundles.Add(new ScriptBundle("~/bundles/mobile/scripts").Include(
				"~/Scripts/jquery-{version}.js",
				"~/Scripts/jquery.mobile-{version}.js",
				"~/Scripts/jquery.mmenu.min.all.js"));

			bundles.Add(new StyleBundle("~/bundles/mobile/styles").Include(
				"~/Content/jquery.mobile-{version}.css",
				"~/Content/jquery.mmenu.all.css"));
		}
	}
}