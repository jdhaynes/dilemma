using System;
using FluentValidation;

namespace DilemmaSvc.Application.Commands.PostDilemmaToTopic
{
    public class PostDilemmaToTopicCommandValidator : AbstractValidator<PostDilemmaToTopicCommand>
    {
        public PostDilemmaToTopicCommandValidator()
        {
            RuleFor(x => x.TopicId).NotEqual(Guid.Empty);
            RuleFor(x => x.PosterId).NotEqual(Guid.Empty);
            RuleFor(x => x.Question).NotNull();
            RuleFor(x => x.Question).NotEmpty();
            RuleFor(x => x.Question).Length(140);
            RuleFor(x => x.Options).NotNull();
            RuleForEach(x => x.Options).SetValidator(new OptionValidator());
        }

        private class OptionValidator : AbstractValidator<PostDilemmaToTopicCommand.Option>
        {
            public OptionValidator()
            {
                RuleFor(x => x.Description).Length(40);
            }
        }
    }
    
}