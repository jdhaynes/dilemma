using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using DilemmaApp.Services.Common.Application;
using MediatR;

namespace DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand,
        Response<AuthenticateUserCommandResult>>
    {
        private readonly IAuthTokenService _tokenService;

        public AuthenticateUserCommandHandler(IAuthTokenService tokenService)
        {
            _tokenService = tokenService;
        }
        
        public Task<Response<AuthenticateUserCommandResult>> Handle(AuthenticateUserCommand request,
            CancellationToken cancellationToken)
        {
            string userId = _tokenService.ValidateToken(request.Token);

            AuthenticateUserCommandResult result = new AuthenticateUserCommandResult()
            {
                UserId = ParseUserIdFromToken(userId),
                IsAuthenticated = userId != null
            };
            
            ResponseState responseState = result.IsAuthenticated
                ? ResponseState.Ok
                : ResponseState.Error;

            Response<AuthenticateUserCommandResult> response =
                new Response<AuthenticateUserCommandResult>(result, responseState);

            return Task.FromResult(response);
        }

        private Guid? ParseUserIdFromToken(string userId)
        {
            if (userId == null)
            {
                return null;
            }

            return new Guid(userId);
        }
    }
}