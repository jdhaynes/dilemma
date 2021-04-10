using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain;
using DilemmaSvc.Domain.Events.Dilemma;

namespace DilemmaSvc.Domain.Model.Dilemma
{
    public class Dilemma : Entity
    {
        private const int MinNumberOptions = 2;
        private const int MaxNumberOptions = 4;

        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Poster Poster { get; private set; }
        public string Question { get; private set; }
        public DateTime? PostedDate { get; private set; }

        public bool IsWithdrawn { get; private set; }
        public DateTime? WithdrawnDate { get; private set; }

        private List<Option> _options;
        public IReadOnlyCollection<Option> Options => _options.AsReadOnly();
        public int OptionCount => _options.Count;
        private List<Guid> OptionIds => Options.Select(x => x.Id).ToList();

        private Dilemma()
        {
            // No public constructor - consumer of domain must be instantiated
            // through factory methods.
        }
        
        public void Post()
        {
            if (OptionCount < MinNumberOptions)
            {
                throw new DomainRuleException("TOO_FEW_OPTIONS");
            }

            if (OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }

            PostedDate = DateTime.Now;
            RaiseDomainEvent(new DilemmaPostedEvent(Id, OptionIds));
        }

        public void AddOption(Guid optionId, string description, byte[] image)
        {
            if (OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }
            
            _options.Add(new Option(optionId, new OptionContent(description, image)));
        }

        public void RemoveOption(Guid optionId)
        {
            Option option = _options.SingleOrDefault(o => o.Id == optionId);
            if (option != null)
            {
                _options.Remove(option);
            }
            else
            {
                throw new DomainRuleException("OPTION_DOESNT_EXIST");
            }
        }

        public void Withdraw()
        {
            DateTime withdrawnDate = DateTime.Now;

            WithdrawnDate = withdrawnDate;
            IsWithdrawn = true;

            RaiseDomainEvent(new DilemmaWithdrawnEvent(Id, withdrawnDate));
        }
    }
}