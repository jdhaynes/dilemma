using System;

namespace DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand
{
    public class AuthenticateUserCommandResult
    {
        public bool IsAuthenticated { get; set; }
        public Guid? UserId { get; set; }
    }
}