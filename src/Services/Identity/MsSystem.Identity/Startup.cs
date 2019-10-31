using JadeFramework.Zipkin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace MsSystem.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddZipkin(Configuration.GetSection(nameof(ZipkinOptions)));

            services.AddServiceRegistration();
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddProfileService<ProfileService>();


            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseZipkin();
            //loggerFactory.AddNLog();
            //if (env.IsDevelopment())
            //{
            //    env.ConfigureNLog("NLog.Development.config");
            //}
            //else
            //{
            //    env.ConfigureNLog("NLog.config");
            //}
            app.UseMvc();
            app.UseIdentityServer();
            app.UseServiceRegistration(new ServiceCheckOptions
            {
                HealthCheckUrl = "api/HealthCheck/Ping"
            });
        }
    }
}
