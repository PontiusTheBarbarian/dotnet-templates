using Microsoft.Extensions.Configuration;

namespace Company.WebApi.Modules.Configuration
{
    internal static class KeyVaultExtensions
    {
        internal static IConfigurationBuilder AddKeyVault(this IConfigurationBuilder configurationBuilder)
        {
            var root = configurationBuilder.Build();
            var vault = root["KeyVault:Vault"];
            if (!string.IsNullOrEmpty(vault))
            {
                configurationBuilder.AddAzureKeyVault(
                $"https://{root["KeyVault:Vault"]}.vault.azure.net/",
                root["KeyVault:ClientId"],
                root["KeyVault:ClientSecret"]);
            }

            return configurationBuilder;
        }
    }
}
