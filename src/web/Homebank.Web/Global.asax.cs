using Homebank.Repositories;
using Homebank.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;

namespace Homebank.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			new Migrator(ConfigurationManager.ConnectionStrings["Default"].ConnectionString).Migrate(r => r.MigrateUp());
		}
	}
}
