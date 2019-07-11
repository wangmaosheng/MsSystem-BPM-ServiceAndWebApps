using JadeFramework.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace JadeFramework.EventBus.Abstractions
{
    public interface IEventBus
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="event"></param>
        void Publish(IntegrationEvent @event);

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T">消息泛型类</typeparam>
        /// <typeparam name="TH">处理泛型类</typeparam>
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        /// <summary>
        /// 取消订阅消息
        /// </summary>
        /// <typeparam name="T">消息泛型类</typeparam>
        /// <typeparam name="TH">处理泛型类</typeparam>
        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;


        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="TH">采用动态Dynamic类型</typeparam>
        /// <param name="eventName"></param>
        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TH">采用动态Dynamic类型</typeparam>
        /// <param name="eventName"></param>
        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        
    }
}
