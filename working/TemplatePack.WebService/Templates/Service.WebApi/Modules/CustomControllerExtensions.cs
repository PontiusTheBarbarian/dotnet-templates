using Microsoft.Extensions.DependencyInjection;
#if (AuthenticationType == "AzureAD")
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
#endif
namespace Company.WebApi.Modules
{
    internal static class CustomControllerExtensions
    {
        /// <summary>
        /// Add custom controller configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
#if (AuthenticationType == "AzureAD")
            services.AddControllers(options
                =>  {
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                        options.Filters.Add(new AuthorizeFilter(policy));
    #if (IncludeAuditing)
						options.Filters.Add<AuditActionFilter>();
    #endif
                    });
#endif
#if (AuthenticationType != "AzureAD")
            services.AddControllers();
#endif
            return services;
        }
    }
}