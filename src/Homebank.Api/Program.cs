using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Homebank.Api
{
    /// <summary>
    /// The programm.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Start the WebHost.
        /// </summary>
        /// <param name="args">The console arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        /// <summary>
        /// Configure the WebHost.
        /// </summary>
        /// <param name="args">The console arguments.</param>
        /// <returns>The WebHost.</returns>
        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((builderContext, config) =>
            {
                config.AddEnvironmentVariables();
            })
            .UseStartup<Startup>()
            .Build();
        }
    }
}
