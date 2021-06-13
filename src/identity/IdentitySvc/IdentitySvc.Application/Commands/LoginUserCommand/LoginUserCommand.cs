using DilemmaApp.Services.Common.Application;
using MediatR;

namespace DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand
{
    public class LoginUserCommand : IRequest<Response<LoginUserCommandResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}