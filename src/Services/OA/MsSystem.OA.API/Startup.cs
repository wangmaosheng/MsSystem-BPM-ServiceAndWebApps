using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using MsSystem.OA.API.Filters;
using MsSystem.OA.API.Hubs;
using MsSystem.OA.API.Infrastructure;
using MsSystem.OA.IRepository;
using MsSystem.OA.IService;
using MsSystem.OA.Repository;
using MsSystem.OA.Service;
using MsSystem.OA.ViewModel;
using NLog.Extensions.Logging;
using NLog.Web;
using Polly;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace MsSystem.OA.API
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

            services.Configure<AppSettings>(Configuration);
            IOptions<AppSettings> appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            services.AddCustomMvc(appSettings).AddHttpClientServices();
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
            app.UseSwagger(options=> 
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
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/messageHub", options => options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
                routes.MapHub<ChatHub>("/chatHub", options => options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransports.All);
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
            services.AddScoped<IOaDbContext, OaDbContext>();
            services.AddScoped<IOaDatabaseFixture, OaDatabaseFixture>();
            services.AddScoped<IWorkFlowService, WorkFlowService>();
            services.AddScoped<IOaLeaveService, OaLeaveService>();
            services.AddScoped<IOaMessageService, OaMessageService>();
            services.AddScoped<IOaChatService, OaChatService>();
            services.AddAutoMapper();

            services.AddMvc(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                .AddJsonOptions(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写

            services.AddSwaggerGen(options =>
            {
                string apiName = Assembly.GetExecutingAssembly().GetName().Name;
                options.SwaggerDoc(apiName, new Info { Title = "行政办公接口", Version = "v1" });
                var xmlFile = $"{apiName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.OperationFilter<AddAuthTokenHeaderParameter>();
            });

            services.AddSignalR();

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


            services.AddHttpClient<ISystemService, SystemService>()
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
