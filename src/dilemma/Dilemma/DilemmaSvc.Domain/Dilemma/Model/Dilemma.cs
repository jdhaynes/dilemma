using System;
using System.Collections.Generic;
using System.Linq;
using DilemmaApp.Services.Common.Domain;
using DilemmaApp.Services.Dilemma.Domain.Dilemma.Events;

namespace DilemmaApp.Services.Dilemma.Domain.Dilemma.Model
{
    public class Dilemma : Entity
    {
        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Guid PosterId { get; private set; }
        public string Question { get; private set; }
        public DateTime PostedDate { get; private set; }

        public bool IsWithdrawn => (WithdrawnDate != null);
        public DateTime? WithdrawnDate { get; private set; }

        private List<Option> _options;
        public IReadOnlyCollection<Option> Options => _options.AsReadOnly();
        public int OptionCount => _options.Count;

        private const int MinNumberOptions = 2;
        private const int MaxNumberOptions = 4;

        private Dilemma()
        {
        }

        public Dilemma(Guid id, Guid topicId, Guid posterId, string question)
        {
            Id = id;
            TopicId = topicId;
            PosterId = posterId;
            Question = question;
            WithdrawnDate = null;
            _options = new List<Option>();
        }

        public void PostToTopic()
        {
            if (OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }

            if (OptionCount - 1 < MinNumberOptions)
            {
                throw new DomainRuleException("TOO_FEW_OPTIONS");
            }

            PostedDate = DateTime.Now;
            RaiseDomainEvent(new DilemmaPostedEvent(Id, PosterId,
                _options.Select(o => o.Id).ToList()));
        }

        public void AddOption(Guid optionId, string description)
        {
            if (OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }

            _options.Add(new Option(optionId, Id, description));
        }

        public void RemoveOption(Guid optionId)
        {
            if (OptionCount - 1 < MinNumberOptions)
            {
                throw new DomainRuleException("TOO_FEW_OPTIONS");
            }

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

            RaiseDomainEvent(new DilemmaWithdrawnEvent(Id, withdrawnDate));
        }
    }
}