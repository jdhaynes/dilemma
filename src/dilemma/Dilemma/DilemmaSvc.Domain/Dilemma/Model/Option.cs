using System;

namespace DilemmaApp.Services.Dilemma.Domain.Model.Dilemma
{
    public class Option
    {
        public Guid Id { get; }
        public Guid ImageId { get; }
        public string Description { get; }

        public Option(Guid id, Guid imageId, string description)
        {
            Id = id;
            ImageId = imageId;
            Description = description;
        }
    }
}