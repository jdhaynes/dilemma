using DilemmaApp.Services.Common.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotingSvc.Application.Commands.CastVoteCommand;

namespace VotingSvc.WebApi.Controllers
{
    [Controller]
    public class VoteController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("votes/cast")]
        public Response<Unit> CastVote([FromBody]CastVoteCommand command)
        {
            return  _mediator.Send(command).Result;
        }
    }
}