using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using MsSystem.Web.Areas.OA.Service;
using MsSystem.Web.Areas.Sys.Service;
using MsSystem.Web.Areas.Weixin.Service;
using MsSystem.Web.Areas.WF.Service;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace MsSystem.Web.Infrastructure
{
    public interface IAutoDIPolicyHttpClient
    { }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoDIPolicyHttpClient(this IServiceCollection services)
        {
            string path = AppContext.BaseDirectory;
            var referencedAssemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            var types = referencedAssemblies.SelectMany(a => a.DefinedTypes).Select(type => type.AsType())
                .Where(x => x != typeof(IAutoDIPolicyHttpClient) && typeof(IAutoDIPolicyHttpClient).IsAssignableFrom(x)).ToArray();

            var implementTypes = types.Where(x => x.IsClass).ToArray();
            var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            foreach (var implementType in implementTypes)
            {
                Type interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                {
                    //TODO ERROR PARAMS!!!20210610
                    //services.AddPolicyHttpClient<interfaceType, implementType >();
                    services.AddScoped(interfaceType, implementType);
                }
            }

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<WebEncoderOptions>(options => options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs));

            services.AddControllersWithViews(option => option.Filters.Add(typeof(HttpGlobalExceptionFilter)))
                //.AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(op => op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());//修改默认首字母为大写
            services.AddMemoryCache();

            string redisHost = configuration.GetSection("RedisConfig").GetSection("Redis_Default").GetValue<string>("Connection");
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisHost;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2); //session活期时间
                options.Cookie.HttpOnly = true;//设为httponly
            });
            //services.AddSession();
            return services;
        }

        public static IHttpClientBuilder AddPolicyHttpClient<TClient, TImplementation>(this IServiceCollection services)
            where TClient : class
            where TImplementation : class, TClient
        {
            return services.AddHttpClient<TClient, TImplementation>()
               .SetHandlerLifetime(TimeSpan.FromMinutes(5))
               .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
               .AddPolicyHandler(GetRetryPolicy())
               .AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        public static IServiceCollection AddSysHttpClientServices(this IServiceCollection services)
        {
            //add http client services

            services.AddPolicyHttpClient<ISysDeptService, SysDeptService>();
            services.AddPolicyHttpClient<ISysLogService, SysLogService>();
            services.AddPolicyHttpClient<ISysReleaseLogService, SysReleaseLogService>();
            services.AddPolicyHttpClient<ISysResourceService, SysResourceService>();
            services.AddPolicyHttpClient<ISysRoleService, SysRoleService>();
            services.AddPolicyHttpClient<ISysSystemService, SysSystemService>();
            services.AddPolicyHttpClient<ISysUserService, SysUserService>();
            services.AddPolicyHttpClient<ICodeBuilderService, CodeBuilderService>();
            services.AddPolicyHttpClient<IScheduleService, ScheduleService>();
            services.AddPolicyHttpClient<IScanningLoginService, ScanningLoginService>();

            return services;
        }
        public static IServiceCollection AddOaHttpClientServices(this IServiceCollection services)
        {
            services.AddPolicyHttpClient<IOaLeaveService, OaLeaveService>();
            services.AddPolicyHttpClient<IOaMessageService, OaMessageService>();
            services.AddPolicyHttpClient<IOaChatService, OaChatService>();
            return services;
        }
        public static IServiceCollection AddWeixinHttpClientServices(this IServiceCollection services)
        {
            services.AddPolicyHttpClient<IAccountService, AccountService>();
            services.AddPolicyHttpClient<IRuleService, RuleService>();
            services.AddPolicyHttpClient<IWxMenuService, WxMenuService>();
            return services;
        }
        public static IServiceCollection AddWfHttpClientServices(this IServiceCollection services)
        {
            services.AddPolicyHttpClient<IConfigService, ConfigService>();
            services.AddPolicyHttpClient<IFormService, FormService>();
            services.AddPolicyHttpClient<IWorkflowCategoryService, WorkflowCategoryService>();
            services.AddPolicyHttpClient<IWorkFlowInstanceService, WorkFlowInstanceService>();
            services.AddPolicyHttpClient<IWorkFlowService, WorkFlowService>();
            return services;
        }
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("MsApplication");
            services.Configure<TokenClientOptions>(options =>
            {
                options.Address = config["url"] + config["tokenurl"];
                options.ClientId = config["client_id"];
                options.ClientSecret = config["client_secret"];
                options.GrantType = config["grant_type"];
            });

            services.AddHttpClient<TokenClient>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //register delegating handlers
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddTransient<HttpClientRequestIdDelegatingHandler>();

            //set 5 min as the lifetime for each HttpMessageHandler int the pool
            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = new PathString("/Sys/User/Login");
                options.AccessDeniedPath = new PathString("/Error/NoAuth");
                options.LogoutPath = new PathString("/Sys/User/LogOut");
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
            });

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
