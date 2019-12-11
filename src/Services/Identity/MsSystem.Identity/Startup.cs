using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

            //services.AddZipkin(Configuration.GetSection(nameof(ZipkinOptions)));

            //services.AddServiceRegistration();
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddProfileService<ProfileService>();


            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app)
        {
            //app.UseZipkin();
            app.UseRouting();
            app.UseIdentityServer();
            //app.UseServiceRegistration(new ServiceCheckOptions
            //{
            //    HealthCheckUrl = "api/HealthCheck/Ping"
            //});
        }
    }
}
