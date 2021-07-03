using System;
using RabbitMQ.Client;

namespace DilemmaApp.Common.Infrastructure.RabbitMqMessageBus
{
    public interface IPersistantRabbitMqConnection : IDisposable
    {
        bool IsConnected { get; }
        void Connect();
        IModel OpenChannel();

    }
}