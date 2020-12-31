using Microsoft.AspNetCore.Builder;

namespace Company.WebApi.Modules
{
    internal static class CustomCorsExtensions
    {
        /// <summary>
        ///  Add Custom CORS configuration
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
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
