using JadeFramework.Zipkin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Resilience.Http.DependencyInjection.Extensions;
using zipkin4net.Transport.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册分布式追踪埋点配置 zipkin
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddZipkin(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ZipkinOptions>(configuration);

            //var zipkinOptions = new ZipkinOptions();
            var zipkinOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ZipkinOptions>>().Value;

            services.AddHttpClient(zipkinOptions.HttpClientServiceName)
                    .AddHttpMessageHandler(sp => TracingHandler.WithoutInnerHandler(zipkinOptions.ApplicationName));

            services.AddResilienceHttpClient(zipkinOptions.HttpClientServiceName);

            return services;
        }
    }
}
