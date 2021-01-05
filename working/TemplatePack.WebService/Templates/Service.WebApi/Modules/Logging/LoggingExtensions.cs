// <copyright file="LoggingExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Company.WebApi.Modules.Logging
{
	/// <summary>
	/// Logging extensions.
	/// </summary>
	internal static class LoggingExtensions
	{
		/// <summary>
		/// Adds invalid request logging to the pipeline.
		/// </summary>
		/// <param name="services"></param>
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
