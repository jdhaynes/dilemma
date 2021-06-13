using System;

namespace DilemmaApp.IdentitySvc.Application.Commands.RegisterUserCommand
{
    public class RegisterUserCommandResult
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }

        public RegisterUserCommandResult(Guid userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}