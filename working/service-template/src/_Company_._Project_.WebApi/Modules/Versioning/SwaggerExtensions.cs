// <copyright file="SwaggerExtensions.cs" company="_Company_">
// Copyright (c) _Company_. All rights reserved.
// </copyright>
#if (AuthenticationType == "AzureAD")
using System;
#endif
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Versioning
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Swagger extensions.
	/// </summary>
	internal static class SwaggerExtensions
	{
		/// <summary>
		/// Adds Swagger to the project.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <param name="configuration">Application configuration.</param>
		/// <returns><see cref="IServiceCollection" />.</returns>
		internal static IServiceCollection AddSwagger(
			this IServiceCollection services,
			IConfiguration configuration)
			=> services.AddSwaggerGen(options
			   =>
			{
				options.SwaggerDoc($"v{VersioningExtensions.GetAssemblyVersion().Major}", new OpenApiInfo
				{
					Version = $"v{VersioningExtensions.GetAssemblyVersion().Major}",
					Title = "_Company_._Project_.WebApi",
					Description = "Simple WebApi",
					Contact = new OpenApiContact()
					{
						Name = "_Company_._Project_.WebApi",
						Email = string.Empty,
					},
				});
#if (AuthenticationType == "BasicAuth")
				options.AddSecurityDefinition("basic", new OpenApiSecurityScheme()
				{
					Name = "Authorisation",
					Type = SecuritySchemeType.Http,
					Scheme = "basic",
					In = ParameterLocation.Header,
					Description = "Basic Auth using header Bearer scheme",
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme()
						{
							Reference = new OpenApiReference()
							{
								Type = ReferenceType.SecurityScheme,
								Id = "basic",
							},
						},
						System.Array.Empty<string>()
					},
				});
#endif
#if (AuthenticationType == "AzureAD")
				var tokenUrl = new Uri($"{configuration.GetValue<string>("AzureAD:Instance")}/{configuration.GetValue<string>("AzureAD:TenantId")}/oauth2/v2.0/token");
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows()
					{
						Implicit = new OpenApiOAuthFlow()
						{
							// TODO
							TokenUrl = tokenUrl,
							AuthorizationUrl = tokenUrl,
						},
					},
				});
#endif
				options.EnableAnnotations();
			});

		/// <summary>
		/// Add versioned swagger.
		/// </summary>
		/// <param name="app">.</param>
		/// <param name="configuration">Current application environment.</param>
		/// <returns><see cref="IApplicationBuilder"/>.</returns>
		internal static IApplicationBuilder UseVersionedSwagger(
			this IApplicationBuilder app,
			IConfiguration configuration)
			=> app.UseSwagger()
				.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint($"/swagger/v{VersioningExtensions.GetAssemblyVersion().Major}/swagger.json", "_Company_._Project_.WebApi");
#if (AuthenticationType == "AzureAD")

					c.OAuthClientId(configuration.GetValue<string>("AzureAD:ClientId"));
					c.OAuthScopeSeparator(" ");
#endif
				});
	}
}
