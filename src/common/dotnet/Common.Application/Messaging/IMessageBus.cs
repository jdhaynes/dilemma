namespace DilemmaApp.Services.Common.Application.Messaging
{
    public interface IMessageBus
    {
        void PublishIntegrationEvent(IntegrationEvent @event);

        void Subscribe<TEvent, THandler>()
            where TEvent : IntegrationEvent
            where THandler : IIntegrationEventHandler<TEvent>;
    }
}