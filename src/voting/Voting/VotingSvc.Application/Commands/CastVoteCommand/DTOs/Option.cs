using System;

namespace VotingSvc.Application.Commands.CastVoteCommand.DTOs
{
    public class Option
    {
        public Guid Id { get; set; }
        public int VoteCount { get; set; }
    }
}