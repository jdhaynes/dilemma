using System;
using System.Collections.Generic;
using Common.Domain;

namespace Dilemma.Rules
{
    public class DilemmaPostedEvent : IDomainEvent
    {
        public Guid DilemmaId { get;  }
        public List<Guid> OptionIds { get; }

        public DilemmaPostedEvent(Guid dilemmaId, List<Guid> optionIds)
        {
            DilemmaId = dilemmaId;
            OptionIds = optionIds;
        }
    }
}