using DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand;
using DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand;
using DilemmaApp.IdentitySvc.Application.Commands.RegisterUserCommand;
using DilemmaApp.Services.Common.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DilemmaApp.IdentitySvc.WebApi.Controllers
{
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("users/login")]
        public Response<LoginUserCommandResult> Login(LoginUserCommand command)
        {
            return _mediator.Send(command).Result;
        }

        [HttpPost]
        [Route("users/authenticate")]
        public Response<AuthenticateUserCommandResult> Authenticate(AuthenticateUserCommand command)
        {
            return _mediator.Send(command).Result;
        }
        
        [HttpPost]
        [Route("users/register")]
        public Response<RegisterUserCommandResult> Register([FromBody]RegisterUserCommand command)
        {
            return _mediator.Send(command).Result;
        }
    }
}