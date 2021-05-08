// <copyright file="KeyVaultExtensions.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Modules.Configuration
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// KeyVault extensions.
	/// </summary>
	internal static class KeyVaultExtensions
	{
		/// <summary>
		/// Add KeyVault to project.
		/// </summary>
		/// <param name="configurationBuilder">.</param>
		/// <returns><see cref="IConfigurationBuilder"/>Returns the configuration builder.</returns>
		internal static IConfigurationBuilder AddKeyVault(
			this IConfigurationBuilder configurationBuilder)
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
