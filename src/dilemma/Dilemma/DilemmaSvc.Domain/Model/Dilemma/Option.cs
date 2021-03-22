using System;

namespace Dilemma.Domain
{
    public abstract class Option
    {
        public Guid Id { get; }
        public Guid DilemmaId { get; }
        public OptionContent Content { get; }
    }
}