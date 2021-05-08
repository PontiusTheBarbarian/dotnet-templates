// <copyright file="FeatureFlagExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.FeatureManagement
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Feature flag extensions.
	/// </summary>
	internal static class FeatureFlagExtensions
	{
		/// <summary>
		/// Add feature flags to project.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <param name="configuration">Application configuration.</param>
		/// <returns><see cref="IServiceCollection"/>Service collection.</returns>
		internal static IServiceCollection AddFeatureFlags(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddFeatureManagement(configuration);

			return services;
		}
	}
}
