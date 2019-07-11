using JadeFramework.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace JadeFramework.EventBus.RabbitMQ.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册EventBus依赖项
        /// publish时不需要调用 UseEventBus扩展方法注册订阅
        /// subscribute时调用 UseEventBus扩展方法注册定义
        /// 注意：由于内部调用CreateScope方法，故建议最后注册此依赖
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, Action<EventBusRabbitMQOptions> configureOptions = null)
        {
            var options = new EventBusRabbitMQOptions();
            configureOptions?.Invoke(options);

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {

                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = options.HostName
                }; 
                return new DefaultRabbitMQPersistentConnection(factory, logger, options.EventBusRetryCount);
            });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.CreateScope(); //sp.GetRequiredService<IServiceScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, options.EventBusRetryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

    }
}
