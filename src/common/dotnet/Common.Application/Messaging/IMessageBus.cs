using System;

namespace DilemmaApp.Services.Common.Application.Messaging
{
    public interface IMessageBus : IDisposable
    {
        void PublishIntegrationEvent(IntegrationEvent @event);

        void Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;
    }
}