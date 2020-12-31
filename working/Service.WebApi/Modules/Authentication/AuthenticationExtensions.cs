#if (AuthenticationType == "BasicAuth")
using Company.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication;
#endif
#if (AuthenticationType == "AzureAD")
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApi.Modules.ExceptionHandling
{
    internal static class AuthenticationExtensions
    {
        /// <summary>
        ///  Add Authentication
        /// </summary>
        /// <param name="services">IServiceCollection services dependency injection container</param>
        /// <returns>IServiceCollection services</returns>
        internal static IServiceCollection AddAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
#if (AuthenticationType == "BasicAuth")
            services.AddAuthentication("BasicAuthentication")
              .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationMiddleware>("BasicAuthentication", null);
#endif
#if (AuthenticationType == "AzureAD")
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"));
#endif
            return services;
        }
    }
}
