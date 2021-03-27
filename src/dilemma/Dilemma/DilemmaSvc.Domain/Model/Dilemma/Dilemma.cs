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

        private Dilemma()
        {
            // No public constructor - consumer of domain must be instantiated
            // through factory methods.
        }

        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Poster Poster { get; private set; }
        public string Question { get; private set; }
        public DateTime PostedDate { get; private set; }

        public bool IsWithdrawn { get; private set; }
        public DateTime? WithdrawnDate { get; private set; }

        public List<Option> Options { get; set; }
        public int OptionCount => Options.Count;

        public static Dilemma PostDilemmaToTopic(Guid id, Guid topicId, string question,
            List<Option> options)
        {
            Dilemma dilemma = new Dilemma
            {
                Id = id,
                TopicId = topicId,
                Question = question,
                PostedDate = DateTime.Now,
                WithdrawnDate = null,
                Options = new List<Option>()
            };

            dilemma.RaiseDomainEvent(new DilemmaPostedEvent(id));
            return dilemma;
        }
        
        public void AddOption(Guid optionId, string description, byte[] image)
        {
            Options.Add(new Option(optionId, Id, new OptionContent(description, image)));
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