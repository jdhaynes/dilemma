using System;

namespace DilemmaSvc.Domain.Model.Dilemma
{
    public class Option
    {
        public Guid Id { get; }
        public OptionContent Content { get; }

        public Option(Guid id, OptionContent content)
        {
            Id = id;
            Content = content;
        }
    }
}