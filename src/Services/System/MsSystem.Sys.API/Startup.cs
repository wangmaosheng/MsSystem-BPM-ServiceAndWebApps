using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MsSystem.Sys.API.Filters;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Repository;
using MsSystem.Sys.Service;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Reflection;

namespace MsSystem.Sys.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.MySQL(Configuration["LogConfig:MySQL"], tableName: "syslog")
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

            //services.AddZipkin(Configuration.GetSection(nameof(ZipkinOptions)));

            //services.AddServiceRegistration();

            services.AddResponseCompression();

            #region Identity Config

            var identityConfig = Configuration.GetSection("Identity");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Authority = identityConfig["Authority"];
                opt.Audience = identityConfig["Audience"];
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                //opt.MetadataAddress = identityConfig["Authority"] + "/.well-known/openid-configuration";
            });

            #endregion


            #region BLL

            services.AddScoped<ISysDbContext, SysDbContext>();
            services.AddScoped<ISysLogDbContext, SysLogDbContext>();
            services.AddScoped<ISysDatabaseFixture, SysDatabaseFixture>();

            services.AddScoped<ILogJobs, LogJobs>();
            services.AddScoped<ISysLogService, SysLogService>();
            services.AddScoped<ISysReleaseLogService, SysReleaseLogService>();
            services.AddScoped<ISysResourceService, SysResourceService>();
            services.AddScoped<ISysRoleService, SysRoleService>();
            services.AddScoped<ISysUserService, SysUserService>();
            services.AddScoped<ISysSystemService, SysSystemService>();
            services.AddScoped<ISysDeptService, SysDeptService>();

            services.AddScoped<IWorkFlowService, WorkFlowService>();
            services.AddScoped<ICodeBuilderService, CodeBuilderService>();
            services.AddScoped<ISysScheduleService, SysScheduleService>();

            #endregion

            services.AddControllers(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver())//修改默认首字母为大写
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(options =>
            {
                string apiName = Assembly.GetExecutingAssembly().GetName().Name;
                options.SwaggerDoc(apiName, new OpenApiInfo { Title = "权限系统", Version = "v1" });
                var xmlFile = $"{apiName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                //options.OperationFilter<AddAuthTokenHeaderParameter>();
            });

            services.AddAuthorization();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseServiceRegistration(new ServiceCheckOptions
            //{
            //    HealthCheckUrl = "api/HealthCheck/Ping"
            //});
        }
    }
}
