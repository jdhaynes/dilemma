using System.Threading.Tasks;

namespace DilemmaApp.Services.Common.Application.Messaging
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}