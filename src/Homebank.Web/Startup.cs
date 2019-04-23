using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Homebank.Core.Interfaces.Repositories;
using Homebank.Core.Migrations;
using Homebank.Core.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Homebank.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=localhost;Database=Homebank;User Id=sa;Password=H0mebankPassw0rd!";

            services.AddDbContext<DatabaseContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Security/Login");
                    options.LogoutPath = new PathString("/Security/Logout");
                }
            );

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITemplateRepository, TemplateRepository>();
            services.AddTransient<IReportingRepository, ReportingRepository>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder => builder
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Migration_001).Assembly).For.Migrations())
                .BuildServiceProvider(false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseExceptionHandler("/Error/500");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseRequestLocalization(new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture("de-CH")
            });
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            UpdateDatabase(app.ApplicationServices);
        }

        private void UpdateDatabase(IServiceProvider serviceProvider)
        {
            using(var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }
    }
}
