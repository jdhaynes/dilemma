using System;
using DilemmaApp.Services.Common.Application.Messaging;

namespace DilemmaApp.IdentitySvc.Application.IntegrationEvents
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}