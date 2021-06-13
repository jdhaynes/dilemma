using FluentValidation;

namespace DilemmaApp.IdentitySvc.Application.Commands.LoginUserCommand
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}