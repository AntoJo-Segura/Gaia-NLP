// <copyright file="LoggingExtensions.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Gaia.API.Extensions
{
    /// <summary>
    /// NLogExtensions.
    /// </summary>
    public static class LoggingExtensions
    {
        private const string GaiaLogLevel = "Logging:LogLevel:Default";

        /// <summary>
        /// Configures the n log.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>IWebHostBuilder.</returns>
        public static IWebHostBuilder ConfigureNLog(this IWebHostBuilder builder)
        {
            ConfigureNLog(builder, null);

            return builder;
        }

        public static IWebHostBuilder ConfigureAWSLambdaLogger(this IWebHostBuilder builder)
        {
            ConfigureLambdaLogger(builder);

            return builder;
        }

        private static IWebHostBuilder ConfigureLambdaLogger(this IWebHostBuilder builder)
        {
            builder.ConfigureLogging((ctx, logging) =>
            {
                logging.ClearProviders();

                // Create and populate LambdaLoggerOptions object
                var loggerOptions = new LambdaLoggerOptions
                {
                    IncludeCategory = true,
                    IncludeLogLevel = true,
                    IncludeNewline = true,
                    IncludeEventId = true,
                    IncludeException = true,
                };

                logging.AddFilter("Microsoft", LogLevel.Warning);
                logging.AddFilter("System", LogLevel.Warning);

                LogLevel level = string.IsNullOrEmpty(ctx.Configuration[GaiaLogLevel])
                ? LogLevel.Information
                : (LogLevel)Enum.Parse(typeof(LogLevel), ctx.Configuration[GaiaLogLevel]);

                logging.SetMinimumLevel(level);

                // Configure Lambda logging
                logging.AddLambdaLogger(loggerOptions);
            });

            return builder;
        }

        private static IWebHostBuilder ConfigureNLog(this IWebHostBuilder builder, Action<NLogOptions, IConfiguration> configure)
        {
            builder.ConfigureLogging((ctx, logging) =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                logging.AddConsole();

                var opts = new NLogOptions();

                configure?.Invoke(opts, ctx.Configuration);

                if (string.IsNullOrEmpty(opts.LogPath))
                {
                    var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    opts.LogPath = $"{directory}/logs";
                }

                NLogBuilder.ConfigureNLog("nlog.config");
                NLog.LogManager.Configuration.Variables["path"] = opts.LogPath;

                NLog.LogManager.Configuration.Reload();
            }).UseNLog();

            return builder;
        }
    }
}
