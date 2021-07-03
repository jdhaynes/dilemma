using FluentValidation;

namespace VotingSvc.Application.Commands.CastVoteCommand
{
    public class CastVoteCommandValidator : AbstractValidator<CastVoteCommand>
    {
        public CastVoteCommandValidator()
        {
            RuleFor(x => x.DilemmaId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}