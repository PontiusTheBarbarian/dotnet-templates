// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.IO;
using Company.WebApi.Modules.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Company.WebApi
{
	/// <summary>
	/// Program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Entry point.
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
			=> CreateHostBuilder(args)
				.Build()
				.Run();

		/// <summary>
		/// Create host builder.
		/// </summary>
		/// <param name="args"></param>
		/// <returns><see cref="IHostBuilder"/>.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args)
			=> Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config)
				=> config
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddApplicationSettings()
#if IncludeKeyVault
			.AddKeyVault()
#endif
			)
			.ConfigureWebHostDefaults(webBuilder
				=> webBuilder.UseStartup<Startup>()
					.UseSerilog());
	}
}
