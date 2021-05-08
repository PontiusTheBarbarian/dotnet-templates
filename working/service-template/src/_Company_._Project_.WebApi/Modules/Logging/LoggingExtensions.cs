// <copyright file="LoggingExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Logging
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Logging extensions.
	/// </summary>
	internal static class LoggingExtensions
	{
		/// <summary>
		/// Adds invalid request logging to the pipeline.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddInvalidRequestLogging(
			this IServiceCollection services)
			=> services.Configure<ApiBehaviorOptions>(o =>
				 {
					 o.InvalidModelStateResponseFactory = actionContext =>
					 {
						 List<string> errors = actionContext.ModelState
							 .Values
							 .SelectMany(x => x.Errors)
							 .Select(x => x.ErrorMessage)
							 .ToList();

						 string jsonModelState = JsonSerializer.Serialize(errors);
						 Log.Logger.Warning(jsonModelState);

						 ValidationProblemDetails problemDetails = new ValidationProblemDetails(actionContext.ModelState);
						 return new BadRequestObjectResult(problemDetails);
					 };
				 });
	}
}
