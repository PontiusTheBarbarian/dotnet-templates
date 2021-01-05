// <copyright file="HealthChecksExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;

namespace Company.WebApi.Modules.Health
{
	/// <summary>
	/// Health checks extensions.
	/// </summary>
	internal static class HealthChecksExtensions
	{
		/// <summary>
		/// Add custom healthchecks to project.
		/// </summary>
		/// <param name="services"></param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddCustomHealthChecks(
			this IServiceCollection services)
		{
			services.AddHealthChecks();

			return services;
		}

		/// <summary>
		/// Use custom health checks.
		/// </summary>
		/// <param name="app"></param>
		/// <returns><see cref="IApplicationBuilder"/>.</returns>
		internal static IApplicationBuilder UseHealthChecks(
			this IApplicationBuilder app)
			 => app.UseHealthChecks("/health", new HealthCheckOptions { ResponseWriter = WriteResponse });

		private static Task WriteResponse(HttpContext context, HealthReport result)
		{
			context.Response.ContentType = MediaTypeNames.Application.Json;

			JObject json = new JObject(
				new JProperty("status", result.Status.ToString()),
				new JProperty("results", new JObject(result.Entries.Select(pair =>
					new JProperty(pair.Key, new JObject(
						new JProperty("status", pair.Value.Status.ToString()),
						new JProperty("description", pair.Value.Description),
						new JProperty("data", new JObject(pair.Value.Data.Select(
							p => new JProperty(p.Key, p.Value))))))))));

			return context.Response.WriteAsync(
				json.ToString((Newtonsoft.Json.Formatting)Formatting.Indented));
		}
	}
}