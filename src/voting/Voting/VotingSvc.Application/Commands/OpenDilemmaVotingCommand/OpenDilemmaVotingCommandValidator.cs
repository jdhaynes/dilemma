using System.Data;
using FluentValidation;

namespace VotingSvc.Application.Commands.OpenDilemmaVotingCommand
{
    public class OpenDilemmaVotingCommandValidator : AbstractValidator<OpenDilemmaVotingCommand>
    {
        public OpenDilemmaVotingCommandValidator()
        {
            RuleFor(x => x.DilemmaId).NotEmpty();
            RuleFor(x => x.PosterId).NotEmpty();
            RuleFor(x => x.OptionIds).NotNull();
            RuleForEach(x => x.OptionIds).NotEmpty();
        }
    }
}