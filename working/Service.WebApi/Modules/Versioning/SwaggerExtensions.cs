using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Company.WebApi.Modules.Versioning
{
    internal static class SwaggerExtensions
    {
        /// <summary>
        /// Configure the swagger homepage
        /// </summary>
        /// <param name="services">IServiceCollection services dependency injection container</param>
        /// <returns>IServiceCollection services</returns>
        internal static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
            => services.AddSwaggerGen(options
               =>
            {
                options.SwaggerDoc($"v{VersioningExtensions.GetAssemblyVersion().Major}", new OpenApiInfo
                {
                    Version = $"v{VersioningExtensions.GetAssemblyVersion().Major}",
                    Title = "Company.WebApi",
                    Description = "Simple WebApi",
                    Contact = new OpenApiContact()
                    {
                        Name = "Company.WebApi",
                        Email = ""
                    }
                });
#if (AuthenticationType == "BasicAuth")
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme()
                {
                    Name = "Authorisation",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Auth using header Bearer scheme"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                           {
                                {
                                    new OpenApiSecurityScheme()
                                    {
                                        Reference = new OpenApiReference()
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "basic"
                                        }
                                    },
                                    new string []{}
                                }
                           });
#endif
#if (AuthenticationType == "AzureAD")
                var tokenUrl = new Uri($"{configuration.GetValue<string>("AzureAD:Instance")}/{configuration.GetValue<string>("AzureAD:TenantId")}/oauth2/v2.0/token");
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            //TODO
                            TokenUrl = tokenUrl,
                            AuthorizationUrl = tokenUrl
                        }
                    }
                });
#endif
                options.EnableAnnotations();
            });

        /// <summary>
        /// User versioned swagger url
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        internal static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder app,
            IConfiguration configuration)
            => app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v{VersioningExtensions.GetAssemblyVersion().Major}/swagger.json", "Company.WebApi");
#if (AuthenticationType == "AzureAD")

                    c.OAuthClientId(configuration.GetValue<string>("AzureAD:ClientId"));
                    c.OAuthScopeSeparator(" ");
#endif
                });


    }
}
