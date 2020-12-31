using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Company.WebApi.Modules.Logging
{
    internal static class SerilogExtensions
    {
        /// <summary>
        /// Add logging provider
        /// </summary>
        /// <param name="services">IServiceCollection servivces dependency injection container</param>
        /// <param name="configuration">IConfiguration configuration</param>
        /// <returns>IConfiguration configuration</returns>
        internal static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WithApplicationInsights(configuration)
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            return services;
        }

        internal static IApplicationBuilder UseSerilog(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            app.UseSerilogRequestLogging();

            return app;
        }

        private static LoggerConfiguration WithApplicationInsights(this LoggerConfiguration config, IConfiguration configuration)
             => config.WriteTo.ApplicationInsights(
                 configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"),
                 TelemetryConverter.Events);
    }
}
