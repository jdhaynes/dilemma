namespace DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand
{
    public class LoginUserCommandResult
    {
        public bool IsAuthenticated { get; }
        public string Token { get; }

        public LoginUserCommandResult(bool isAuthenticated, string token)
        {
            IsAuthenticated = isAuthenticated;
            Token = token;
        }
    }
}