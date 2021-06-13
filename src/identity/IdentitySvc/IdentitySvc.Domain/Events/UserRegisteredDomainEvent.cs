using System;
using DilemmaApp.Services.Common.Domain;

namespace DilemmaApp.IdentitySvc.Domain.Events
{
    public class UserRegisteredDomainEvent : IDomainEvent
    {
        public Guid UserId { get; private set; }

        public UserRegisteredDomainEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}