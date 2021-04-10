using System;
using System.Collections.Generic;
using System.Linq;
using DilemmaApp.Services.Common.Domain;
using DilemmaApp.Services.Dilemma.Domain.Events.Dilemma;

namespace DilemmaApp.Services.Dilemma.Domain.Model.Dilemma
{
    public class Dilemma : Entity
    {
        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Guid PosterId { get; private set; }
        public string Question { get; private set; }
        public DateTime PostedDate { get; private set; }

        public bool IsWithdrawn => (WithdrawnDate == null);
        public DateTime? WithdrawnDate { get; private set; }

        private List<Option> _options;
        public IReadOnlyCollection<Option> Options => _options.AsReadOnly();
        public int OptionCount => _options.Count;

        private const int MinNumberOptions = 2;
        private const int MaxNumberOptions = 4;

        private Dilemma()
        {
            // No public constructor - consumer of domain must be instantiated
            // through factory methods.
        }

        public static Dilemma PostDilemmaToTopic(Guid id, Guid topicId, Guid posterId,
            string question, List<Option> options)
        {
            Dilemma dilemma = new Dilemma()
            {
                Id = id,
                TopicId = topicId,
                PosterId = posterId,
                Question = question,
                PostedDate = DateTime.Now,
                WithdrawnDate = null,
                _options = options
            };

            if (dilemma.OptionCount < MinNumberOptions)
            {
                throw new DomainRuleException("TOO_FEW_OPTIONS");
            }

            if (dilemma.OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }

            DilemmaPostedEvent postedEvent = new DilemmaPostedEvent(dilemma.Id, dilemma.PosterId,
                dilemma._options.Select(x => x.Id).ToList());
            dilemma.RaiseDomainEvent(postedEvent);

            return dilemma;
        }

        public void AddOption(Guid optionId, Guid imageId, string description)
        {
            if (OptionCount > MaxNumberOptions)
            {
                throw new DomainRuleException("TOO_MANY_OPTIONS");
            }

            _options.Add(new Option(optionId, imageId, description));
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