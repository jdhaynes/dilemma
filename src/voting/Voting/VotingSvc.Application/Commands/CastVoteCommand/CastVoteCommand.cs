using System;
using DilemmaApp.Services.Common.Application;
using MediatR;
using VotingSvc.Application.Commands.CastVoteCommand.DTOs;

namespace VotingSvc.Application.Commands.CastVoteCommand
{
    public class CastVoteCommand : IRequest<Response<Dilemma>>
    {
        public Guid UserId { get; set; }
        public Guid DilemmaId { get; set; }
        public Guid? OptionId { get; set; }
    }
}