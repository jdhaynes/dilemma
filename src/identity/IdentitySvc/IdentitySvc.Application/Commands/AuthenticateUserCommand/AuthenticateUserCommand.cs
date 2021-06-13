using DilemmaApp.Services.Common.Application;
using MediatR;

namespace DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand
{
    public class AuthenticateUserCommand : IRequest<Response<AuthenticateUserCommandResult>>
    {
        public string Token { get; set; }
    }
}