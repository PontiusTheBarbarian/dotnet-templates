using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Company.WebApi.Modules.ExceptionHandling
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
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
