using Autofac;
using Autofac.Integration.Mvc;
using Homebank.Core.Repositories;
using Homebank.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Homebank.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
            RegisterServices();

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			new Migrator(ConfigurationManager.ConnectionStrings["Default"].ConnectionString).Migrate(r => r.MigrateUp());
        }

        private void RegisterServices() {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<DatabaseContext>()
                .WithParameter(new NamedParameter("connectionString", ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                .InstancePerRequest();

            builder.RegisterType<UserRepository>().InstancePerRequest();
            builder.RegisterType<AccountRepository>().InstancePerRequest();
            builder.RegisterType<BookingRepository>().InstancePerRequest();
            builder.RegisterType<CategoryRepository>().InstancePerRequest();
            builder.RegisterType<TransactionRepository>().InstancePerRequest();
            builder.RegisterType<TemplateRepository>().InstancePerRequest();
            builder.RegisterType<ReportingRepository>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
	}
}
