using System;

namespace VotingSvc.Domain.Dilemma.Model
{
    public class Option
    {
        public Guid Id { get; private set; }
        public Guid DilemmaId { get; private set; }
        public int VoteCount { get; private set; }
        
        public Option(Guid id, Guid dilemmaId, int voteCount)
        {
            Id = id;
            DilemmaId = dilemmaId;
            VoteCount = voteCount;
        }
    }
}