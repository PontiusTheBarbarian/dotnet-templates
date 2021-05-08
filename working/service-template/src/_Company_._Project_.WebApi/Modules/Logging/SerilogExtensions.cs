// <copyright file="SerilogExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Logging
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Serilog extensions.
	/// </summary>
	internal static class SerilogExtensions
	{
		/// <summary>
		/// Add logging provider.
		/// </summary>
		/// <param name="services">IServiceCollection servivces dependency injection container.</param>
		/// <param name="configuration">IConfiguration configuration.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddSerilog(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			Log.Logger = new LoggerConfiguration()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.WithApplicationInsights(configuration)
				.CreateLogger();

			services.AddSingleton(Log.Logger);

			return services;
		}

		/// <summary>
		/// Add Serilog to project.
		/// </summary>
		/// <param name="app">Application builder.</param>
		/// <param name="loggerFactory">Logger factory.</param>
		/// <returns><see cref="IApplicationBuilder"/>Application builder.</returns>
		internal static IApplicationBuilder UseSerilog(
			this IApplicationBuilder app,
			ILoggerFactory loggerFactory)
		{
			loggerFactory.AddSerilog();
			app.UseSerilogRequestLogging();

			return app;
		}

		private static LoggerConfiguration WithApplicationInsights(
			this LoggerConfiguration config,
			IConfiguration configuration)
			 => config.WriteTo.ApplicationInsights(
				 configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"),
				 TelemetryConverter.Events);
	}
}
