// <copyright file="CustomControllerExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
#if (AuthenticationType == "AzureAD")
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
#endif

namespace Company.WebApi.Modules
{
	/// <summary>
	/// Custom controller extensions.
	/// </summary>
	internal static class CustomControllerExtensions
	{
		/// <summary>
		/// Add custome controllers.
		/// </summary>
		/// <param name="services"></param>
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
#if (IncludeAuditing)
						options.Filters.Add<AuditActionFilter>();
#endif
			});
#endif
#if (AuthenticationType != "AzureAD")
            services.AddControllers();
#endif
			return services;
		}
	}
}