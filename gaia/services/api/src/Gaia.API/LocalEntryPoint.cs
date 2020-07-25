// <copyright file="LocalEntryPoint.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using Gaia.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Gaia.API
{
    public sealed class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureNLog()
                        .UseStartup<Startup>();
                });
    }
}
