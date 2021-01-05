// <copyright file="AuthenticationExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#if (AuthenticationType == "BasicAuth")
using Company.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication;
#endif
#if (AuthenticationType == "AzureAD")
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApi.Modules.ExceptionHandling
{
	/// <summary>
	/// Authentication extensions.
	/// </summary>
	internal static class AuthenticationExtensions
	{
		/// <summary>
		/// Adds Authentication to the application.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddAuthentication(
			this IServiceCollection services,
			IConfiguration configuration)
		{
#if (AuthenticationType == "BasicAuth")
			services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationMiddleware>("BasicAuthentication", null);
#endif
#if (AuthenticationType == "AzureAD")
			services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"));
#endif
			return services;
		}
	}
}
