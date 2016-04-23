using System.Configuration;
using Homebank.Repositories;
using Ninject.Parameters;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Homebank.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Homebank.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Homebank.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
				Container.Initialize(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
			kernel.Bind<DatabaseContext>().To<DatabaseContext>().InRequestScope().WithParameter(new ConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["Default"].ConnectionString));

			kernel.Bind<UserRepository>().To<UserRepository>().InRequestScope();
			kernel.Bind<AccountRepository>().To<AccountRepository>().InRequestScope();
			kernel.Bind<CategoryRepository>().To<CategoryRepository>().InRequestScope();
			kernel.Bind<TransactionRepository>().To<TransactionRepository>().InRequestScope();
			kernel.Bind<TemplateRepository>().To<TemplateRepository>().InRequestScope();
			kernel.Bind<ReportingRepository>().To<ReportingRepository>().InRequestScope();	
        }        
    }
}
