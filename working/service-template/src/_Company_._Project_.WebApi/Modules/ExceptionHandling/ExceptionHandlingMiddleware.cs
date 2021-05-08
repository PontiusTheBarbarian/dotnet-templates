// <copyright file="ExceptionHandlingMiddleware.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.ExceptionHandling
#pragma warning restore SA1300 // Element should begin with upper-case letter
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
		/// <param name="next">.</param>
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
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
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
