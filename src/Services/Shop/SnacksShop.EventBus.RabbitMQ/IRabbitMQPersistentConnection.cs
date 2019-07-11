using RabbitMQ.Client;
using System;

namespace SnacksShop.EventBus.RabbitMQ
{
    public interface IRabbitMQPersistentConnection:IDisposable
    {

        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
