using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Homebank.Api.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Homebank.Api
{
    /// <summary>
    /// Startup class for the API.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The ApplicationBuilder.</param>
        /// <param name="env">The HostingEnvironment.</param>
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.BasePath = "/");
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Homebank API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the services.
        /// </summary>
        /// <param name="services">The ServiceCollection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var server = configuration["DATABASE_HOST"];
            var port = configuration["DATABASE_Port"];
            var databaseName = configuration["DATABASE_NAME"];
            var user = configuration["DATABASE_USER"];
            var password = configuration["DATABASE_PASSWORD"];

            var conenctionString = $"Server={server},{port};Database={databaseName};User Id={user};Password={password};";

            services.AddDbContext<HomebankContext>(options =>
            {
                options.UseSqlServer($"Server={server},{port};Database={databaseName};User Id={user};Password={password};");
            });

            services.AddFluentMigratorCore()
                .ConfigureRunner(builder =>
                {
                    builder.AddSqlServer()
                    .WithGlobalConnectionString(conenctionString)
                    .ScanIn(typeof(HomebankContext).Assembly).For.Migrations();
                });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                c.SwaggerDoc("v1", new Info { Title = "Homebank API", Version = "v1" });
                c.IncludeXmlComments(commentsFile);
            });
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Workaround for the DB creation.
            // Move this to a migration page where the user can click to migrate (also solves concurrency problems).
            Thread.Sleep(TimeSpan.FromSeconds(10));

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
