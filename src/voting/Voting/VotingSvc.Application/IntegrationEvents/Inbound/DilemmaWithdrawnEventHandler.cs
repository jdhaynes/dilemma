using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application.Messaging;

namespace VotingSvc.Application.IntegrationEvents.Inbound
{
    public class DilemmaWithdrawnEventHandler : IIntegrationEventHandler<DilemmaWithdrawnEvent>
    {
        public Task Handle(DilemmaWithdrawnEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}