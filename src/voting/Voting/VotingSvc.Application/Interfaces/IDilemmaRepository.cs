using System;
using VotingSvc.Domain.Dilemma.Model;

namespace VotingSvc.Application.Interfaces
{
    public interface IDilemmaRepository
    {
        public Dilemma GetDilemmaForOption(Guid optionId);
    }
}