// <copyright file="ApplicationInsightsExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Logging
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Application extensions.
	/// </summary>
	internal static class ApplicationInsightsExtensions
	{
		/// <summary>
		/// Add application insights and application insights telemetry client.
		/// </summary>
		/// <param name="services">IServiceCollection servivces dependency injection container.</param>
		/// <param name="configuration">IConfiguration configuration.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddApplicationInsights(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddApplicationInsightsTelemetry(configuration)
					.AddTelemetryClient(configuration);

			return services;
		}

		private static IServiceCollection AddTelemetryClient(this IServiceCollection services, IConfiguration configuration)
		{
			var telemetryConfiguration = TelemetryConfiguration.CreateDefault();
			telemetryConfiguration.InstrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

			services.AddSingleton(telemetryConfiguration);

			return services;
		}
	}
}
