using System;

namespace DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand.DTOs
{
    public class UserCredentials
    {
        public Guid UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}