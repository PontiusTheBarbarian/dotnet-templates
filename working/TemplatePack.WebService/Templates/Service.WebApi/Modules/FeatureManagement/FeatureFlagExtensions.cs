using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Company.WebApi.Modules.FeatureManagement
{
    internal static class FeatureFlagExtensions
    {
        internal static IServiceCollection AddFeatureFlags(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddFeatureManagement(configuration);

            return services;
        }
    }
}
