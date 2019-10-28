namespace Microsoft.AspNetCore.Builder
{
    using Consul;
    using JadeFramework.Core.Consul;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using System;

    /// <summary>
    /// Consul 中间件扩展
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="app"></param>
        /// <param name="checkOptions"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseServiceRegistration(this IApplicationBuilder app, ServiceCheckOptions checkOptions)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            var lifetime = app.ApplicationServices.GetService(typeof(IApplicationLifetime)) as IApplicationLifetime;

            var serviceOptions = app.ApplicationServices.GetService(typeof(IOptions<ServiceDiscoveryOptions>)) as IOptions<ServiceDiscoveryOptions>;
            var consul = app.ApplicationServices.GetService(typeof(IConsulClient)) as IConsulClient;

            lifetime.ApplicationStarted.Register(() =>
            {
                OnStart(app, serviceOptions.Value, consul, lifetime, checkOptions);
            });
            lifetime.ApplicationStopped.Register(() =>
            {
                OnStop(app, serviceOptions.Value, consul, lifetime);
            });

            return app;
        }
        private static void OnStop(IApplicationBuilder app, ServiceDiscoveryOptions serviceOptions, IConsulClient consul, IApplicationLifetime lifetime)
        {
            var serviceId = $"{serviceOptions.Service.Name}_{serviceOptions.Service.Address}:{serviceOptions.Service.Port}";

            consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();

        }

        private static void OnStart(IApplicationBuilder app, ServiceDiscoveryOptions serviceOptions, IConsulClient consul, IApplicationLifetime lifetime, ServiceCheckOptions checkOptions)
        {

            var serviceId = $"{serviceOptions.Service.Name}_{serviceOptions.Service.Address}:{serviceOptions.Service.Port}";
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(5),
                Interval = TimeSpan.FromSeconds(serviceOptions.Service.Interval),
                HTTP = $"http://{serviceOptions.Service.Address}:{serviceOptions.Service.Port}/{checkOptions.HealthCheckUrl}"
            };

            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                Address = serviceOptions.Service.Address,
                ID = serviceId,
                Name = serviceOptions.Service.Name,
                Port = serviceOptions.Service.Port
            };

            consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
        }
    }
}
