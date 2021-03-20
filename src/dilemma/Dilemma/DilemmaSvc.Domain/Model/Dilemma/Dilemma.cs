using System;
using System.Collections.Generic;
using System.Net;

namespace Dilemma.Domain
{
    public class Dilemma
    {
        private const int MinNumberOptions = 2;
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
        public bool IsOpen => CalculateIsOpen();
        public bool IsWithdrawn { get; private set; }

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
                PostedDate = DateTime.Now, // TODO
                ClosedDate = null,
                Options = new List<Option>()
            };
        }

        public void Post()
        {
            // Must be at least min options.
            // Must be less than max options.
            PostedDate = DateTime.Now;
        }

        public void AddOption()
        {
            // Can only be done if building up dilemma. After posted, cannot be added.
            // Cannot exceed max number of options.
            throw new NotImplementedException();
        }

        public void Withdraw()
        {
            // Must be open and not withdrawn already.
            IsWithdrawn = false;
            Close();
        }

        public void Close()
        {
            // Must be open and not withdrawn.
            ClosedDate = DateTime.Now;
        }

        private DateTime CalculateScheduledClosedDate()
        {
            return PostedDate.AddHours(HoursOpenAfterPosting);
        }

        private bool CalculateIsOpen()
        {
            return DateTime.Now < ClosedDate;
        }
    }
}