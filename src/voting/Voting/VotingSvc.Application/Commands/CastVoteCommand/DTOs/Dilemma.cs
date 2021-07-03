using System;
using System.Collections.Generic;

namespace VotingSvc.Application.Commands.CastVoteCommand.DTOs
{
    public class Dilemma
    {
        public Guid Id { get; set; }
        public DateTime? Opened { get; set; }
        public DateTime? Closed { get; set; }
        public ICollection<Option> Options { get; set; }
        public bool UserHasVoted { get; set; }
    }
}