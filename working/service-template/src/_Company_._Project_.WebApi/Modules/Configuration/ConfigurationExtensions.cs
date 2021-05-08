// <copyright file="ConfigurationExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Configuration
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Configuration extensions.
	/// </summary>
	internal static class ConfigurationExtensions
	{
		/// <summary>
		/// Loads application settings.
		/// </summary>
		/// <param name="configurationBuilder"><see cref="IConfigurationBuilder"/>.</param>
		/// <returns><see cref="IConfigurationBuilder"/>Returns the configuration builder.</returns>
		internal static IConfigurationBuilder AddApplicationSettings(
			this IConfigurationBuilder configurationBuilder)
			=> configurationBuilder
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
					.AddEnvironmentVariables();
	}
}
