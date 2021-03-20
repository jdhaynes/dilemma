using System;

namespace Dilemma.Domain
{
    public class Topic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsOpen { get; private set; }
        
        protected Topic(Guid id, string name, string description, bool isOpen)
        {
            Id = id;
            Name = name;
            Description = description;
            IsOpen = isOpen;
        }

        static Topic Create(Guid id, string name, string description, bool isOpen)
        {
            return new Topic(id, name, description, isOpen);
        }
        
        public void Close()
        {
            IsOpen = false;
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }
    }
}
