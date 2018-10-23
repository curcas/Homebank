using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Homebank.Api
{
    /// <summary>
    /// Startup class for the API.
    /// </summary>
    public class Startup
    {
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
        }

        /// <summary>
        /// Configure the services.
        /// </summary>
        /// <param name="services">The ServiceCollection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
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
    }
}
