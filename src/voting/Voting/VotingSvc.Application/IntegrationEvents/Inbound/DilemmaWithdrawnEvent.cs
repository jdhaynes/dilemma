using System;
using DilemmaApp.Services.Common.Application.Messaging;

namespace VotingSvc.Application.IntegrationEvents.Inbound
{
    public class DilemmaWithdrawnEvent : IntegrationEvent
    {
        public Guid DilemmaId { get; set; }
    }
}