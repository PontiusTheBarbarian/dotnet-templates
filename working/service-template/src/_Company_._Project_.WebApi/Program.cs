// <copyright file="Program.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using System.IO;
using _Company_._Project_.WebApi.Modules.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Entry point.
		/// </summary>
		/// <param name="args">Program arguments.</param>
		public static void Main(string[] args)
			=> CreateHostBuilder(args)
				.Build()
				.Run();

		/// <summary>
		/// Create host builder.
		/// </summary>
		/// <param name="args">Program arguments.</param>
		/// <returns><see cref="IHostBuilder"/>.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args)
			=> Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config)
				=> config
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddApplicationSettings()
					.AddKeyVault())
					.ConfigureWebHostDefaults(webBuilder => webBuilder
					.UseStartup<Startup>()
					.UseSerilog());
	}
}
