using System;
using DilemmaApp.Services.Common.Domain;

namespace DilemmaApp.Services.Dilemma.Domain.Topic.Events
{
    public class TopicPostedEvent : IDomainEvent
    {
        public Guid TopicId { get; }
        public string Name { get; }

        public TopicPostedEvent(Guid topicId, string name)
        {
            TopicId = topicId;
            Name = name;
        }
    }
}