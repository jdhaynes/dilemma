using System;
using DilemmaApp.Services.Common.Domain;

namespace DilemmaApp.Services.Dilemma.Domain.Dilemma.Events
{
    public class DilemmaWithdrawnEvent : IDomainEvent
    {
        public Guid DilemmaId { get; private set; }
        public DateTime DateWithdrawn { get; private set; }

        public DilemmaWithdrawnEvent(Guid dilemmaId, DateTime dateWithdrawn)
        {
            DilemmaId = dilemmaId;
            DateWithdrawn = dateWithdrawn;
        }
    }
}