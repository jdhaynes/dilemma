using System;

namespace DilemmaApp.Services.Dilemma.Application.Interfaces
{
    public interface IDilemmaRepository
    {
        void GetDilemma(Guid id);
        void AddDilemma(Domain.Dilemma.Model.Dilemma dilemma);
        void UpdateDilemma(Domain.Dilemma.Model.Dilemma dilemma);
        void DeleteDilemma(Guid id);
    }
}