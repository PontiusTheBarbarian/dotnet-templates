// <copyright file="ExceptionHandlingExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Company.WebApi.Modules.ExceptionHandling
{
	internal static class ExceptionHandlingExtensions
	{
		/// <summary>
		/// Add exception handling to the project.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
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
