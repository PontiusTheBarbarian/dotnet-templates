// <copyright file="CustomCorsExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;

namespace Company.WebApi.Modules
{
	/// <summary>
	/// Custom CORS extensions.
	/// </summary>
	internal static class CustomCorsExtensions
	{
		/// <summary>
		/// Use custom CORS.
		/// </summary>
		/// <param name="app"></param>
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
