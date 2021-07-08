using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application.Messaging;
using VotingSvc.Application.IntegrationEvents.Outbound;

namespace VotingSvc.Application.IntegrationEvents.Inbound
{
    public class VoteCastEventHandler : IIntegrationEventHandler<VoteCastEvent>
    {
        public Task Handle(VoteCastEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}