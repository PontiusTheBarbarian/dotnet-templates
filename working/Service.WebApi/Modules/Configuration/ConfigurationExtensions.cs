using Microsoft.Extensions.Configuration;

namespace Company.WebApi.Modules.Configuration
{
    internal static class ConfigurationExtentions
    {
        internal static IConfigurationBuilder AddApplicationSettings(this IConfigurationBuilder configurationBuilder)
            => configurationBuilder
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
    }
}
