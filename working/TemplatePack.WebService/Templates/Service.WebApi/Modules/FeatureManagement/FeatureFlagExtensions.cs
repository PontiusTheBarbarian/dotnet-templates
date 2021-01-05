// <copyright file="FeatureFlagExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Company.WebApi.Modules.FeatureManagement
{
	/// <summary>
	/// Feature flag extensions.
	/// </summary>
	internal static class FeatureFlagExtensions
	{
		/// <summary>
		/// Add feature flags to project.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddFeatureFlags(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddFeatureManagement(configuration);

			return services;
		}
	}
}
