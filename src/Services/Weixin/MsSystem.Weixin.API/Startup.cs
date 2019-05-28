using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MsSystem.Weixin.API.Filters;
using MsSystem.Weixin.API.Hubs;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Repository;
using MsSystem.Weixin.Service;
using NLog.Extensions.Logging;
using NLog.Web;
using System;

namespace MsSystem.Weixin.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddServiceRegistration();
            services.AddResponseCompression();

            services.AddHttpClient();

            #region Identity Config

            var identityConfig = Configuration.GetSection("Identity");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Authority = identityConfig["Authority"];
                opt.Audience = identityConfig["Audience"];
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
            });

            #endregion

            #region Service

            services.AddScoped<IWeixinDbContext, WeixinDbContext>();
            services.AddScoped<IWeixinDatabaseFixture, WeixinDatabaseFixture>();

            services.AddScoped<IWxAccountService, WxAccountService>();
            services.AddScoped<IWxRuleService, WxRuleService>();
            services.AddScoped<IWxMenuService, WxMenuService>();
            services.AddScoped<IWxUserService, WxUserService>();
            services.AddScoped<IWxMiniprogramUserService, WxMiniprogramUserService>();

            #endregion

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });
            services.AddSignalR();
            services.AddMvc(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写

            services.AddAutoMapper();

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                env.ConfigureNLog("NLog.Development.config");
            }
            else
            {
                env.ConfigureNLog("NLog.config");
            }
            app.UseCors("CorsPolicy");
            app.UseResponseCompression();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAuthentication();
            app.UseMvc();
            app.UseSignalR(routes =>
            {
                routes.MapHub<MiniProgramMessageHub>("/MiniProgramMessageHub", options =>
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
                routes.MapHub<ChatHub>("/ChatHub", options =>
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
            });

            app.UseServiceRegistration(new ServiceCheckOptions
            {
                HealthCheckUrl= "/api/HealthCheck/ping"
            });
        }
    }
}
