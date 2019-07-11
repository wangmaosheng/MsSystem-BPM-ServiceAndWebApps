using System;
using System.Collections.Generic;
using System.Text;

namespace JadeFramework.EventBus.RabbitMQ.Extensions
{
    public class EventBusRabbitMQOptions
    {
        public EventBusRabbitMQOptions()
        {
            HostName = "localhost";
            EventBusRetryCount = 5;
        }
        /// <summary>
        /// RabbitMQ 主机名,默认为 localhost
        /// </summary>
        public string HostName { get; set; } 

        /// <summary>
        /// 重试次数，默认为5
        /// </summary>
        public int EventBusRetryCount { get; set; }
    }
}
