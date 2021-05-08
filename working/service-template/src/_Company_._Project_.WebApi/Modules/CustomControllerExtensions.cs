// <copyright file="CustomControllerExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>
#if (AuthenticationType == "AzureAD")
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
#endif
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Custom controller extensions.
	/// </summary>
	internal static class CustomControllerExtensions
	{
		/// <summary>
		/// Add custome controllers.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddCustomControllers(this IServiceCollection services)
		{
#if (AuthenticationType == "AzureAD")
			services.AddControllers(options
				=>
			{
				var policy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
				options.Filters.Add(new AuthorizeFilter(policy));

				// options.Filters.Add<AuditActionFilter>();
			});
#endif

#if (AuthenticationType == "AzureAD")
			services.AddControllers();
#endif
			return services;
		}
	}
}