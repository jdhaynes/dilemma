using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Common.Application.Messaging;
using MediatR;
using VotingSvc.Application.Commands.CastVoteCommand.DTOs;
using VotingSvc.Application.IntegrationEvents.Outbound;

namespace VotingSvc.Application.Commands.CastVoteCommand
{
    public class CastVoteCommandHandler : IRequestHandler<CastVoteCommand, Response<Unit>>
    {
        private IMessageBus _messageBus;

        public CastVoteCommandHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public Task<Response<Unit>> Handle(CastVoteCommand request,
            CancellationToken cancellationToken)
        {
            VoteCastEvent @event = new VoteCastEvent()
            {
                DilemmaId = request.DilemmaId,
                OptionId = request.OptionId,
                VoterId = request.UserId,
                VotedAt = DateTime.Now
            };

            _messageBus.PublishIntegrationEvent(@event);

            Response<Unit> response = new Response<Unit>();
            response.State = ResponseState.Ok;

            return Task.FromResult(response);
        }
    }
}