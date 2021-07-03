using System;

namespace VotingSvc.Domain.Dilemma.Model
{
    public class Vote
    {
        public Guid Id { get; }
        public Guid DilemmaId { get; }
        public Guid? OptionId { get; }
        public Guid VoterId { get; }
        public DateTime Voted { get; }

        public Vote(Guid id, Guid dilemmaId, Guid? optionId, Guid voterId, DateTime voted)
        {
            Id = id;
            DilemmaId = dilemmaId;
            OptionId = optionId;
            VoterId = voterId;
            Voted = voted;
        }
    }
}