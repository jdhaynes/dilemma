using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.IdentitySvc.Application.Interfaces;
using DilemmaApp.IdentitySvc.Domain.Models;
using DilemmaApp.Services.Common.Application;
using MediatR;
using MediatR.Pipeline;

namespace DilemmaApp.IdentitySvc.Application.Commands.RegisterUserCommand
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand,
        Response<RegisterUserCommandResult>>
    {
        private IUserRepository _userRepository;
        private IAuthTokenService _tokenService;
        private IPasswordService _passwordService;

        public RegisterUserCommandHandler(IUserRepository userRepository,
            IAuthTokenService tokenService, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordService = passwordService;
        }

        public Task<Response<RegisterUserCommandResult>> Handle(RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            Guid userId = Guid.NewGuid();
            string salt = _passwordService.GenerateSalt();
            string hash = _passwordService.GenerateHash(request.Password, salt);

            User user = User.Register(userId, request.FirstName, request.LastName,
                request.Email, request.DateOfBirth, hash, salt);

            _userRepository.AddUser(user);

            string authToken = _tokenService.GenerateToken(userId.ToString());
            
            RegisterUserCommandResult result = new RegisterUserCommandResult(userId, authToken);
            Response<RegisterUserCommandResult> response =
                new Response<RegisterUserCommandResult>(result, ResponseState.Created);

            return Task.FromResult(response);
        }
    }
}