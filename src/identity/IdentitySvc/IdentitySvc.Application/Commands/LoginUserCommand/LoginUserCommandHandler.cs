using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand.DTOs;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Common.Application.ErrorHandling;
using DilemmaApp.Services.Common.Application.Interfaces;
using DilemmaApp.Services.Common.Application.Messaging;
using MediatR;

namespace DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,
        Response<LoginUserCommandResult>>
    {
        private IPasswordService _passwordService;
        private ISqlConnectionFactory _sqlConnectionFactory;
        private IAuthTokenService _tokenService;
        private IMessageBus _messageBus;

        public LoginUserCommandHandler(IPasswordService passwordService,
            ISqlConnectionFactory sqlConnectionFactory, 
            IAuthTokenService tokenService,
            IMessageBus messageBus)
        {
            _passwordService = passwordService;
            _sqlConnectionFactory = sqlConnectionFactory;
            _tokenService = tokenService;
            _messageBus = messageBus;
        }

        public Task<Response<LoginUserCommandResult>> Handle(LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            Response<LoginUserCommandResult> response = new Response<LoginUserCommandResult>();
            UserCredentials credentials = GetUserCredentials(request.Email);
            
            if (credentials == null)
            {
                response.Payload = new LoginUserCommandResult(false, null);
                response.State = ResponseState.Error;
                response.RaiseError(ErrorType.NotAuthorized, 
                    "NOT_AUTHENTICATED",
                    "User credentials could not be authenticated");
            }
            else
            {
                bool isAuthenticated = _passwordService.AuthenticatePassword(
                    request.Password,
                    credentials.PasswordSalt,
                    credentials.PasswordHash);
   
                if (isAuthenticated)
                {
                    string token = _tokenService.GenerateToken(credentials.UserId.ToString());
                    response.Payload = new LoginUserCommandResult(true, token);
                    response.State = ResponseState.Ok;
                }
                else
                {
                    response.Payload = new LoginUserCommandResult(false, null);
                    response.State = ResponseState.Error;
                    response.RaiseError(ErrorType.NotAuthorized, 
                        "NOT_AUTHENTICATED",
                        "User credentials could not be authenticated");
                }
            }

            return Task.FromResult(response);
        }

        private UserCredentials GetUserCredentials(string email)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection())
            {
                string sql = $@"
                    SELECT u.password AS {nameof(UserCredentials.PasswordHash)},
	                       u.salt     AS {nameof(UserCredentials.PasswordSalt)},
                           u.id       AS {nameof(UserCredentials.UserId)}
                    FROM ""user"" AS u
                    WHERE email = @Email";

                UserCredentials credentials = connection
                    .Query<UserCredentials>(sql, param: new {Email = email})
                    .SingleOrDefault();

                return credentials;
            }
        }
    }
}