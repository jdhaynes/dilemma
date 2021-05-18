using System;

namespace DilemmaApp.Services.Dilemma.Application.Interfaces
{
    public interface IDilemmaRepository
    {
        Domain.Dilemma.Model.Dilemma GetDilemma(Guid id);
        void AddDilemma(Domain.Dilemma.Model.Dilemma dilemma);
        void UpdateDilemma(Domain.Dilemma.Model.Dilemma dilemma);
        void DeleteDilemma(Domain.Dilemma.Model.Dilemma dilemma);
    }
}