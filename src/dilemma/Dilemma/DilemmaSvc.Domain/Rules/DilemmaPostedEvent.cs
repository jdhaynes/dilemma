using System;
using Common.Domain;

namespace Dilemma.Rules
{
    public class DilemmaPostedEvent : IDomainEvent
    {
        public Guid DilemmaId { get;  }
        public Guid PosterId { get; }

        public DilemmaPostedEvent(Guid dilemmaId, Guid posterId)
        {
            DilemmaId = dilemmaId;
            PosterId = posterId;
        }
    }
}