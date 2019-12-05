using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace MsSystem.Gateway
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
            var identityConfig = Configuration.GetSection("Identity");
            services.AddAuthentication()
            .AddIdentityServerAuthentication("MsSystem", opt =>
            {
                opt.ApiName = identityConfig["ApiName"];
                opt.ApiSecret = identityConfig["ApiSecret"];
                opt.Authority = identityConfig["Authority"];
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });
            services.AddOcelot()
                //.AddConsul()
                .AddCacheManager(x => x.WithDictionaryHandle())
                .AddPolly();
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("MsSystem.Gateway", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "网关服务", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseSwagger();
            var apis = new List<string> { "MsSystem.OA.API", "MsSystem.WF.API", "MsSystem.Sys.API", "MsSystem.Weixin.API" };
            app.UseSwaggerUI(options =>
            {
                options.ShowExtensions();
                options.EnableValidator(null);
                apis.ForEach(m =>
                {
                    options.SwaggerEndpoint($"/{m}/swagger.json", m);
                });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOcelot().Wait();
        }
    }
}
