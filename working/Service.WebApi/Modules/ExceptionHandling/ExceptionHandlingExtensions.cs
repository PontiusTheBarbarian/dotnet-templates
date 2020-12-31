using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Company.WebApi.Modules.ExceptionHandling
{
    internal static class ExceptionHandlingExtensions
    {
		/// <summary>
		/// Registers the Api exception middleware in the request pipeline
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		internal static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app,
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
