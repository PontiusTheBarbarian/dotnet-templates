// <copyright file="Startup.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using _Company_._Project_.WebApi.Modules;
using _Company_._Project_.WebApi.Modules.ExceptionHandling;
using _Company_._Project_.WebApi.Modules.FeatureManagement;
using _Company_._Project_.WebApi.Modules.Health;
using _Company_._Project_.WebApi.Modules.Logging;
using _Company_._Project_.WebApi.Modules.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Templates.WebApi.Modules.Auditing;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Startup.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration">Application configuration.</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		///  Gets Configuration object.
		/// </summary>
		public virtual IConfiguration Configuration { get; }

		/// <summary>
		/// Register services.
		/// </summary>
		/// <param name="services">Service collection.</param>
		public virtual void ConfigureServices(
			IServiceCollection services)
		{
			services
						   .AddFeatureFlags(Configuration)
						   .AddSerilog(Configuration)
						   .AddCustomControllers()
						   .AddAuthentication(Configuration)
						   .AddCors()
						   .AddInvalidRequestLogging()
						   .AddCustomHealthChecks()
						   .AddApplicationInsights(Configuration)
						   .AddVersioning()
						   .AddSwagger(Configuration)
						   .AddAuditing()
						   .AddMvc()
					   ;
		}

		/// <summary>
		/// Configure services.
		/// </summary>
		/// <param name="app">Application builder.</param>
		/// <param name="env">Application environment.</param>
		/// <param name="loggerFactory">Logger factory.</param>
		public virtual void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			ILoggerFactory loggerFactory)
			=> app
			   .UseSerilog(loggerFactory)
			   .UseExceptionHandling(env)
			   .UseVersionedSwagger(Configuration)
			   .UseCustomCors()
			   .UseHttpsRedirection()
			   .UseRouting()
			   .UseHealthChecks()
			   .UseAuthentication()
			   .UseAuthorization()
			   .UseEndpoints(endpoints =>
			   {
				   endpoints.MapControllers();
				   endpoints.MapHealthChecks("/health");
			   })
			;
	}
}
