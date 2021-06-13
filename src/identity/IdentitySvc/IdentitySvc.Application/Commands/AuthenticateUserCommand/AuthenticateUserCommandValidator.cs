using FluentValidation;

namespace DilemmaApp.IdentitySvc.Application.Commands.AuthenticateUserCommand
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}