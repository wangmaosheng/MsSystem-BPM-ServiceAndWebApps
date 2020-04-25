using AutoMapper;
using JadeFramework.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MsSystem.WF.API.Filters;
using MsSystem.WF.API.Infrastructure;
using MsSystem.WF.IRepository;
using MsSystem.WF.IService;
using MsSystem.WF.Repository;
using MsSystem.WF.Service;
using MsSystem.WF.ViewModel;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace MsSystem.WF.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.MySQL(Configuration["LogConfig:MySQL"], tableName: "wflog")
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

            services.Configure<AppSettings>(Configuration);
            IOptions<AppSettings> appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            services.AddCustomMvc(appSettings).AddHttpClientServices();

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
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IOptions<AppSettings> appSettings)
        {
            //缓存
            services.AddMemoryCache();
            services.AddScoped<ICachingProvider, MemoryCachingProvider>();
            //services.AddServiceRegistration();
            services.AddResponseCompression();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Authority = appSettings.Value.Identity.Authority;
                opt.Audience = appSettings.Value.Identity.Audience;
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
            });


            services.AddScoped<IConfigService, ConfigService>();

            services.AddScoped<IWFDbContext, WFDbContext>();
            services.AddScoped<IWFDatabaseFixture, WFDatabaseFixture>();

            services.AddScoped<IWorkFlowService, WorkFlowService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IWorkFlowInstanceService, WorkFlowInstanceService>();
            services.AddScoped<IWorkflowCategoryService, WorkflowCategoryService>();

            services.AddScoped<ICachingProvider, MemoryCachingProvider>();
            services.AddAutoMapper();

            services.AddControllers(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写

            services.AddSwaggerGen(options =>
            {
                string apiName = Assembly.GetExecutingAssembly().GetName().Name;
                options.SwaggerDoc(apiName, new OpenApiInfo { Title = "工作流接口", Version = "v1" });
                var xmlFile = $"{apiName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                //options.OperationFilter<AddAuthTokenHeaderParameter>();
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

            services.AddCap(x =>
            {
                x.UseRabbitMQ(opt =>
                {
                    opt.HostName = appSettings.Value.RabbitMQ.HostName;
                    opt.UserName = appSettings.Value.RabbitMQ.UserName;
                    opt.Password = appSettings.Value.RabbitMQ.Password;
                });
                x.UseMySql(appSettings.Value.MySQL.Connection);
            });
            return services;
        }
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient<TokenClient>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<HttpClientRequestIdDelegatingHandler>();

            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));


            services.AddHttpClient<IConfigService, ConfigService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
              .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }
        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
