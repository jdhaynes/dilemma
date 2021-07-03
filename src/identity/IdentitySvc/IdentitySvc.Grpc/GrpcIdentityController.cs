using System;
using System.Threading.Tasks;
using DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand;
using DilemmaApp.Services.Common.Application;
using Grpc.Core;
using MediatR;

namespace IdentitySvc.Grpc
{
    public class GrpcIdentityController : Identity.IdentityBase
    {
        private IMediator _mediator;
        
        public GrpcIdentityController(IMediator mediatr)
        {
            _mediator = _mediator;
        }
        
        public override async Task<AuthenticateUserResponse> AuthenticateUser(
            AuthenticateUserRequest request, ServerCallContext context)
        {
            AuthenticateUserCommand command = new AuthenticateUserCommand()
            {
                Token = request.Token
            };

            Response<AuthenticateUserCommandResult> result = await _mediator.Send(command);

            return null;
        }
    }
}