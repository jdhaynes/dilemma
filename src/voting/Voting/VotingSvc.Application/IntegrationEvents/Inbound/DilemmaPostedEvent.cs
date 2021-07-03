using System;
using System.Collections.Generic;
using DilemmaApp.Services.Common.Application.Messaging;

namespace VotingSvc.Application.IntegrationEvents.Inbound
{
    public class DilemmaPostedEvent : IntegrationEvent
    {
        public Guid DilemmaId { get; set; }
        public Guid PosterId { get; set; }
        public List<Guid> OptionIds { get; set; }
    }
}