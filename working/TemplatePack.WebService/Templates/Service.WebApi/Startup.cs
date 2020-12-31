#if (IncludeAuditing)
using Templates.WebApi.Modules.Auditing;
#endif
using Company.WebApi.Modules;
using Company.WebApi.Modules.ExceptionHandling;
using Company.WebApi.Modules.FeatureManagement;
using Company.WebApi.Modules.Logging;
using Company.WebApi.Modules.Versioning;
using Company.WebApi.Modules.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Company.WebApi
{
    public class Startup
    {
        public virtual IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
            => services
                .AddFeatureFlags(Configuration)
                .AddSerilog(Configuration)
                .AddCustomControllers()
                .AddAuthentication(Configuration)
                .AddCors()
                .AddInvalidRequestLogging()
                .AddCustomHealthChecks()
                .AddApplicationInsights(Configuration)
                .AddVersioning()
                .AddSwagger(Configuration)
#if(IncludeAuditing)
                .AddAuditing()
#endif
                .AddMvc()
                ;

        public virtual void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
            => app
               .UseSerilog(loggerFactory)
               .UseExceptionHandling(env)
               .UseVersionedSwagger(Configuration)
               .UseCustomCors()
               .UseHttpsRedirection()
               .UseRouting()
               .UseHealthChecks()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
                   endpoints.MapHealthChecks("/health");
               })
            ;
    }
}
