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
						"~/Scripts/bootstrap.js"));

			bundles.Add(new StyleBundle("~/bundles/styles").Include(
					  "~/Content/themes/dark-hive/jquery-ui-{version}.custom.css",
					  "~/Content/bootstrap.css",
					  "~/Content/site.css"));

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
