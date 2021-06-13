namespace DilemmaApp.Services.Common.Application.Messaging
{
    public interface IMessageBus
    {
        void PublishIntegrationEvent(IntegrationEvent @event);
    }
}