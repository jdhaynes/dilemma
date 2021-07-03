using System;
using DilemmaApp.Services.Common.Application.Messaging;

namespace VotingSvc.Application.IntegrationEvents.Outbound
{
    public class VoteCastEvent : IntegrationEvent
    {
        public Guid VoterId { get; set; }
        public Guid? OptionId { get; set; }
        public DateTime VotedAt { get; set; }
    }
}