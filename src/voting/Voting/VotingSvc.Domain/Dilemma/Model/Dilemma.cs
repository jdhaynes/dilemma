using System;
using System.Collections.Generic;
using System.Linq;
using DilemmaApp.Services.Common.Domain;

namespace VotingSvc.Domain.Dilemma.Model
{
    public class Dilemma
    {
        private const int VOTING_WINDOW_HOURS = 24;

        public Guid Id { get; private set; }
        public DateTime? Opened { get; private set; }
        public DateTime? Closed { get; private set; }
        public Guid? UserVoteId { get; private set; }

        public bool HasAlreadyVoted => UserVoteId != null;
        public bool HasBeenOpen => Opened != null;
        public bool IsCurrentlyOpen => DateTime.Now > Opened && DateTime.Now < Closed;
        
        public void OpenForVoting()
        {
            if (HasBeenOpen || IsCurrentlyOpen)
            {
                throw new DomainRuleException("DILEMMA_ALREADY_OPEN",
                    "Dilemma cannot be opened for voting if already open or previously opened");
            }

            Opened = DateTime.Now;
            Closed = Opened.Value.AddHours(VOTING_WINDOW_HOURS);
        }

        public void CloseForVoting()
        {
            if (!IsCurrentlyOpen)
            {
                throw new DomainRuleException("DILEMMA_ALREADY_CLOSED",
                    "Dilemma cannot be closed for voting if already closed");
            }

            Closed = DateTime.Now;
        }

        public Vote Vote(Guid voteId, Guid? optionId, Guid voterId)
        {
            if (!IsCurrentlyOpen)
            {
                throw new DomainRuleException("CANNOT_VOTE_CLOSED",
                    "Cannot vote as dilemma is closed for voting");
            }

            if (HasAlreadyVoted)
            {
                throw new DomainRuleException("ALREADY_VOTED",
                    "Cannot vote if user has already voted");
            }

            UserVoteId = voteId;
            return new Vote(voteId, Id, optionId, voterId, DateTime.Now);
        }
    }
}