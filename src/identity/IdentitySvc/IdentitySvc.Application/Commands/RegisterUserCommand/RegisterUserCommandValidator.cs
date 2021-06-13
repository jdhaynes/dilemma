using System;
using FluentValidation;

namespace DilemmaApp.IdentitySvc.Application.Commands.RegisterUserCommand
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.DateOfBirth)
                .Must(DateNotDefault)
                .Must(DateNotInFuture);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(64)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]")
                .Matches("[^a-zA-Z0-9]");
        }

        private bool DateNotDefault(DateTime dateTime)
        {
            return dateTime != default;
        }

        private bool DateNotInFuture(DateTime dateTime)
        {
            return dateTime < DateTime.Now;
        }
    }
}