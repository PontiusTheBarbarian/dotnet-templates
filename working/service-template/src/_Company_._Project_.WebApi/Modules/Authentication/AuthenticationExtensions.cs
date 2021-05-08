// <copyright file="AuthenticationExtensions.cs" company="_Company_.">
// Copyright (c) _Company_ All rights reserved.
// </copyright>

#if (AuthenticationType == "BasicAuth")
using _Company_._Project_.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication;
#endif
#if (AuthenticationType == "AzureAD")
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if (AuthenticationType == "AzureAD")
using Microsoft.Identity.Web;
#endif

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.ExceptionHandling
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Authentication extensions.
	/// </summary>
	internal static class AuthenticationExtensions
	{
		/// <summary>
		/// Adds Authentication to the application.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <param name="configuration">Application configuration.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddAuthentication(
			this IServiceCollection services,
			IConfiguration configuration)
		{
#if (AuthenticationType == "BasicAuth")
			services.AddAuthentication("BasicAuthentication")
					.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationMiddleware>("BasicAuthentication", null);
#endif
#if (AuthenticationType == "AzureAD")
			services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
					.AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"));
#endif
			return services;
		}
	}
}
