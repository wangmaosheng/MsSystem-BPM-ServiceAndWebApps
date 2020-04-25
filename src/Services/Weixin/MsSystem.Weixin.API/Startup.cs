using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MsSystem.Weixin.API.Filters;
using MsSystem.Weixin.API.Hubs;
using MsSystem.Weixin.IRepository;
using MsSystem.Weixin.IService;
using MsSystem.Weixin.Repository;
using MsSystem.Weixin.Service;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Reflection;

namespace MsSystem.Weixin.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.MySQL(Configuration["LogConfig:MySQL"], tableName: "weixinlog")
            //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["LogConfig:ElasticsearchUri"]))
            //    {
            //        AutoRegisterTemplate = true,
            //    })
            //.CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddServiceRegistration();

            //services.AddZipkin(Configuration.GetSection(nameof(ZipkinOptions)));

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
            services.AddControllers(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写

            services.AddSwaggerGen(options =>
            {
                string apiName = Assembly.GetExecutingAssembly().GetName().Name;
                options.SwaggerDoc(apiName, new OpenApiInfo { Title = "微信接口", Version = "v1" });
                var xmlFile = $"{apiName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseZipkin();
            //loggerFactory.AddSerilog();
            app.UseCors("CorsPolicy");
            app.UseResponseCompression();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseStaticFiles();
            string apiName = Assembly.GetExecutingAssembly().GetName().Name;
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "{documentName}/swagger.json";
            })
            .UseSwaggerUI(options =>
            {
                options.ShowExtensions();
                options.EnableValidator(null);
                options.SwaggerEndpoint($"/{apiName}/swagger.json", $"{apiName} V1");
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
                routes.MapHub<MiniProgramMessageHub>("/MiniProgramMessageHub", options =>
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
                routes.MapHub<ChatHub>("/ChatHub", options =>
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
            });
            //app.UseServiceRegistration(new ServiceCheckOptions
            //{
            //    HealthCheckUrl = "api/HealthCheck/Ping"
            //});
        }
    }
}
