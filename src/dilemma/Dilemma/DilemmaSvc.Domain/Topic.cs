using System;

namespace Dilemma.Domain
{
    public class Topic
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        
        protected Topic(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        static Topic Create(string name, string description)
        {
            return new Topic(Guid.NewGuid(), name, description);
        }
    }
}
