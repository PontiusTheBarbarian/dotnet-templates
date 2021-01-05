// <copyright file="BasicAuthenticationMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Company.WebApi.Middleware
{
	/// <summary>
	/// Basic authentication middleware.
	/// </summary>
	internal class BasicAuthenticationMiddleware : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		private readonly IConfiguration configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicAuthenticationMiddleware"/> class.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="logger"></param>
		/// <param name="encoder"></param>
		/// <param name="clock"></param>
		/// <param name="configuration"></param>
		public BasicAuthenticationMiddleware(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock,
			IConfiguration configuration)
			: base(options, logger, encoder, clock)
		{
			this.configuration = configuration;
		}

#pragma warning disable 1998
		/// <inheritdoc/>
		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
#pragma warning restore 1998
		{
			var endpoint = this.Context.GetEndpoint();
			if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
			{
				return AuthenticateResult.NoResult();
			}

			if (!this.Request.Headers.ContainsKey("Authorization"))
			{
				return AuthenticateResult.Fail("Missing Authorization Header");
			}

			try
			{
				var authHeader = AuthenticationHeaderValue.Parse(this.Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
				var username = credentials[0];
				var password = credentials[1];
				if (!this.IsAuthenticated(username, password))
				{
					return AuthenticateResult.Fail("Invalid Authorization Header");
				}

				var claims = new[]
				{
					new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
					new Claim(ClaimTypes.Name, username),
				};
				var identity = new ClaimsIdentity(claims, this.Scheme.Name);
				var principal = new ClaimsPrincipal(identity);
				var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

				return AuthenticateResult.Success(ticket);
			}
			catch
			{
				return AuthenticateResult.Fail("Invalid Authorization Header");
			}
		}

		private bool IsAuthenticated(string username, string password)
			=> username == this.configuration.GetValue<string>("BasicAuth:UserName")
				&& password == this.configuration.GetValue<string>("BasicAuth:Password");
	}
}