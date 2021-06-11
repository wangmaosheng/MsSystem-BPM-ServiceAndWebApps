using JadeFramework.Cache;
using JadeFramework.Core.Domain.Container;
using JadeFramework.Core.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MsSystem.Utility;
using MsSystem.Web.Areas.Sys.Hubs;
using MsSystem.Web.Areas.Sys.Service;
using MsSystem.Web.Infrastructure;

namespace MsSystem.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                .AddHttpClientServices(Configuration)
                .AddSysHttpClientServices()
                .AddOaHttpClientServices()
                .AddWeixinHttpClientServices()
                .AddWfHttpClientServices()
                .AddCustomAuthentication();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });
            services.AddSignalR().AddNewtonsoftJsonProtocol();
            //缓存
            services.AddScoped<ICachingProvider, MemoryCachingProvider>();
            //验证码
            services.AddScoped<IVerificationCode, VerificationCode>();
            services.AddScoped<IPermissionStorageContainer, PermissionStorageService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddSerilog();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute(
                    name: "TurntableRoute",
                    pattern: "{area:exists}/{controller=Activity}/{action=Turntable}/{id}.html");

                routes.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseEndpoints(routes =>
            {
                routes.MapHub<ScanningLoginHub>("/scanningLoginHub", options => options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
            });
        }

    }
}
