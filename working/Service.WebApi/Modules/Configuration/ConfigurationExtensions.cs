// <copyright file="ConfigurationExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;

namespace Company.WebApi.Modules.Configuration
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
		/// <returns><see cref="IConfigurationBuilder"/>.</returns>
		internal static IConfigurationBuilder AddApplicationSettings(
			this IConfigurationBuilder configurationBuilder)
			=> configurationBuilder
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
					.AddEnvironmentVariables();
	}
}
