// <copyright file="ExceptionHandlingMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Company.WebApi.Modules.ExceptionHandling
{
	/// <summary>
	/// Exception handling middleware.
	/// </summary>
	internal class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate next;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
		/// </summary>
		/// <param name="next"></param>
		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		/// <summary>
		/// Trys to invoke the next item in the request pipeline.
		/// </summary>
		/// <param name="context">http context.</param>
		/// <returns><see cref="Task"/>.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await this.next(context);
			}
			catch (Exception ex)
			{
				await this.HandleExceptionAsync(context, ex);
			}
		}

#pragma warning disable CS1998
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
#pragma warning restore CS1998
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			Log.Error(exception, "An exception was caught in the API request pipeline");
		}
	}
}
