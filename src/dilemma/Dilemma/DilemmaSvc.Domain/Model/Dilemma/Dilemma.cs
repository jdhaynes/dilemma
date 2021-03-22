using System;
using System.Collections.Generic;
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
        public string Details { get; private set; }
        public DateTime PostedDate { get; private set; }

        public bool IsWithdrawn { get; private set; }
        public DateTime? WithdrawnDate { get; private set; }
        
        public List<Option> Options { get; set; }
        public int OptionCount => Options.Count;

        private Dilemma()
        {
            // No public constructor - consumer of domain must be instantiated
            // through factory methods.
        }

        static Dilemma PostToTopic(Guid id, Guid topicId, string question, string details)
        {
            return new Dilemma
            {
                Id = id,
                TopicId = topicId,
                Question = question,
                Details = details,
                PostedDate = DateTime.Now,
                WithdrawnDate = null,
                Options = new List<Option>()
            };
        }
        
        public void AddOption()
        {
            // Can only be done if building up dilemma. After posted, cannot be added.
            // Cannot exceed max number of options.
            throw new NotImplementedException();
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