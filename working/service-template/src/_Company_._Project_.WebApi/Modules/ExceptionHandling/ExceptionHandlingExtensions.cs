// <copyright file="ExceptionHandlingExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.ExceptionHandling
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Extensions related to exception handling.
	/// </summary>
	internal static class ExceptionHandlingExtensions
	{
		/// <summary>
		/// Add exception handling to the project.
		/// </summary>
		/// <param name="app">.</param>
		/// <param name="env">Current environment.</param>
		/// <returns><see cref="IApplicationBuilder"/>.</returns>
		internal static IApplicationBuilder UseExceptionHandling(
			this IApplicationBuilder app,
			IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			return app.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}
