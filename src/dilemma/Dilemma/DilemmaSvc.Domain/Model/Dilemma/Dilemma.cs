using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain;
using Dilemma.Rules;

namespace Dilemma.Domain
{
    public class Dilemma : Entity
    {
        private const int MinNumberOptions = 2;
        private const int MaxNumberOptions = 4;

        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Poster Poster { get; private set; }
        public string Question { get; private set; }
        public Nullable<DateTime> PostedDate { get; private set; }

        public bool IsWithdrawn { get; private set; }
        public Nullable<DateTime> WithdrawnDate { get; private set; }

        private List<Option> _options;
        public IReadOnlyCollection<Option> Options => _options.AsReadOnly();
        public int OptionCount => _options.Count;
        private List<Guid> _optionIds => Options.Select(x => x.Id).ToList();

        private Dilemma()
        {
            // No public constructor - consumer of domain must be instantiated
            // through factory methods.
        }

        public static Dilemma StartDraft(Guid id, Guid topicId, string question, Poster poster)
        {
            Dilemma dilemma = new Dilemma
            {
                Id = id,
                TopicId = topicId,
                Question = question,
                PostedDate = null,
                WithdrawnDate = null,
                Poster = poster,
                _options = new List<Option>(),
            };

            return dilemma;
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
            RaiseDomainEvent(new DilemmaPostedEvent(Id, _optionIds));
        }

        public void AddOption(Guid optionId, string description, byte[] image)
        {
            if (OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }
            
            _options.Add(new Option(optionId, new OptionContent(description, image)));
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