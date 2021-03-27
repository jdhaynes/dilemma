using System;
using Common.Domain;

namespace Dilemma.Rules
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