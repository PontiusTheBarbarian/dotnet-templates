// <copyright file="KeyVaultExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;

namespace Company.WebApi.Modules.Configuration
{
	/// <summary>
	/// KeyVault extensions.
	/// </summary>
	internal static class KeyVaultExtensions
	{
		/// <summary>
		/// Add KeyVault to project.
		/// </summary>
		/// <param name="configurationBuilder"></param>
		/// <returns><see cref="IConfigurationBuilder"/>.</returns>
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
