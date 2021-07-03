using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application.Messaging;
using MediatR;
using VotingSvc.Application.Commands.OpenDilemmaVotingCommand;

namespace VotingSvc.Application.IntegrationEvents.Inbound
{
    public class DilemmaPostedEventHandler : IIntegrationEventHandler<DilemmaPostedEvent>
    {
        private IMediator _mediator;

        public DilemmaPostedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Handle(DilemmaPostedEvent @event)
        {
            OpenDilemmaVotingCommand command = new OpenDilemmaVotingCommand()
            {
                DilemmaId = @event.DilemmaId,
                OptionIds = @event.OptionIds,
                PosterId = @event.PosterId
            };

            return _mediator.Send(command);
        }
    }
}