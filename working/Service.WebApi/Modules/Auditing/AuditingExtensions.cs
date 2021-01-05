// <copyright file="AuditingExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using Audit.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Templates.WebApi.Modules.Auditing
{
	/// <summary>
	/// Auditing extensions.
	/// </summary>
	internal static class AuditingExtensions
	{
		/// <summary>
		/// Add Auditing to application.
		/// </summary>
		/// <param name="services">Service collection.</param>
		/// <returns><see cref="IServiceCollection"/>.</returns>
		internal static IServiceCollection AddAuditing(
			this IServiceCollection services)
		{
			services.AddSingleton<IAuditScopeFactory, AuditScopeFactory>();

			Configure();

			return services;
		}

		private static void Configure()
		{
			Configuration.Setup()
				.UseFileLogProvider(config => config
					.DirectoryBuilder(_ => $@"audit\{DateTime.Now:yyyy-MM-dd}")
					.FilenameBuilder(auditEvent => $"{auditEvent.Environment.UserName}_{DateTime.Now.Ticks}.json"));
		}
	}
}
