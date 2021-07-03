using System;
using System.Collections.Generic;
using DilemmaApp.Services.Common.Application;
using MediatR;

namespace VotingSvc.Application.Commands.OpenDilemmaVotingCommand
{
    public class OpenDilemmaVotingCommand : IRequest<Response<OpenDilemmaVotingCommandResult>>
    {
        public Guid DilemmaId { get; set; }
        public Guid PosterId { get; set; }
        public List<Guid> OptionIds { get; set; }
    }
}