using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApi.Modules.Versioning
{
    internal static class VersioningExtensions
    {
        /// <summary>
        /// Configure API URL versioning
        /// </summary>
        /// <param name="services">IServiceCollection servivces dependency injection container</param>
        /// <param name="configureApiVersion">IConfiguration configuration</param>
        /// <returns>IServiceCollection services</returns>
       	internal static IServiceCollection AddVersioning(this IServiceCollection services)
		{
			var version = GetAssemblyVersion();

			services.AddSingleton<Version>(i => version);

			return services.AddApiVersioning(setup =>
		   {
			   setup.DefaultApiVersion = new ApiVersion(
				   majorVersion: version.Major,
				   minorVersion: version.Minor);
			   setup.AssumeDefaultVersionWhenUnspecified = true;
			   setup.ReportApiVersions = true;
		   });
		}

        internal static Version GetAssemblyVersion()
             => typeof(VersioningExtensions).Assembly.GetName().Version;
    }
}
