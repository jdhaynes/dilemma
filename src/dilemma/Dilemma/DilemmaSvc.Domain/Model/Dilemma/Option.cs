using System;

namespace Dilemma.Domain
{
    public class Option
    {
        public Guid Id { get; }
        public Guid DilemmaId { get; }
        public OptionContent Content { get; }

        public Option(Guid id, Guid dilemmaId, OptionContent content)
        {
            Id = id;
            DilemmaId = dilemmaId;
            Content = content;
        }
    }
}