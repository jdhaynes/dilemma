using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand.DTOs;
using DilemmaApp.IdentitySvc.Application.IntegrationEvents;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Common.Application.Interfaces;
using DilemmaApp.Services.Common.Application.Messaging;
using MediatR;

namespace DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,
        Response<LoginUserCommandResult>>
    {
        private IPasswordService _passwordService;
        private ISqlConnectionFactory _connectionFactory;
        private IAuthTokenService _tokenService;
        private IMessageBus _messageBus;

        public LoginUserCommandHandler(IPasswordService passwordService,
            ISqlConnectionFactory connectionFactory, IAuthTokenService tokenService,
            IMessageBus messageBus)
        {
            _passwordService = passwordService;
            _connectionFactory = connectionFactory;
            _tokenService = tokenService;
            _messageBus = messageBus;
        }

        public Task<Response<LoginUserCommandResult>> Handle(LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            UserCredentials credentials;
            LoginUserCommandResult result;

            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                string sql = $@"
                    SELECT u.password AS {nameof(UserCredentials.PasswordHash)},
	                       u.salt     AS {nameof(UserCredentials.PasswordSalt)},
                           u.id       AS {nameof(UserCredentials.UserId)}
                    FROM ""user"" AS u
                    WHERE email = @Email";

                credentials = connection
                    .Query<UserCredentials>(sql, param: new {Email = request.Email})
                    .SingleOrDefault();
            }

            if (credentials == null)
            {
                result = new LoginUserCommandResult(false, null);
            }
            else
            {
                bool isAuthenticated = _passwordService.AuthenticatePassword(request.Password,
                    credentials.PasswordSalt, credentials.PasswordHash);

                string token = isAuthenticated
                    ? _tokenService.GenerateToken(credentials.UserId.ToString())
                    : null;

                result = new LoginUserCommandResult(isAuthenticated, token);
                
                _messageBus.PublishIntegrationEvent(
                    new UserLoggedInIntegrationEvent(credentials.UserId));
            }

            Response<LoginUserCommandResult> response =
                new Response<LoginUserCommandResult>(result, ResponseState.Ok);
            
            return Task.FromResult(response);
        }
    }
}