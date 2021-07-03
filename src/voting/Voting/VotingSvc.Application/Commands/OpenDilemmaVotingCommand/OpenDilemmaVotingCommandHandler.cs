using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application;
using MediatR;

namespace VotingSvc.Application.Commands.OpenDilemmaVotingCommand
{
    public class OpenDilemmaVotingCommandHandler : IRequestHandler<OpenDilemmaVotingCommand,
        Response<OpenDilemmaVotingCommandResult>>
    {
        public Task<Response<OpenDilemmaVotingCommandResult>> Handle(
            OpenDilemmaVotingCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}