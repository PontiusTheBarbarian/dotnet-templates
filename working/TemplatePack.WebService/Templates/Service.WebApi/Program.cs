using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Company.WebApi.Modules.Configuration;
using Serilog;

namespace Company.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config)
                => config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddApplicationSettings()
#if (IncludeKeyVault)
			.AddKeyVault()
#endif
            )
            .ConfigureWebHostDefaults(webBuilder
                => webBuilder.UseStartup<Startup>()
                    .UseSerilog()
            );
    }
}
