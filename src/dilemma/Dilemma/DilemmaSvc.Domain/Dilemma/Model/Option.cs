using System;

namespace DilemmaApp.Services.Dilemma.Domain.Dilemma.Model
{
    public class Option
    {
        public Guid Id { get; set;}
        public Guid DilemmaId { get; set;}
        public string Description { get; set;}

        public Option(Guid id, Guid dilemmaId, string description)
        {
            Id = id;
            DilemmaId = dilemmaId;
            Description = description;
        }
    }
}