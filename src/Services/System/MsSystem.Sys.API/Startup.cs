using Autofac;
using Autofac.Extensions.DependencyInjection;
using JadeFramework.Zipkin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MsSystem.Sys.API.Filters;
using MsSystem.Sys.API.Infrastructure;
using MsSystem.Sys.IRepository;
using MsSystem.Sys.IService;
using MsSystem.Sys.Repository;
using MsSystem.Sys.Service;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
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
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
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
            services.AddScoped<ISysDatabaseFixture,SysDatabaseFixture>();

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



            services.AddMvc(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写

            services.AddSwaggerGen(options =>
            {
                string apiName = Assembly.GetExecutingAssembly().GetName().Name;
                options.SwaggerDoc(apiName, new Info { Title = "权限系统", Version = "v1" });
                var xmlFile = $"{apiName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<AddAuthTokenHeaderParameter>();
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

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseZipkin();

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

            app.UseMvc();
            //app.UseServiceRegistration(new ServiceCheckOptions
            //{
            //    HealthCheckUrl = "api/HealthCheck/Ping"
            //});
        }
    }
}
