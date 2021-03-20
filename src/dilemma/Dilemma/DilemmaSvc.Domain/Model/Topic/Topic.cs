using System;

namespace Dilemma.Domain
{
    public class Topic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        protected Topic(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        static Topic Post(Guid id, string name, string description)
        {
            return new Topic(id, name, description);
        }
        
        public void ChangeDescription(string description)
        {
            Description = description;
        }
    }
}
