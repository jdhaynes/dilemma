using System;
using Common.Domain;

namespace DilemmaSvc.Domain.Events.Dilemma
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