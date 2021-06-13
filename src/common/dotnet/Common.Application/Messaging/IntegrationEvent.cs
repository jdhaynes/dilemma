using System;

namespace DilemmaApp.Services.Common.Application.Messaging
{
    public class IntegrationEvent
    {
        public Guid EventId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}