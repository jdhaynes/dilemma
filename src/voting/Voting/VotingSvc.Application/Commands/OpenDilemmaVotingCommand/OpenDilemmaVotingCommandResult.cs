using System;

namespace VotingSvc.Application.Commands.OpenDilemmaVotingCommand
{
    public class OpenDilemmaVotingCommandResult
    {
        public DateTime Opened { get; set; }
        public DateTime ScheduledClose { get; set; }
    }
}