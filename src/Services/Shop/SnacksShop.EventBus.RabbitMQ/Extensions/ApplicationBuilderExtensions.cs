using JadeFramework.EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SnacksShop.EventBus.RabbitMQ.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 注册订阅EventBus相关处理类
        /// </summary>
        /// <param name="app"></param>
        /// <param name="evtBus"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app, Action<IEventBus> evtBus)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            evtBus?.Invoke(eventBus);
            return app;
        }
    }
}
