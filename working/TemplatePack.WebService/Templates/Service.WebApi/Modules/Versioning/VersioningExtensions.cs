// <copyright file="VersioningExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApi.Modules.Versioning
{
	/// <summary>
	/// Versioning extensions.
	/// </summary>
	internal static class VersioningExtensions
	{
		/// <summary>
		/// Add versioning to project.
		/// </summary>
		/// <param name="services"></param>
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
