using System;
using DilemmaApp.Services.Common.Application.Messaging;

namespace VotingSvc.Application.IntegrationEvents.Outbound
{
    public class DilemmaVotingOpenedEvent : IntegrationEvent
    {
        public Guid DilemmaId { get; set; }
        public DateTime Opened { get; set; }
        public DateTime ScheduledClose { get; set; }
    }
}