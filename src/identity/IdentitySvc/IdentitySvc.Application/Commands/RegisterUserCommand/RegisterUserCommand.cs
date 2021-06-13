using System;
using DilemmaApp.Services.Common.Application;
using MediatR;

namespace DilemmaApp.IdentitySvc.Application.Commands.RegisterUserCommand
{
    public class RegisterUserCommand : IRequest<Response<RegisterUserCommandResult>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}