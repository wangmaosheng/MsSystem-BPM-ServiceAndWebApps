using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JadeFramework.EventBus.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDynamicIntegrationEventHandler
    {
        /// <summary>
        /// 处理EventBus消息
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        Task Handle(dynamic eventData);
    }
}
