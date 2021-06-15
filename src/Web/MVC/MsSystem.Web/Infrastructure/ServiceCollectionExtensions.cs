﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace MsSystem.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
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
            services.AddHttpClient<ISysDeptService, SysDeptService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Sample. Default lifetime is 2 minutes
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<ISysLogService, SysLogService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<ISysReleaseLogService, SysReleaseLogService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<ISysResourceService, SysResourceService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<ISysRoleService, SysRoleService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<ISysSystemService, SysSystemService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<ISysUserService, SysUserService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<ICodeBuilderService, CodeBuilderService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());


            services.AddHttpClient<IScheduleService, ScheduleService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            //services.AddScoped<IScanningLoginService, ScanningLoginService>();

            services.AddHttpClient<IScanningLoginService, ScanningLoginService>()
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }
        public static IServiceCollection AddOaHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient<IOaLeaveService, OaLeaveService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IOaMessageService, OaMessageService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IOaChatService, OaChatService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy());
            return services;
        }
        public static IServiceCollection AddWeixinHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAccountService, AccountService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IRuleService, RuleService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IWxMenuService, WxMenuService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            return services;
        }
        public static IServiceCollection AddWfHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient<IConfigService, ConfigService>()
                    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IFormService, FormService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IWorkflowCategoryService, WorkflowCategoryService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IWorkFlowInstanceService, WorkFlowInstanceService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
            services.AddHttpClient<IWorkFlowService, WorkFlowService>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                   .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                   .AddPolicyHandler(GetRetryPolicy())
                   .AddPolicyHandler(GetCircuitBreakerPolicy());
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
