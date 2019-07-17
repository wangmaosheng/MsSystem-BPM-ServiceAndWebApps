using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Resilience.Http.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册ResilienceHTTPClient（采用Pollicy库实现）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="exceptionsAllowedBeforeBreaking">在发生了exceptionsAllowedBeforeBreaking次数时，熔断打开</param>
        /// <returns></returns>
        public static IServiceCollection AddResilienceHttpClient(this IServiceCollection services, string serviceName = null, int retryCount = 6, int exceptionsAllowedBeforeBreaking = 5)
        {

            services.AddSingleton<IResilienceHttpClientFactory, ResilienceHttpClientFactory>(sp => {
                var logger = sp.GetRequiredService<ILogger<ResilienceHttpClient>>();
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var httpFactory = sp.GetRequiredService<IHttpClientFactory>();
                Func<HttpClient> httpClientCreator = () => new HttpClient();
                if (!string.IsNullOrWhiteSpace(serviceName))
                {
                    httpClientCreator = () => httpFactory.CreateClient(serviceName);
                }

                return new ResilienceHttpClientFactory(logger, httpContextAccessor, retryCount, exceptionsAllowedBeforeBreaking, httpClientCreator);
            });
            services.AddSingleton<IHttpClient, ResilienceHttpClient>(sp => sp.GetService<IResilienceHttpClientFactory>().CreateResilienceHttpClient());

            return services;
        }
    }
}
