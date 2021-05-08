// <copyright file="CustomCorsExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Custom CORS extensions.
	/// </summary>
	internal static class CustomCorsExtensions
	{
		/// <summary>
		/// Use custom CORS.
		/// </summary>
		/// <param name="app">.</param>
		/// <returns><see cref="IApplicationBuilder"/>.</returns>
		internal static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
		{
			app.UseCors(x => x
				   .AllowAnyOrigin()
				   .AllowAnyMethod()
				   .AllowAnyHeader());

			return app;
		}
	}
}
