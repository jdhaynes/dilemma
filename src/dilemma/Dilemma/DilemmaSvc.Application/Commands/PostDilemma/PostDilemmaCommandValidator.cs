using System;
using FluentValidation;

namespace DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma
{
    public class PostDilemmaCommandValidator : AbstractValidator<PostDilemmaCommand>
    {
        public PostDilemmaCommandValidator()
        {
            RuleFor(x => x.TopicId).NotEqual(Guid.Empty);
            RuleFor(x => x.PosterId).NotEqual(Guid.Empty);
            RuleFor(x => x.Question).NotNull();
            RuleFor(x => x.Question).NotEmpty();
            RuleFor(x => x.Question).MaximumLength(140);
            RuleFor(x => x.Options).NotNull();
            RuleForEach(x => x.Options).SetValidator(new OptionValidator());
        }

        private class OptionValidator : AbstractValidator<PostDilemmaCommand.Option>
        {
            public OptionValidator()
            {
                RuleFor(x => x.Description).MaximumLength(40);
            }
        }
    }
    
}