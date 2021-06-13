using System;

namespace DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand.DTOs
{
    public class UserCredentials
    {
        public Guid UserId { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
    }
}