using System.Data;
using FluentValidation;

namespace DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma
{
    public class WithdrawDilemmaCommandValidator : AbstractValidator<WithdrawDilemmaCommand>
    {
        public WithdrawDilemmaCommandValidator()
        {
            RuleFor(x => x.DilemmaId).NotEmpty();
        }
    }
}