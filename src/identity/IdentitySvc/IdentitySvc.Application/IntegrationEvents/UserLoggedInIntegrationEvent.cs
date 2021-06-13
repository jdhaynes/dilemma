using System;
using DilemmaApp.Services.Common.Application.Messaging;

namespace DilemmaApp.IdentitySvc.Application.IntegrationEvents
{
    public class UserLoggedInIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        public UserLoggedInIntegrationEvent(Guid userId = default)
        {
            UserId = userId;
        }
    }
}