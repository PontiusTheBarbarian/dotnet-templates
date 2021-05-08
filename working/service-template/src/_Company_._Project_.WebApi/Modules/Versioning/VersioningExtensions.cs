// <copyright file="VersioningExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Versioning
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Versioning extensions.
	/// </summary>
	internal static class VersioningExtensions
	{
		/// <summary>
		/// Add versioning to project.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddVersioning(
			this IServiceCollection services)
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

		/// <summary>
		/// Get Assembly version.
		/// </summary>
		/// <returns><see cref="Version"/>.</returns>
		internal static Version GetAssemblyVersion()
			 => typeof(VersioningExtensions).Assembly.GetName().Version;
	}
}
