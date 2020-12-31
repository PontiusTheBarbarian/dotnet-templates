using System;
using Audit.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Templates.WebApi.Modules.Auditing
{
    internal static class AuditingExtensions
    {
        internal static IServiceCollection AddAuditing(this IServiceCollection services)
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
