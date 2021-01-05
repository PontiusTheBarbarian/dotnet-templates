// <copyright file="Startup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#if IncludeAuditing
using Templates.WebApi.Modules.Auditing;
#endif
using Company.WebApi.Modules;
using Company.WebApi.Modules.ExceptionHandling;
using Company.WebApi.Modules.FeatureManagement;
using Company.WebApi.Modules.Health;
using Company.WebApi.Modules.Logging;
using Company.WebApi.Modules.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Company.WebApi
{
	/// <summary>
	/// Startup.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		/// <summary>
		/// Gets Configuration object.
		/// </summary>
		public virtual IConfiguration Configuration { get; }

		/// <summary>
		/// Register services.
		/// </summary>
		/// <param name="services"></param>
		public virtual void ConfigureServices(
			IServiceCollection services)
			=> services
				.AddFeatureFlags(this.Configuration)
				.AddSerilog(this.Configuration)
				.AddCustomControllers()
				.AddAuthentication(this.Configuration)
				.AddCors()
				.AddInvalidRequestLogging()
				.AddCustomHealthChecks()
				.AddApplicationInsights(this.Configuration)
				.AddVersioning()
				.AddSwagger(this.Configuration)
#if IncludeAuditing
                .AddAuditing()
#endif
				.AddMvc()
				;

		/// <summary>
		/// Configure services.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="loggerFactory"></param>
		public virtual void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			ILoggerFactory loggerFactory)
			=> app
			   .UseSerilog(loggerFactory)
			   .UseExceptionHandling(env)
			   .UseVersionedSwagger(this.Configuration)
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
