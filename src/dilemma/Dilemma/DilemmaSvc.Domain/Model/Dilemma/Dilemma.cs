using System;
using System.Collections.Generic;

namespace Dilemma.Domain
{
    public class Dilemma
    {
        private const int MaxNumberOptions = 4;
        private const int HoursOpenAfterPosting = 24;
        
        public Guid Id { get; private set; }
        public Guid TopicId { get; private set; }
        public Poster Poster { get; private set; }
        public string Question { get; private set; }
        public string Details { get; private set; }
        public DateTime PostedDate { get; private set; }
        public DateTime ScheduledClosedDate => CalculateScheduledClosedDate();
        public DateTime? ClosedDate { get; private set; }
        public List<Option> Options { get; set; }
        public int OptionCount => Options.Count;
        public bool IsOpen { get; set; }

        protected Dilemma()
        {
            // No public constructor - consumer of domain must be instantiated
            // through factory methods.
        }

        static Dilemma Post(Guid id, Guid topicId, string question, string details)
        {
            return new Dilemma
            {
                Id = id,
                TopicId = topicId,
                Question = question,
                Details = details,
                ClosedDate = null,
                Options = new List<Option>()
            };
        }

        public void AddOption()
        {
            throw new NotImplementedException();
        }

        public void RemoveOption(Guid optionId)
        {
            throw new NotImplementedException();
        }

        public void Withdraw()
        {
            throw new NotImplementedException();
        }

        private DateTime CalculateScheduledClosedDate()
        {
            return PostedDate.AddHours(HoursOpenAfterPosting);
        }
    }
}