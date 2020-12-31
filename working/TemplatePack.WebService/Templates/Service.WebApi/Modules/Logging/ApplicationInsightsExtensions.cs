using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApi.Modules.Logging
{
    internal static class ApplicationInsightsExtensions
    {
		/// <summary>
		/// Add application insights and application insights telemetry client
		/// </summary>
		/// <param name="services">IServiceCollection servivces dependency injection container</param>
		/// <param name="configuration">IConfiguration configuration</param>
		/// <returns>IServiceCollection services</returns>
		internal static IServiceCollection AddApplicationInsights(this IServiceCollection services,
            IConfiguration configuration)
        {
			services.AddApplicationInsightsTelemetry(configuration);
			services.AddTelemetryClient(configuration);

            return services;
        }

		private static IServiceCollection AddTelemetryClient(this IServiceCollection services, IConfiguration configuration)
		{
			TelemetryConfiguration telemetryConfiguration = TelemetryConfiguration.CreateDefault();
			telemetryConfiguration.InstrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

			services.AddSingleton(telemetryConfiguration);

			return services;
		}
	}
}
